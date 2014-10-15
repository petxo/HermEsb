using System;
using HermEsb.Core;
using HermEsb.Core.ErrorHandling.Messages;
using HermEsb.Core.Messages;
using MongoDB.Bson;
using Mrwesb.Data.Mongo;

namespace HermEsb.Monitoring.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorRouterEntity : AbstractMongoEntity<ObjectId>
    {
        /// <summary>
        /// Gets or sets the service id.
        /// </summary>
        /// <value>The service id.</value>
        public Identification ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the UTC time taken sample.
        /// </summary>
        /// <value>The UTC time taken sample.</value>
        public DateTime UtcSuccessAt { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public ExceptionMessage Exception { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public MessageBus Message { get; set; }
    }
}