using System.Runtime.Serialization;
using HermEsb.Core.Messages.Control;

namespace HermEsb.Core.Controller.Messages
{
    public interface ISubscriptionCompletedMessage : IControlMessage
    {
        /// <summary>
        /// Gets or sets the input gateway.
        /// </summary>
        /// <value>The input gateway.</value>
        [DataMember]
        EndPointMessage InputGateway { get; set; }

        /// <summary>
        /// Gets or sets the bus identification.
        /// </summary>
        /// <value>The bus identification.</value>
        [DataMember]
        Identification BusIdentification { get; set; }
    }
}