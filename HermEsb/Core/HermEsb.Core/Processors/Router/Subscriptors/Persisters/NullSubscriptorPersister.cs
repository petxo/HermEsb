using System.Collections.Generic;

namespace HermEsb.Core.Processors.Router.Subscriptors.Persisters
{
    /// <summary>
    /// 
    /// </summary>
    public class NullSubscriptorPersister : ISubscriptorsPersister
    {
        /// <summary>
        /// Adds the specified subscriptor.
        /// </summary>
        /// <param name="subscriptor">The subscriptor.</param>
        public void Add(Subscriptor subscriptor)
        {
            
        }

        /// <summary>
        /// Removes the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        public void Remove(Identification service)
        {
            
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            
        }

        /// <summary>
        /// Gets the subscriptors.
        /// </summary>
        /// <returns></returns>
        public IList<Subscriptor> GetSubscriptors(Identification identification)
        {
            return new List<Subscriptor>();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            
        }
    }
}