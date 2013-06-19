using System.Runtime.Serialization;

namespace HermEsb.Core.Controller.Messages
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class UnsubscriptionMessage : IUnsubscriptionMessage
    {
        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>The service.</value>
        [DataMember]
        public Identification Service { get; set; }
    }
}