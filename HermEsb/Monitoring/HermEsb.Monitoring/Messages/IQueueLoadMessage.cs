using HermEsb.Core.Messages.Monitoring;

namespace HermEsb.Monitoring.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public interface IQueueLoadMessage : IMonitoringMessage
    {
        /// <summary>
        /// Gets or sets the total messages.
        /// </summary>
        /// <value>The total messages.</value>
        int TotalMessages { get; set; }
    }
}