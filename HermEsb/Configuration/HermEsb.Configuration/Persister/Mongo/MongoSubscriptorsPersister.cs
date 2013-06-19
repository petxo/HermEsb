using System;
using System.Collections.Generic;
using System.Linq;
using BteamMongoDB.Repository;
using HermEsb.Core;
using HermEsb.Core.Processors.Router.Subscriptors;
using HermEsb.Core.Processors.Router.Subscriptors.Persisters;
using MongoDB.Bson;

namespace HermEsb.Configuration.Persister.Mongo
{
    public class MongoSubscriptorsPersister : ISubscriptorsPersister
    {
        private readonly IRepository<SubscriptorEntity, ObjectId> _repository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MongoSubscriptorsPersister" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        internal MongoSubscriptorsPersister(IRepository<SubscriptorEntity, ObjectId> repository)
        {
            _repository = repository;
        }

        /// <summary>
        ///     Adds the specified subscriptor.
        /// </summary>
        /// <param name="subscriptor">The subscriptor.</param>
        public void Add(Subscriptor subscriptor)
        {
            SubscriptorEntity subscriptorEntity =
                _repository.FindOne(
                    s => s.Service.Id == subscriptor.Service.Id && s.Service.Type == subscriptor.Service.Type) ??
                new SubscriptorEntity();
            subscriptorEntity.From(subscriptor);
            _repository.Save(subscriptorEntity);
        }

        /// <summary>
        ///     Removes the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        public void Remove(Identification service)
        {
            SubscriptorEntity subscriptorEntity =
                _repository.FindOne(s => s.Service.Id == service.Id && s.Service.Type == service.Type);
            if (subscriptorEntity != null)
            {
                _repository.Remove(subscriptorEntity);
            }
        }

        /// <summary>
        ///     Gets the subscriptors.
        /// </summary>
        /// <returns></returns>
        public IList<Subscriptor> GetSubscriptors(Identification identification)
        {
            IEnumerable<SubscriptorEntity> subscriptorEntities = _repository.Find(new {});

            return subscriptorEntities.Select(s => s.ToSubscriptor(identification)).ToList();
        }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        public void Clear()
        {
            _repository.Remove(new {});
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
        }
    }
}