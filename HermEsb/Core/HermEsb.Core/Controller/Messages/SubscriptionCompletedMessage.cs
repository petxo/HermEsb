using System.Runtime.Serialization;

namespace HermEsb.Core.Controller.Messages
{
    /// <summary>
    /// 
    /// </summary>

    [KnownType(typeof(Identification))]
    public class SubscriptionCompletedMessage : ISubscriptionCompletedMessage
    {
        /// <summary>
        /// Gets or sets the input gateway.
        /// </summary>
        /// <value>The input gateway.</value>

        public EndPointMessage InputGateway { get; set; }


        /// <summary>
        /// Gets or sets the bus identification.
        /// </summary>
        /// <value>The bus identification.</value>

        public Identification BusIdentification { get; set; }
    }
}