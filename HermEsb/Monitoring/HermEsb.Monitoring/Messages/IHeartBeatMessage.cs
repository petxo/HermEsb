using HermEsb.Core.Messages.Monitoring;
using HermEsb.Core.Processors;

namespace HermEsb.Monitoring.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHeartBeatMessage : IMonitoringMessage
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        ProcessorStatus Status { get; set; }
    }
}