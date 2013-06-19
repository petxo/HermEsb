using System;

namespace HermEsb.Configuration.Builder.Registration.Services
{
    internal interface IServiceStrategy
    {
        /// <summary>
        /// Gets the interface.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        Type GetInterface(Type type);
    }
}