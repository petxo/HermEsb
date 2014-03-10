using System;
using HermEsb.Core;
using HermEsb.Core.Messages.Monitoring;
using MongoDB.Bson;
using Mrwesb.Data.Mongo;

namespace HermEsb.Monitoring.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class MonitoringEntity : AbstractMongoEntity<ObjectId>
    {
        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        /// <value>The identification.</value>
        public Identification Identification { get; set; }

        /// <summary>
        /// Gets or sets the UTC time taken sample.
        /// </summary>
        /// <value>The UTC time taken sample.</value>
        public DateTime UtcTimeTakenSample { get; set; }

        /// <summary>
        /// Gets or sets the bus identification.
        /// </summary>
        /// <value>The bus identification.</value>
        public Identification BusIdentification { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public ProcessorType Type { get; set; }
    }
}