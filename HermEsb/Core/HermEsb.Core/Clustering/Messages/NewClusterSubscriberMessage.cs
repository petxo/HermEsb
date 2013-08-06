using HermEsb.Core.Controller.Messages;

namespace HermEsb.Core.Clustering.Messages
{
    public class NewClusterSubscriberMessage : INewClusterSubscriberMessage
    {
        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        /// <value>
        /// The identification.
        /// </value>
        public Identification Identification { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public SubscriptionMessage Service { get; set; }
    }
}