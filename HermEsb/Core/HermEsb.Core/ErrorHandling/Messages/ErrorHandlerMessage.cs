using System.Runtime.Serialization;
using HermEsb.Core.Messages;

namespace HermEsb.Core.ErrorHandling.Messages
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ErrorHandlerMessage : ErrorMessage, IErrorHandlerMessage
    {
        /// <summary>
        /// Gets or sets the type of the handler.
        /// </summary>
        /// <value>The type of the handler.</value>
        [DataMember]
        public string HandlerType { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        [DataMember]
        public MessageHeader Header { get; set; }

        /// <summary>
        /// Gets or sets the message bus.
        /// </summary>
        /// <value>
        /// The message bus.
        /// </value>
        [DataMember]
        public byte[] MessageBus { get; set; }
    }
}