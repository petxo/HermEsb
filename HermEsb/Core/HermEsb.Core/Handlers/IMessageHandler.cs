using System;
using HermEsb.Core.Messages;

namespace HermEsb.Core.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public interface IMessageHandler<in TMessage> : IDisposable where TMessage : IMessage
    {
        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void HandleMessage(TMessage message);
    }
}