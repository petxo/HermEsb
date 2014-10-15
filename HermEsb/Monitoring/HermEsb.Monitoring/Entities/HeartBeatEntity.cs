using HermEsb.Core.Processors;

namespace HermEsb.Monitoring.Entities
{
    public class HeartBeatEntity : MonitoringEntity 
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public ProcessorStatus Status { get; set; }
    }
}