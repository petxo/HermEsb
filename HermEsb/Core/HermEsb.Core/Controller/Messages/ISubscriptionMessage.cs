using System.Collections.Generic;
using HermEsb.Core.Messages.Control;

namespace HermEsb.Core.Controller.Messages
{
    public interface ISubscriptionMessage : IControlMessage
    {
        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>The service.</value>
        Identification Service { get; set; }

        /// <summary>
        /// Gets or sets the types.
        /// </summary>
        /// <value>The types.</value>
        IList<SubscriptionKeyMessage> Types { get; set; }

        /// <summary>
        /// Gets or sets the input gateway.
        /// </summary>
        /// <value>The input gateway.</value>
        EndPointMessage InputGateway { get; set; }

        /// <summary>
        /// Gets or sets the input control queue.
        /// </summary>
        /// <value>The input control queue.</value>
        EndPointMessage InputControlGateway { get; set; }
    }
}