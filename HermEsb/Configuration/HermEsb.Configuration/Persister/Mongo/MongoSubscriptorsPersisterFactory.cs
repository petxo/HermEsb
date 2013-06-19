using System;
using BteamMongoDB;
using BteamMongoDB.Repository;
using HermEsb.Core.Processors.Router.Subscriptors.Persisters;
using HermEsb.Logging;
using MongoDB.Bson;

namespace HermEsb.Configuration.Persister.Mongo
{
    /// <summary>
    /// </summary>
    public static class MongoSubscriptorsPersisterFactory
    {
        /// <summary>
        ///     Creates the specified connection name.
        /// </summary>
        /// <param name="mongoSettings">The mongo settings.</param>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static ISubscriptorsPersister Create(MongoSettingsExtended mongoSettings, string collection)
        {
            try
            {
                MongoHelperProvider.Instance.AddConnection("SubscriptorsEntities", mongoSettings);
                var repository = new Repository<SubscriptorEntity, ObjectId>("SubscriptorsEntities", collection);
                return new MongoSubscriptorsPersister(repository);
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error("Error Create Mongo Subscriptor Persister", ex);
                throw;
            }
        }
    }
}