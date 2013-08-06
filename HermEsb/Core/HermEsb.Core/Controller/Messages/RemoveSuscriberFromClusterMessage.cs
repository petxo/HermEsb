namespace HermEsb.Core.Controller.Messages
{
    public class RemoveSubscriberFromClusterMessage : IRemoveSubscriberFromClusterMessage
    {
        /// <summary>
        /// Gets or sets the trigger.
        /// </summary>
        /// <value>
        /// The trigger.
        /// </value>
        public Identification Trigger { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public Identification Service { get; set; }
    }
}