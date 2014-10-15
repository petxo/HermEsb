using System;
using HermEsb.Core;
using HermEsb.Core.ErrorHandling.Messages;
using HermEsb.Core.Messages;
using MongoDB.Bson;
using Mrwesb.Data.Mongo;

namespace HermEsb.Monitoring.Entities
{
    public class ErrorHandlerEntity : AbstractMongoEntity<ObjectId>
    {
        /// <summary>
        /// Gets or sets the service id.
        /// </summary>
        /// <value>The service id.</value>
        public Identification ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the UTC success at.
        /// </summary>
        /// <value>The UTC success at.</value>
        public DateTime UtcSuccessAt { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public ExceptionMessage Exception { get; set; }

        /// <summary>
        /// Gets or sets the type of the handler.
        /// </summary>
        /// <value>The type of the handler.</value>
        public string HandlerType { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the message bus.
        /// </summary>
        /// <value>
        /// The message bus.
        /// </value>
        public byte[] MessageBus { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        public MessageHeader Header { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public StatusError Status { get; set; }

        /// <summary>
        /// Gets or sets the solved at.
        /// </summary>
        /// <value>
        /// The solved at.
        /// </value>
        public DateTime ResolvedAt { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum StatusError
    {
        /// <summary>
        /// The open
        /// </summary>
        Open,

        /// <summary>
        /// The solved
        /// </summary>
        Resolved,

        /// <summary>
        /// The rejected
        /// </summary>
        Rejected
    }
}