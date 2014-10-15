using System;
using System.Collections.Generic;
using BteamMongoDB.Repository;
using HermEsb.Core;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Wrappers;
using Mrwesb.Data.Mongo;

namespace HermEsb.Monitoring.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class MonitoringRepository<TEntity> : Repository<TEntity, ObjectId>, IMonitoringRepository<TEntity>
        where TEntity : AbstractMongoEntity<ObjectId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonitoringRepository&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="connectionName">Name of the connection.</param>
        public MonitoringRepository(string connectionName) : base(connectionName) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitoringRepository&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="connectionName">Name of the connection.</param>
        /// <param name="collection">The collection.</param>
        public MonitoringRepository(string connectionName, string collection) : base(connectionName, collection) { }

        /// <summary>
        /// Finds the by date.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="first">The first.</param>
        /// <param name="last">The last.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindByDate(Identification identification, DateTime first, DateTime last)
        {
            var firstDate = first.ToString("yyyy/MM/dd HH:mm:ss");
            var lastDate = last.ToString("yyyy/MM/dd HH:mm:ss");

            var query = Query.And(
                QueryWrapper.Create(new { Identification = identification }),
                Query.LT("UtcTimeTakenSample", lastDate),
                Query.GT("UtcTimeTakenSample", firstDate)
                );

            return _mongoHelper.Repository.GetCollection<TEntity>(_collection).Find(query);

            //return Find(new { Identification = identification, UtcTimeTakenSample = Q.LessThan(lastDate).And(Q.GreaterThan(firstDate)) });
        }
    }
}