using System.Collections.Generic;
using HermEsb.Core.Messages.Monitoring;

namespace HermEsb.Monitoring.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class OutputTypesMessage: MonitoringMessage, IOutputTypesMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTypesMessage"/> class.
        /// </summary>
        public OutputTypesMessage()
        {
            MessageTypes = new List<MessageType>();
        }
        
        /// <summary>
        /// Gets or sets the message types.
        /// </summary>
        /// <value>The message types.</value>
        public IList<MessageType> MessageTypes { get; set; }
    }
}