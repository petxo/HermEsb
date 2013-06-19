using System.Runtime.Serialization;

namespace HermEsb.Core.Controller.Messages
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [KnownType(typeof(Identification))]
    public class SubscriptionCompletedMessage : ISubscriptionCompletedMessage
    {
        /// <summary>
        /// Gets or sets the input gateway.
        /// </summary>
        /// <value>The input gateway.</value>
        [DataMember]
        public EndPointMessage InputGateway { get; set; }


        /// <summary>
        /// Gets or sets the bus identification.
        /// </summary>
        /// <value>The bus identification.</value>
        [DataMember]
        public Identification BusIdentification { get; set; }
    }
}