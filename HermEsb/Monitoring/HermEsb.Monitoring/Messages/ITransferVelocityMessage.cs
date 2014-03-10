using HermEsb.Core.Messages.Monitoring;

namespace HermEsb.Monitoring.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransferVelocityMessage : IMonitoringMessage
    {
        /// <summary>
        /// Gets or sets the input velocity.
        /// </summary>
        /// <value>Velocity: Bytes processed per second</value>
        VelocityMessage Input { get; set; }

        /// <summary>
        /// Gets or sets the output velocity.
        /// </summary>
        /// <value>Velocity: Bytes processed per second</value>
        VelocityMessage Output { get; set; }
    }
}