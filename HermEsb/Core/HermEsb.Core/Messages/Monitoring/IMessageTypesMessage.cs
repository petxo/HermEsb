using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HermEsb.Core.Messages.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMessageTypesMessage : IMonitoringMessage
    {
        /// <summary>
        /// Gets or sets the message types.
        /// </summary>
        /// <value>The message types.</value>
        [DataMember]
        IList<MessageType> MessageTypes { get; set; }
    }
}