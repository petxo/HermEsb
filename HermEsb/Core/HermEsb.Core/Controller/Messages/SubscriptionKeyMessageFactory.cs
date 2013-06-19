using System;

namespace HermEsb.Core.Controller.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public static class SubscriptionKeyMessageFactory
    {

        /// <summary>
        /// Creates from type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static SubscriptionKeyMessage CreateFromType(Type type)
        {
            var subscriptionKeyMessage = new SubscriptionKeyMessage
                {
                    Key = string.Format("{0},{1}", type.FullName, type.Assembly.GetName().Name)
                };

            if (type.BaseType != null)
            {
                subscriptionKeyMessage.ParentKeys.Add(CreateFromType(type.BaseType));
            }

            foreach (var @interface in type.GetInterfaces())
            {
                subscriptionKeyMessage.ParentKeys.Add(CreateFromType(@interface));
            }

            return subscriptionKeyMessage;
        }

    }
}