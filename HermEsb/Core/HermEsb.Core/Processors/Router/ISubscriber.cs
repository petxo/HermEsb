using HermEsb.Core.Gateways;
using HermEsb.Core.Processors.Router.Subscriptors;

namespace HermEsb.Core.Processors.Router
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISubscriber
    {
        /// <summary>
        /// Subscribes the specified message type.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="service"></param>
        /// <param name="outputGateway">The message sender.</param>
        void Subscribe(SubscriptionKey messageType, Identification service, IOutputGateway<byte[]> outputGateway);

        /// <summary>
        /// Unsubscribes the specified message type.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="service"></param>
        /// <param name="outputGateway">The message sender.</param>
        void Unsubscribe(SubscriptionKey messageType, Identification service, IOutputGateway<byte[]> outputGateway);
    }
}