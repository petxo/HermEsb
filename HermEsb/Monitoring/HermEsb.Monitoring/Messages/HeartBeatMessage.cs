using HermEsb.Core.Messages.Monitoring;
using HermEsb.Core.Processors;

namespace HermEsb.Monitoring.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class HeartBeatMessage : MonitoringMessage, IHeartBeatMessage
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public ProcessorStatus Status { get; set; }


    }
}