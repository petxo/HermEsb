namespace HermEsb.Monitoring.Entities
{
    public class TransferVelocityEntity : MonitoringEntity
    {
        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>The input.</value>
        public VelocityEntity Input { get; set; }

        /// <summary>
        /// Gets or sets the output.
        /// </summary>
        /// <value>The output.</value>
        public VelocityEntity Output { get; set; }
    }
}