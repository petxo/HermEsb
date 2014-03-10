using System.Collections.Generic;
using HermEsb.Core.Messages.Monitoring;

namespace HermEsb.Monitoring.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutputTypesMessage : IMonitoringMessage
    {
        /// <summary>
        /// Gets or sets the message types.
        /// </summary>
        /// <value>The message types.</value>
        IList<MessageType> MessageTypes { get; set; }
    }
}