using System.Runtime.Serialization;
using HermEsb.Core.Messages;

namespace HermEsb.Core.ErrorHandling.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public interface IErrorHandlerMessage : IErrorMessage
    {
        /// <summary>
        /// Gets or sets the type of the handler.
        /// </summary>
        /// <value>The type of the handler.</value>
        string HandlerType { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        string Message { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        MessageHeader Header { get; set; }

        /// <summary>
        /// Gets or sets the message bus.
        /// </summary>
        /// <value>
        /// The message bus.
        /// </value>
        byte[] MessageBus { get; set; }
    }
}