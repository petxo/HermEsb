using System;
using Bteam.Specifications;
using HermEsb.Core;

namespace HermEsb.Monitoring.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentitySpec : ISpecification<Identification>
    {
        /// <summary>
        /// Initializes the <see cref="IdentitySpec"/> class.
        /// </summary>
        static IdentitySpec()
        {
            Instance = new IdentitySpec();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentitySpec"/> class.
        /// </summary>
        private IdentitySpec()
        {
            
        }

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static IdentitySpec Instance { get; private set; }

        public bool IsSatisfiedBy(Identification spec)
        {
            return (spec != null && !String.IsNullOrWhiteSpace(spec.Id) && !String.IsNullOrWhiteSpace(spec.Type));
        }
    }
}