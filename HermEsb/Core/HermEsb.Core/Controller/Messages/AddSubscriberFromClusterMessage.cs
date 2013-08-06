namespace HermEsb.Core.Controller.Messages
{
    public class AddSubscriberFromClusterMessage : IAddSubscriberFromClusterMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddSubscriberFromClusterMessage"/> class.
        /// </summary>
        public AddSubscriberFromClusterMessage()
        {
            SubscriberService = new SubscriptionMessage();
        }

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
        public SubscriptionMessage SubscriberService { get; set; }
    }
}