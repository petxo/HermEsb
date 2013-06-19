using System.Linq;
using HermEsb.Core.Controller.Messages;

namespace HermEsb.Core.Processors.Router.Subscriptors
{
    /// <summary>
    /// 
    /// </summary>
    public static class SubscriptionKeyExtensions
    {
        /// <summary>
        /// Toes the subscriptor key.
        /// </summary>
        /// <param name="subscription">The subscription.</param>
        /// <returns></returns>
        public static SubscriptionKey ToSubscriptorKey(this SubscriptionKeyMessage subscription)
        {
            var subscriptionKey = new SubscriptionKey
                {
                    Key = subscription.Key,
                    ParentKeys = subscription.ParentKeys.Select(pK => ToSubscriptorKey(pK)).ToList()
                };
            return subscriptionKey;
        }

        /// <summary>
        /// Determines whether [is assignable key] [the specified key1].
        /// </summary>
        /// <param name="key1">The key1.</param>
        /// <param name="key">The key.</param>
        /// <returns>
        /// 	<c>true</c> if [is assignable key] [the specified key1]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAssignableKey(this SubscriptionKey key1, string key)
        {
            return key1.Key == key || (key1.ParentKeys != null && key1.ParentKeys.Any(subscriptionKey => subscriptionKey != null && subscriptionKey.IsAssignableKey(key)));
        }
    }
}