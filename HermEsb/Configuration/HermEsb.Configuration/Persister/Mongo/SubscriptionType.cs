using System.Collections.Generic;

namespace HermEsb.Configuration.Persister.Mongo
{
    public class SubscriptionType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionType"/> class.
        /// </summary>
        public SubscriptionType()
        {
            ParentKeys = new List<SubscriptionType>();
        }
        /// <summary>
        /// Gets or sets the assembly.
        /// </summary>
        /// <value>The assembly.</value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public List<SubscriptionType> ParentKeys { get; set; }
    }
}