using Bteam.Specifications;
using HermEsb.Core.Messages.Monitoring;

namespace HermEsb.Monitoring.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    public class BusTypeSpec : ISpecification<ProcessorType>
    {
        /// <summary>
        /// Initializes the <see cref="BusTypeSpec"/> class.
        /// </summary>
        static BusTypeSpec()
        {
            Instance = new BusTypeSpec();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusTypeSpec"/> class.
        /// </summary>
        private BusTypeSpec()
        {
            
        }

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static BusTypeSpec Instance { get; private set; }

        public bool IsSatisfiedBy(ProcessorType spec)
        {
            return (spec == ProcessorType.Bus);
        }
    }
}