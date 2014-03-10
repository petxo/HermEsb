namespace HermEsb.Monitoring.Translators
{
    /// <summary>
    /// A generic definition of a translator between a source and a destination classes
    /// </summary>
    public interface ITranslator
    {
        /// <summary>
        /// Translates the specified message.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        TDestination Translate<TSource, TDestination>(TSource source);

        /// <summary>
        /// Translates the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        void Translate<TSource, TDestination>(TSource source, TDestination destination);
    }
}