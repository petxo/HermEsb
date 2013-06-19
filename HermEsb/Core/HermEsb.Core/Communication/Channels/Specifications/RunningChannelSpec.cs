using Bteam.Specifications;
using HermEsb.Core.Communication.EndPoints;

namespace HermEsb.Core.Communication.Channels.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    public class RunningChannelSpec : ISpecification<IReceiverChannel>
    {
        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static RunningChannelSpec Instance { get; private set; }

        /// <summary>
        /// Initializes the <see cref="RunningChannelSpec"/> class.
        /// </summary>
        static RunningChannelSpec()
        {
            Instance = new RunningChannelSpec();
        }

        private RunningChannelSpec()
        {
            
        }

        /// <summary>
        /// Determines whether [is satisfied by] [the specified candidate].
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns>
        /// 	<c>true</c> if [is satisfied by] [the specified candidate]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSatisfiedBy(IReceiverChannel candidate)
        {
            return candidate.Status == EndPointStatus.Receiving;
        }
    }
}