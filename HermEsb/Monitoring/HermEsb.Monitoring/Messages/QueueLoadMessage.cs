using HermEsb.Core.Messages.Monitoring;

namespace HermEsb.Monitoring.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class QueueLoadMessage : MonitoringMessage, IQueueLoadMessage
    {
        /// <summary>
        /// Gets or sets the total messages.
        /// </summary>
        /// <value>The total messages.</value>
        public int TotalMessages { get; set; }
    }
}