using System;
using System.Runtime.Serialization;
using HermEsb.Core.Communication;

namespace HermEsb.Core.Controller.Messages
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]

    public class EndPointMessage
    {
        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>The URI.</value>

        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>

        public TransportType Type { get; set; }
    }
}