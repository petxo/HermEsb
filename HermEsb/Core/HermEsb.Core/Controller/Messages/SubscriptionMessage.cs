using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HermEsb.Core.Controller.Messages
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]

    public class SubscriptionMessage : ISubscriptionMessage
    {
        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>The service.</value>

        public Identification Service { get; set; }

        /// <summary>
        /// Gets or sets the types.
        /// </summary>
        /// <value>The types.</value>

        public IList<SubscriptionKeyMessage> Types { get; set; }

        /// <summary>
        /// Gets or sets the input gateway.
        /// </summary>
        /// <value>The input gateway.</value>

        public EndPointMessage InputGateway { get; set; }

        /// <summary>
        /// Gets or sets the input control queue.
        /// </summary>
        /// <value>The input control queue.</value>

        public EndPointMessage InputControlGateway { get; set; }
    }
}