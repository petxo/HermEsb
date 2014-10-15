using AutoMapper;

namespace HermEsb.Monitoring.Translators
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseTranslator : ITranslator
    {
        /// <summary>
        /// Translates the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public TDestination Translate<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source); 
        }

        /// <summary> 
        /// Translates the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public void Translate<TSource, TDestination>(TSource source, TDestination destination)
        {
            Mapper.Map(source, destination); 
        }


    }
}