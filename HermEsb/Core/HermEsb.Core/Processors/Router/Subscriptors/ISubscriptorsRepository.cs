using System;
using System.Collections.Generic;

namespace HermEsb.Core.Processors.Router.Subscriptors
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISubscriptorsRepository : IDisposable
    {
        /// <summary>
        /// Subscribes the specified type.
        /// </summary>
        /// <param name="subscriptor">The subscriptor.</param>
        void Add(Subscriptor subscriptor);

        /// <summary>
        /// Unsubscribes the specified type.
        /// </summary>
        /// <param name="subscriptor">The subscriptor.</param>
        void Remove(Subscriptor subscriptor);

        /// <summary>
        /// Gets the specified identification.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <returns></returns>
        Subscriptor Get(Identification identification);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Subscriptor> GetAll();

        /// <summary>
        /// Determines whether [contains] [the specified identification].
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <returns>
        /// 	<c>true</c> if [contains] [the specified identification]; otherwise, <c>false</c>.
        /// </returns>
        bool Contains(Identification identification);
    }
}