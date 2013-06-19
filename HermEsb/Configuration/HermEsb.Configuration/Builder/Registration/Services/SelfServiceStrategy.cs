using System;

namespace HermEsb.Configuration.Builder.Registration.Services
{
    /// <summary>
    /// 
    /// </summary>
    internal class SelfServiceStrategy : IServiceStrategy
    {
        /// <summary>
        /// Gets the interface.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public Type GetInterface(Type type)
        {
            return type;
        }
    }
}