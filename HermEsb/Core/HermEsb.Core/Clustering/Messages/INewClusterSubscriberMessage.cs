using HermEsb.Core.Controller.Messages;
using HermEsb.Core.Messages;

namespace HermEsb.Core.Clustering.Messages
{
    public interface INewClusterSubscriberMessage : IMessage
    {
        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        /// <value>
        /// The identification.
        /// </value>
        Identification Identification { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        SubscriptionMessage Service { get; set; }
    }
}