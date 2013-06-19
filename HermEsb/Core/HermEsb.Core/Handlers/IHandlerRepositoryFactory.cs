using System.Collections.Generic;

namespace HermEsb.Core.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHandlerRepositoryFactory
    {
        /// <summary>
        /// Creates the specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <returns></returns>
        HandlerRepository Create(IEnumerable<string> assemblies);
    }
}