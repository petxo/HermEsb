using System;
using Bteam.Specifications;

namespace HermEsb.Configuration.Builder.Registration.Specifications
{
    public class TypeInNamespaceSpec : ISpecification<Type>
    {
        private readonly string _namespace;

        internal TypeInNamespaceSpec(string ns)
        {
            _namespace = ns;
        }

        /// <summary>
        ///     Determines whether [is satisfied by] [the specified candidate].
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns>
        ///     <c>true</c> if [is satisfied by] [the specified candidate]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSatisfiedBy(Type candidate)
        {
            return candidate.Namespace == _namespace;
        }
    }
}