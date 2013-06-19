using System;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Builders;

namespace HermEsb.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPublisher : IDisposable
    {
        /// <summary>
        /// Publica en mensaje especificado en el Bus
        /// </summary>
        /// <param name="message">Mensaje</param>
        void Publish(IMessage message);

        /// <summary>
        /// Gets the message builder.
        /// </summary>
        /// <value>The message builder.</value>
        IMessageBuilder MessageBuilder { get; }
    }
}