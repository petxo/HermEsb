using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HermEsb.Core.Gateways;
using HermEsb.Core.LoadBalancers;
using HermEsb.Core.Processors.Router.Subscriptors;
using HermEsb.Logging;

namespace HermEsb.Core.Processors.Router.Outputs
{
    /// <summary>
    /// </summary>
    public class TypeHierarchicalKeyEngine : IHierarchicalKeyEngine<Type>
    {
        private readonly IDictionary<string, LoadBalancerController<string, IOutputGateway<string>>>
            _assignableTypesList;

        private readonly IDictionary<SubscriptionKey, List<IOutputGateway<string>>> _keyDictionary;
        private SpinLock _lockAsignableTypes;
        private SpinLock _lockKeys;
        private int _numThreadsRunning;


        /// <summary>
        ///     Initializes a new instance of the <see cref="TypeHierarchicalKeyEngine" /> class.
        /// </summary>
        public TypeHierarchicalKeyEngine()
        {
            _lockAsignableTypes = new SpinLock();
            _lockKeys = new SpinLock();
            _numThreadsRunning = 0;

            _assignableTypesList = new Dictionary<string, LoadBalancerController<string, IOutputGateway<string>>>();
            _keyDictionary = new Dictionary<SubscriptionKey, List<IOutputGateway<string>>>();
        }

        /// <summary>
        ///     Gets the keys.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubscriptionKey> GetKeys()
        {
            return _keyDictionary.Keys;
        }

        /// <summary>
        ///     Gets the output gateways.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOutputGateway<string>> GetOutputGateways()
        {
            var outputGateways = new List<IOutputGateway<string>>();

            bool lockTaken = false;
            _lockKeys.Enter(ref lockTaken);
            if (lockTaken)
            {
                foreach (var output in _keyDictionary.Values)
                {
                    outputGateways.AddRange(output);
                }
                _lockKeys.Exit();
            }
            return outputGateways;
        }

        /// <summary>
        ///     Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="service">The service.</param>
        /// <param name="outputGateway">The output gateway.</param>
        public void Add(SubscriptionKey key, Identification service, IOutputGateway<string> outputGateway)
        {
            LoggerManager.Instance.Info(string.Format("Add Type {0}", key.Key));
            bool lockTaken = false;
            _lockKeys.Enter(ref lockTaken);
            if (lockTaken)
            {
                try
                {
                    if (!_keyDictionary.ContainsKey(key))
                    {
                        _keyDictionary.Add(key, new List<IOutputGateway<string>>());
                    }
                    _keyDictionary[key].Add(outputGateway);

                    ReloadAssignableTypes(key, service, outputGateway);
                }
                catch (Exception exception)
                {
                    LoggerManager.Instance.Error(string.Format("Error al añadir suscriptor {0}", key.Key), exception);
                }
                finally
                {
                    LoggerManager.Instance.Info(string.Format("Se libera el lock {0}", key.Key));
                    _lockKeys.Exit();
                }
            }
        }

        /// <summary>
        ///     Removes the specified output gateway.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="service"></param>
        /// <param name="outputGateway">The output gateway.</param>
        public void Remove(SubscriptionKey key, Identification service, IOutputGateway<string> outputGateway)
        {
            LoggerManager.Instance.Info(string.Format("Remove Type {0}", key.Key));
            bool lockTaken = false;
            _lockKeys.Enter(ref lockTaken);
            if (lockTaken)
            {
                try
                {
                    if (_keyDictionary.ContainsKey(key))
                    {
                        _keyDictionary[key].Remove(outputGateway);
                        if (_keyDictionary[key].Count == 0)
                            _keyDictionary.Remove(key);
                    }
                }
                finally
                {
                    _lockKeys.Exit();
                }
            }

            lockTaken = false;
            _lockAsignableTypes.Enter(ref lockTaken);
            if (lockTaken)
            {
                try
                {
                    SpinWait.SpinUntil(() => _numThreadsRunning == 0);

                    foreach (var assignableType in _assignableTypesList)
                    {
                        assignableType.Value.Remove(service.Type, outputGateway);
                    }

                    foreach (var assignableType in _assignableTypesList.Where(pair => pair.Value.Count == 0).ToList())
                    {
                        _assignableTypesList.Remove(assignableType);
                    }
                }
                finally
                {
                    _lockAsignableTypes.Exit();
                }
            }
        }

        /// <summary>
        ///     Gets the message senders.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public IEnumerable<IOutputGateway<string>> GetMessageSenders(string key)
        {
            var outputGateways = new List<IOutputGateway<string>>();
            if (string.IsNullOrEmpty(key))
            {
                return outputGateways;
            }

            SpinWait.SpinUntil(() => !_lockAsignableTypes.IsHeld);
            Interlocked.Increment(ref _numThreadsRunning);
            try
            {
                if (_assignableTypesList.ContainsKey(key))
                {
                    outputGateways.AddRange(_assignableTypesList[key].GetNextValues());
                }
                else
                {
                    LoggerManager.Instance.Debug(string.Format("No se han encontrado suscriptores para la clave {0}",
                                                               key));
                }
            }
            finally
            {
                Interlocked.Decrement(ref _numThreadsRunning);
            }
            return outputGateways;
        }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        public void Clear()
        {
            bool lockTaken = false;
            _lockAsignableTypes.Enter(ref lockTaken);
            if (lockTaken)
            {
                SpinWait.SpinUntil(() => _numThreadsRunning == 0);
                _assignableTypesList.Clear();
                _lockAsignableTypes.Exit();
            }
        }

        /// <summary>
        ///     Reloads the assignable types.
        /// </summary>
        /// <param name="subscriptionKey">The subscription key.</param>
        /// <param name="service">The service.</param>
        /// <param name="outputGateway">The output gateway.</param>
        private void ReloadAssignableTypes(SubscriptionKey subscriptionKey, Identification service,
                                           IOutputGateway<string> outputGateway)
        {
            //Recargamos la lista de tipos asignables
            bool lockTaken = false;
            _lockAsignableTypes.Enter(ref lockTaken);
            if (lockTaken)
            {
                SpinWait.SpinUntil(() => _numThreadsRunning == 0);

                foreach (var assignableType in _assignableTypesList)
                {
                    KeyValuePair<SubscriptionKey, List<IOutputGateway<string>>> key =
                        _keyDictionary.FirstOrDefault(pair => pair.Key.Key == assignableType.Key);

                    if (key.Key != null && key.Key.IsAssignableKey(subscriptionKey.Key))
                    {
                        if (!assignableType.Value.Exists(outputGateway))
                        {
                            assignableType.Value.Add(service.Type, outputGateway);
                        }
                    }
                }

                if (!_assignableTypesList.ContainsKey(subscriptionKey.Key))
                {
                    var loadBalancerController = new LoadBalancerController<string, IOutputGateway<string>>();
                    _assignableTypesList.Add(subscriptionKey.Key, loadBalancerController);

                    loadBalancerController.Add(service.Type, outputGateway);
                }

                _lockAsignableTypes.Exit();
            }
        }
    }
}