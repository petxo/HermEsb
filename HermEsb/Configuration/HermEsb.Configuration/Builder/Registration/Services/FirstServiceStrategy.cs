using System;
using System.Collections.Generic;
using System.Linq;

namespace HermEsb.Configuration.Builder.Registration.Services
{
    /// <summary>
    /// 
    /// </summary>
    internal class FirstServiceStrategy : IServiceStrategy
    {
        /// <summary>
        /// Gets the interface.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public Type GetInterface(Type type)
        {
            var interfaces = type.GetInterfaces().ToList();

            var baseInterfaces = GetBaseInterfaces(type);
            interfaces = interfaces.Except(baseInterfaces).ToList();

            return interfaces.Any() ? interfaces.First() : type;
        }

        private static IEnumerable<Type> GetBaseInterfaces(Type type)
        {
            var baseInterfaces = new List<Type>();
            if (type.BaseType != null)
            {
                var interfaces = type.BaseType.GetInterfaces();
                if (interfaces .Any())
                    baseInterfaces.AddRange(interfaces.ToList());
            }
            return baseInterfaces;
        }
    }
}