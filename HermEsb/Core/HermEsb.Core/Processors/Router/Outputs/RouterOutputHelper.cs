using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Processors.Router.Subscriptors;
using HermEsb.Core.Serialization;
using HermEsb.Logging;

namespace HermEsb.Core.Processors.Router.Outputs
{
    /// <summary>
    /// </summary>
    public class RouterOutputHelper : IRouterOutputHelper
    {
        private readonly IDataContractSerializer _dataContractSerializer;
        private readonly IGatewaysRepository _gatewaysRepository;
        private readonly IDictionary<Identification, IOutputGateway<byte[]>> _subcriptorsList;
        private SpinLock _lockSubcriptorsList;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RouterOutputHelper" /> class.
        /// </summary>
        /// <param name="gatewaysRepository">The sender repository.</param>
        public RouterOutputHelper(IGatewaysRepository gatewaysRepository)
        {
            _gatewaysRepository = gatewaysRepository;
            _dataContractSerializer = new JsonDataContractSerializer();
            _subcriptorsList = new Dictionary<Identification, IOutputGateway<byte[]>>();
        }

        /// <summary>
        ///     Subscribes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="service"></param>
        /// <param name="sender">The sender.</param>
        public void Subscribe(SubscriptionKey type, Identification service, IOutputGateway<byte[]> sender)
        {
            bool lockTaken = false;
            _lockSubcriptorsList.Enter(ref lockTaken);
            if (lockTaken)
            {
                try
                {
                    if (!_subcriptorsList.ContainsKey(service))
                    {
                        _subcriptorsList.Add(service, sender);
                    }
                }
                catch (Exception exception)
                {
                    LoggerManager.Instance.Error(string.Format("Error al añadir suscriptor {0}", service.Id), exception);
                }
                finally
                {
                    LoggerManager.Instance.Info(string.Format("Se libera el lock {0}", service.Id));
                    _lockSubcriptorsList.Exit();
                }
            }

            _gatewaysRepository.AddSender(type, service, sender);
        }

        /// <summary>
        ///     Unsubscribes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="service"></param>
        /// <param name="sender">The sender.</param>
        public void Unsubscribe(SubscriptionKey type, Identification service, IOutputGateway<byte[]> sender)
        {
            bool lockTaken = false;
            _lockSubcriptorsList.Enter(ref lockTaken);
            if (lockTaken)
            {
                try
                {
                    if (_subcriptorsList.ContainsKey(service))
                    {
                        _subcriptorsList.Remove(service);
                    }
                }
                catch (Exception exception)
                {
                    LoggerManager.Instance.Error(string.Format("Error al añadir suscriptor {0}", service.Id), exception);
                }
                finally
                {
                    LoggerManager.Instance.Info(string.Format("Se libera el lock {0}", service.Id));
                    _lockSubcriptorsList.Exit();
                }
            }

            _gatewaysRepository.RemoveSender(type, service, sender);
        }

        /// <summary>
        ///     Sends the specified message bus.
        /// </summary>
        /// <param name="messageBus">The message bus.</param>
        public void Publish(MessageBus messageBus)
        {
            byte[] serializedMessage = Encoding.UTF8.GetBytes(_dataContractSerializer.Serialize(messageBus));
            //TODO: Crear la serializacion del message bus
            Publish(messageBus.Header.BodyType, messageBus.Header.Priority, serializedMessage);
        }

        /// <summary>
        ///     Sends the specified message bus.
        /// </summary>
        /// <param name="routingKey">The routing key.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="serializedMessage">The serialized message.</param>
        public void Publish(string routingKey, int priority, byte[] serializedMessage)
        {
            if (string.IsNullOrEmpty(routingKey))
            {
                return;
            }

            IEnumerable<IOutputGateway<byte[]>> messageSenders = _gatewaysRepository.GetMessageSenders(routingKey);
            int size = 0;

            foreach (var messageSender in messageSenders)
            {
                if (messageSender != null)
                {
                    LoggerManager.Instance.Debug(string.Format("Publicando {0} en {1}", routingKey,
                                                               messageSender.EndPoint.Uri));
                    size += messageSender.Send(serializedMessage, priority);
                }
                else
                {
                    LoggerManager.Instance.Warn(string.Format("Publicando {0} No hay suscriptores", routingKey));
                }
            }

            InvokeSentMessage(routingKey, size);
        }

        /// <summary>
        ///     Replies the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="serializedMessage">The serialized message.</param>
        public void Reply(Identification service, int priority, byte[] serializedMessage)
        {
            if (_subcriptorsList.ContainsKey(service))
            {
                _subcriptorsList[service].Send(serializedMessage, priority);
            }
        }

        /// <summary>
        ///     Gets the message types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubscriptionKey> GetMessageTypes()
        {
            return _gatewaysRepository.GetMessageTypes();
        }

        /// <summary>
        ///     Occurs when [sent message].
        /// </summary>
        public event MessageGatewayEventHandler SentMessage;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        ///     Invokes the sent message.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="size">The size.</param>
        protected void InvokeSentMessage(string type, int size)
        {
            MessageGatewayEventHandler handler = SentMessage;
            if (handler != null)
                handler(this, new MessageGatewayEventHandlerArgs
                    {
                        MessageType = type,
                        Size = size
                    });
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _gatewaysRepository.Dispose();
            }
        }
    }
}