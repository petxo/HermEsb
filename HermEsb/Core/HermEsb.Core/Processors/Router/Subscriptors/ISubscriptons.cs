namespace HermEsb.Core.Processors.Router.Subscriptors
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISubscriptons
    {
        /// <summary>
        /// Subscribes the specified type.
        /// </summary>
        /// <param name="subscriptor">The subscriptor.</param>
        void Add(Subscriptor subscriptor);

        /// <summary>
        /// Unsubscribes the specified type.
        /// </summary>
        /// <param name="subscriptorIdentification">The subscriptor identification.</param>
        void Remove(Identification subscriptorIdentification);

        /// <summary>
        /// Determines whether [contains] [the specified identification].
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <returns>
        /// 	<c>true</c> if [contains] [the specified identification]; otherwise, <c>false</c>.
        /// </returns>
        bool Contains(Identification identification);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        void Refresh();

        void AddService(Identification serviceId);
    }
}