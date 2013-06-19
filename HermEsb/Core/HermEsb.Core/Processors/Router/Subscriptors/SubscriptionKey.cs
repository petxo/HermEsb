using System.Collections.Generic;

namespace HermEsb.Core.Processors.Router.Subscriptors
{
    /// <summary>
    /// 
    /// </summary>
    public class SubscriptionKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionKey"/> class.
        /// </summary>
        public SubscriptionKey()
        {
            ParentKeys = new List<SubscriptionKey>();
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the parent keys.
        /// </summary>
        /// <value>The parent keys.</value>
        public IList<SubscriptionKey> ParentKeys { get; set; }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        protected bool Equals(SubscriptionKey other)
        {
            return string.Equals(Key, other.Key);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SubscriptionKey) obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return (Key != null ? Key.GetHashCode() : 0);
        }
    }
}