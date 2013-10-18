using System;
using HermEsb.Core.Communication;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Messages;
using HermEsb.Core.Monitoring;

namespace HermEsb.Core.Gateways
{
    /// <summary>
    /// Functionality present at a receiving endpoint
    /// </summary>
    public interface IInputGateway<TMessage, THeader> : IStartable<EndPointStatus>, IDisposable, IReceiver, IMonitorableReceiverGateway
    {
        /// <summary>
        /// Occurs when a message is retrieved form the associated queue.
        /// </summary>
        event OutputGatewayEventHandler<TMessage, THeader> OnMessage;

        /// <summary>
        /// Purges this instance.
        /// </summary>
        void Purge();

        /// <summary>
        /// Reinjects the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="header"></param>
        void Reinject(IMessage message, MessageHeader header);
    }
}
