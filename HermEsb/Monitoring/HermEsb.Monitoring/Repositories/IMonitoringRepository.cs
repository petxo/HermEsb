using System;
using System.Collections.Generic;
using BteamMongoDB.Repository;
using HermEsb.Core;
using MongoDB.Bson;
using Mrwesb.Data.Mongo;

namespace HermEsb.Monitoring.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IMonitoringRepository<TEntity> : IRepository<TEntity, ObjectId> where TEntity : AbstractMongoEntity<ObjectId>
    {
        /// <summary>
        /// Finds the by date.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="first">The first.</param>
        /// <param name="last">The last.</param>
        /// <returns></returns>
        IEnumerable<TEntity> FindByDate(Identification identification, DateTime first, DateTime last);
    }
}