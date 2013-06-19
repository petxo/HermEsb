using System;
using System.Runtime.Serialization;
using HermEsb.Core.Communication;

namespace HermEsb.Core.Controller.Messages
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DataContract]
    public class EndPointMessage
    {
        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>The URI.</value>
        [DataMember]
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [DataMember]
        public TransportType Type { get; set; }
    }
}