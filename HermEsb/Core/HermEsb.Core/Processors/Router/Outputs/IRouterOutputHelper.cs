using System;
using System.Collections.Generic;
using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Processors.Router.Subscriptors;

namespace HermEsb.Core.Processors.Router.Outputs
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRouterOutputHelper : IMonitorableSenderGateway, IDisposable
    {
        /// <summary>
        /// Subscribes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="service"></param>
        /// <param name="sender">The sender.</param>
        void Subscribe(SubscriptionKey type, Identification service, IOutputGateway<byte[]> sender);

        /// <summary>
        /// Unsubscribes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="service"></param>
        /// <param name="sender">The sender.</param>
        void Unsubscribe(SubscriptionKey type, Identification service, IOutputGateway<byte[]> sender);

        /// <summary>
        /// Sends the specified message bus.
        /// </summary>
        /// <param name="messageBus">The message bus.</param>
        void Publish (MessageBus messageBus);

        /// <summary>
        /// Gets the message types.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SubscriptionKey> GetMessageTypes();

        /// <summary>
        /// Sends the specified message bus.
        /// </summary>
        /// <param name="routingKey">The routing key.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="serializedMessage">The serialized message.</param>
//        void Publish(string routingKey, int priority, string serializedMessage);

        /// <summary>
        /// Publishes the specified routing key.
        /// </summary>
        /// <param name="routingKey">The routing key.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="serializedMessage">The serialized message.</param>
        void Publish(string routingKey, int priority, byte[] serializedMessage);

        /// <summary>
        /// Replies the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="serializedMessage">The serialized message.</param>
        void Reply(Identification service, int priority, byte[] serializedMessage);
    }
}