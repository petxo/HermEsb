using HermEsb.Core.Messages.Monitoring;

namespace HermEsb.Monitoring.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProcessingVelocityMessage : IMonitoringMessage
    {
        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        /// <value>Velocity: Number of messages processed per second</value>
        float Velocity { get; set; }

        /// <summary>
        /// Gets or sets the number of messages processed.
        /// </summary>
        /// <value>The number of messages processed.</value>
        int NumberMessagesProcessed { get; set; }

        /// <summary>
        /// Gets or sets the latency.
        /// </summary>
        /// <value>The latency.</value>
        float Latency { get; set; }

        /// <summary>
        /// Gets or sets the peak max latency.
        /// </summary>
        /// <value>The peak max latency.</value>
        float PeakMaxLatency { get; set; }

        /// <summary>
        /// Gets or sets the peak min latency.
        /// </summary>
        /// <value>The peak min latency.</value>
        float PeakMinLatency { get; set; }
    }
}