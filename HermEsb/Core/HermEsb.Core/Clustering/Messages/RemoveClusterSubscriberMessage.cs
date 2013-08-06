namespace HermEsb.Core.Clustering.Messages
{
    public class RemoveClusterSubscriberMessage : IRemoveClusterSubscriberMessage
    {
        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        /// <value>
        /// The identification.
        /// </value>
        public Identification Identification { get; set; }

        /// <summary>
        /// Gets or sets the service id.
        /// </summary>
        /// <value>
        /// The service id.
        /// </value>
        public Identification ServiceId { get; set; }
    }
}