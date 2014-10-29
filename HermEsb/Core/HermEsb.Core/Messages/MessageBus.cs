using System;
using System.Runtime.Serialization;

namespace HermEsb.Core.Messages
{
    /// <summary>
    /// Internal Message Bus
    /// </summary>
    [Serializable]

    public class MessageBus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBus"/> class.
        /// </summary>
        public MessageBus()
        {
            Header = new MessageHeader();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBus"/> class.
        /// </summary>
        /// <param name="body">The body.</param>
        public MessageBus(string body)
            : this()
        {
            Body = body;
        }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>

        public MessageHeader Header { get; set; }

        /// <summary>
        /// Gets or sets the body in JSON
        /// </summary>
        /// <value>The body.</value>

        public string Body { get; set; }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(MessageBus other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Header, Header) && Equals(other.Body, Body);
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
            if (obj.GetType() != typeof (MessageBus)) return false;
            return Equals((MessageBus) obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Header != null ? Header.GetHashCode() : 0)*397) ^ (Body != null ? Body.GetHashCode() : 0);
            }
        }
    }
}