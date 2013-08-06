using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Control;

namespace HermEsb.Core.Controller.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAddSubscriberFromClusterMessage : IControlMessage
    {
        /// <summary>
        /// Gets or sets the trigger.
        /// </summary>
        /// <value>
        /// The trigger.
        /// </value>
        Identification Trigger { get; set; }


        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        SubscriptionMessage SubscriberService { get; set; }
    }
}