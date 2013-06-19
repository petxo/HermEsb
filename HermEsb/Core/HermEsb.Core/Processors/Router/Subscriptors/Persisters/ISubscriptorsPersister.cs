using System;
using System.Collections.Generic;

namespace HermEsb.Core.Processors.Router.Subscriptors.Persisters
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISubscriptorsPersister : IDisposable
    {
        /// <summary>
        /// Adds the specified subscriptor.
        /// </summary>
        /// <param name="subscriptor">The subscriptor.</param>
        void Add(Subscriptor subscriptor);

        /// <summary>
        /// Removes the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        void Remove(Identification service);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets the subscriptors.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <returns></returns>
        IList<Subscriptor> GetSubscriptors(Identification identification);
    }
}