using System;
using System.Runtime.Serialization;

namespace HermEsb.Core.Messages.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [KnownType(typeof(Identification))]
    public class MonitoringMessage : IMonitoringMessage
    {
        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        /// <value>The identification.</value>
        [DataMember]
        public Identification Identification { get; set; }

        /// <summary>
        /// Gets or sets the UTC time taken sample.
        /// </summary>
        /// <value>The UTC time taken sample.</value>
        [DataMember]
        public DateTime UtcTimeTakenSample { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [DataMember]
        public ProcessorType Type { get; set; }

        /// <summary>
        /// Gets or sets the bus identification.
        /// </summary>
        /// <value>The bus identification.</value>
        [DataMember]
        public Identification BusIdentification { get; set; }
    }
}