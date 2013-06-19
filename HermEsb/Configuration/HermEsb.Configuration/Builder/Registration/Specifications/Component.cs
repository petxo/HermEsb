using System;
using Bteam.Specifications;

namespace HermEsb.Configuration.Builder.Registration.Specifications
{
    /// <summary>
    /// </summary>
    public static class Component
    {
        /// <summary>
        ///     Determines whether [is in namespace] [the specified ns].
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <returns></returns>
        public static ISpecification<Type> IsInNamespace(string ns)
        {
            return new TypeInNamespaceSpec(ns);
        }
    }
}