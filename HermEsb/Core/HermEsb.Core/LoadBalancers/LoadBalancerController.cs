using System.Collections.Generic;
using System.Linq;

namespace HermEsb.Core.LoadBalancers
{
    /// <summary>
    /// Mantiene la relacion entre la clave y los balanceadores de carga
    /// </summary>
    public class LoadBalancerController<TKey,TObject>
    {

        private readonly IDictionary<TKey, ILoadBalancer<TObject>> _loadBalancers;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadBalancerController&lt;TKey, TObject&gt;"/> class.
        /// </summary>
        public LoadBalancerController()
        {
            _loadBalancers = new Dictionary<TKey, ILoadBalancer<TObject>>();
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(TKey key, TObject value)
        {
            if (!_loadBalancers.ContainsKey(key))
            {
                _loadBalancers.Add(key, new RoundRobinLoadBalancer<TObject>());
            }
            _loadBalancers[key].Add(value);
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Remove(TKey key, TObject value)
        {
            if (_loadBalancers.ContainsKey(key))
            {
                _loadBalancers[key].Remove(value);

            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get { return _loadBalancers.Count; }
        }

        /// <summary>
        /// Existses the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public bool Exists(TObject obj)
        {
            return _loadBalancers.Values.Any(balancer => balancer.Exists(obj));
        }

        /// <summary>
        /// Gets the next values.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TObject> GetNextValues()
        {
            return _loadBalancers.Select(loadBalancer => loadBalancer.Value.Next());
        }
    }
}