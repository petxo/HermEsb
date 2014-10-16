using System;
using System.Runtime.Serialization;
using HermEsb.Core.Messages;

namespace HermEsb.Core.Test.Fakes.Messages
{
    public interface IMessageFake : IMessage
    {
        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        DateTime CreatedAt { get; set; }
    }


    public class MessageFake : IMessageFake
    {

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>

        public DateTime CreatedAt { get; set; }


        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(MessageFake other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.CreatedAt.Equals(CreatedAt);
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
            if (obj.GetType() != typeof (MessageFake)) return false;
            return Equals((MessageFake) obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return CreatedAt.GetHashCode();
        }
    }
}