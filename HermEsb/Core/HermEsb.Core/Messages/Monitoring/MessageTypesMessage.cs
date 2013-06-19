using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HermEsb.Core.Messages.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class MessageTypesMessage : MonitoringMessage, IMessageTypesMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTypesMessage"/> class.
        /// </summary>
        public MessageTypesMessage()
        {
            MessageTypes = new List<MessageType>();
        }
        
        /// <summary>
        /// Gets or sets the message types.
        /// </summary>
        /// <value>The message types.</value>
        [DataMember]
        public IList<MessageType> MessageTypes { get; set; }
    }
}