using System;
using System.Runtime.Serialization;

namespace HermEsb.Core.Messages.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMonitoringMessage : IMessage
    {
        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        /// <value>The identification.</value>
        [DataMember]
        Identification Identification { get; set; }

        /// <summary>
        /// Gets or sets the UTC time taken sample.
        /// </summary>
        /// <value>The UTC time taken sample.</value>
        [DataMember]
        DateTime UtcTimeTakenSample { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [DataMember]
        ProcessorType Type { get; set; }

        /// <summary>
        /// Gets or sets the bus identification.
        /// </summary>
        /// <value>The bus identification.</value>
        [DataMember]
        Identification BusIdentification { get; set; }
    }
}