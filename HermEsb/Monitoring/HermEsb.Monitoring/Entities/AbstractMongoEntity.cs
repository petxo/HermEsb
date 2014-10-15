using BteamMongoDB;

namespace Mrwesb.Data.Mongo
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractMongoEntity<TId> : IMongoEntity<TId>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [BsonId]
        public TId Id { get; set; }
    }
}