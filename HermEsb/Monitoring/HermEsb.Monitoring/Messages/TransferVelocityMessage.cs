using HermEsb.Core.Messages.Monitoring;

namespace HermEsb.Monitoring.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class TransferVelocityMessage : MonitoringMessage, ITransferVelocityMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransferVelocityMessage"/> class.
        /// </summary>
        public TransferVelocityMessage()
        {
            Input = new VelocityMessage();
            Output = new VelocityMessage();
        }

        /// <summary>
        /// Gets or sets the input velocity.
        /// </summary>
        /// <value>Velocity: Bytes processed per second</value>
        public VelocityMessage Input { get; set; }


        /// <summary>
        /// Gets or sets the output velocity.
        /// </summary>
        /// <value>Velocity: Bytes processed per second</value>
        public VelocityMessage Output { get; set; }
    }
}