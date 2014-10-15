using HermEsb.Core.Messages.Monitoring;

namespace HermEsb.Monitoring.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessingVelocityMessage : MonitoringMessage, IProcessingVelocityMessage
    {
        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        /// <value>Velocity: Number of messages processed per second</value>
        public float Velocity { get; set; }

        /// <summary>
        /// Gets or sets the number of messages processed.
        /// </summary>
        /// <value>The number of messages processed.</value>
        public int NumberMessagesProcessed { get; set; }

        /// <summary>
        /// Gets or sets the latency.
        /// </summary>
        /// <value>The latency.</value>
        public float Latency { get; set; }

        /// <summary>
        /// Gets or sets the peak max latency.
        /// </summary>
        /// <value>The peak max latency.</value>
        public float PeakMaxLatency { get; set; }

        /// <summary>
        /// Gets or sets the peak min latency.
        /// </summary>
        /// <value>The peak min latency.</value>
        public float PeakMinLatency { get; set; }

        
    }
}
