using HermEsb.Core.Messages.Monitoring;

namespace HermEsb.Monitoring.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHealthMessage : IMonitoringMessage
    {
        /// <summary>
        /// Gets or sets the memory working set.
        /// </summary>
        /// <value>The memory working set.</value>
        long MemoryWorkingSet { get; set; }
    }
}