using System;
using HermEsb.Core.Messages;

namespace HermEsb.Core.ErrorHandling
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorOnHandlersEventHandlerArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the type of the handler.
        /// </summary>
        /// <value>The type of the handler.</value>
        public Type HandlerType { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the message bus.
        /// </summary>
        /// <value>
        /// The message bus.
        /// </value>
        public byte[] MessageBus { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        public MessageHeader Header { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; set; }
    }
}