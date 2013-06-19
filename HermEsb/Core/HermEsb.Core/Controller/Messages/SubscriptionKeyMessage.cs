using System.Runtime.Serialization;
using System.Collections.Generic;

namespace HermEsb.Core.Controller.Messages
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class SubscriptionKeyMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionKeyMessage"/> class.
        /// </summary>
        public SubscriptionKeyMessage()
        {
            ParentKeys = new List<SubscriptionKeyMessage>();
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [DataMember]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the parent keys.
        /// </summary>
        /// <value>The parent keys.</value>
        [DataMember]
        public IList<SubscriptionKeyMessage> ParentKeys { get; set; }
    }
}