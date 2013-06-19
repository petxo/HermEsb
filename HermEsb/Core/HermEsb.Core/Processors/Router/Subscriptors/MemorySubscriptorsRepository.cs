using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace HermEsb.Core.Processors.Router.Subscriptors
{
    /// <summary>
    /// 
    /// </summary>
    public class MemorySubscriptorsRepository : ISubscriptorsRepository
    {
        private readonly ConcurrentDictionary<Identification, Subscriptor> _subscriptors;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemorySubscriptorsRepository"/> class.
        /// </summary>
        public MemorySubscriptorsRepository()
        {
            _subscriptors = new ConcurrentDictionary<Identification, Subscriptor>();
        }

        /// <summary>
        /// Subscribes the specified type.
        /// </summary>
        /// <param name="subscriptor">The subscriptor.</param>
        public void Add(Subscriptor subscriptor)
        {
            _subscriptors.GetOrAdd(subscriptor.Service, subscriptor);
        }

        /// <summary>
        /// Unsubscribes the specified type.
        /// </summary>
        /// <param name="subscriptor">The subscriptor.</param>
        public void Remove(Subscriptor subscriptor)
        {
            if (_subscriptors.ContainsKey(subscriptor.Service))
            {
                Subscriptor subs;
                if (_subscriptors.TryRemove(subscriptor.Service, out subs))
                {
                    subs.Dispose();
                    //_subscriptors.Keys.Remove(subscriptor.Service); Collection Read-Only
                }
            }
        }

        /// <summary>
        /// Gets the specified identification.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <returns></returns>
        public Subscriptor Get(Identification identification)
        {
            Subscriptor subscriptor = default(Subscriptor);
            _subscriptors.TryGetValue(identification,  out subscriptor);

            return subscriptor;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Subscriptor> GetAll()
        {
            return _subscriptors.Select(keyvalue => keyvalue.Value);
        }

        /// <summary>
        /// Determines whether [contains] [the specified identification].
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <returns>
        /// 	<c>true</c> if [contains] [the specified identification]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(Identification identification)
        {
            return _subscriptors.ContainsKey(identification);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var keyvalue in _subscriptors)
                {
                    keyvalue.Value.Dispose();
                }
            }
        }
    }
}
