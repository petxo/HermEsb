using System.Collections.Generic;
using BteamMongoDB;
using HermEsb.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HermEsb.Configuration.Persister.Mongo
{
    public class SubscriptorEntity : IMongoEntity<ObjectId>
    {
        /// <summary>
        ///     Gets or sets the service.
        /// </summary>
        /// <value>The service.</value>
        public Identification Service { get; set; }

        /// <summary>
        ///     Gets or sets the types.
        /// </summary>
        /// <value>The types.</value>
        public IList<SubscriptionType> SubcriptionTypes { get; set; }

        /// <summary>
        ///     Gets or sets the input gateway.
        /// </summary>
        /// <value>The input gateway.</value>
        public GatewayEntity InputGateway { get; set; }

        /// <summary>
        ///     Gets or sets the input control queue.
        /// </summary>
        /// <value>The input control queue.</value>
        public GatewayEntity InputControlGateway { get; set; }

        [BsonId]
        public ObjectId Id { get; set; }
    }
}