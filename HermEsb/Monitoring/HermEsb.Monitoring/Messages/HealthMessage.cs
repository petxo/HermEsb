using HermEsb.Core.Messages.Monitoring;

namespace HermEsb.Monitoring.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class HealthMessage : MonitoringMessage, IHealthMessage
    {
        /// <summary>
        /// Gets or sets the memory working set.
        /// </summary>
        /// <value>The memory working set.</value>
        public long MemoryWorkingSet { get; set; }
    }
}