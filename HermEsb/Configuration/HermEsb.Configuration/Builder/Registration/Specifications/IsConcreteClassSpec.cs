using System;
using Bteam.Specifications;

namespace HermEsb.Configuration.Builder.Registration.Specifications
{
    /// <summary>
    /// </summary>
    public class IsConcreteClassSpec : ISpecification<Type>
    {
        /// <summary>
        ///     Determines whether [is satisfied by] [the specified candidate].
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns>
        ///     <c>true</c> if [is satisfied by] [the specified candidate]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSatisfiedBy(Type candidate)
        {
            return !candidate.IsInterface && !candidate.IsAbstract;
        }
    }
}