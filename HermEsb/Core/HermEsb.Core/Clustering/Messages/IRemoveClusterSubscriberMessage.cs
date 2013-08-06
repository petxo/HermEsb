using HermEsb.Core.Messages;

namespace HermEsb.Core.Clustering.Messages
{
    public interface IRemoveClusterSubscriberMessage : IMessage
    {
        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        /// <value>
        /// The identification.
        /// </value>
        Identification Identification { get; set; }

        /// <summary>
        /// Gets or sets the service id.
        /// </summary>
        /// <value>
        /// The service id.
        /// </value>
        Identification ServiceId { get; set; }
    }
}