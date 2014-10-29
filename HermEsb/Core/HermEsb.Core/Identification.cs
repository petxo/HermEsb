using System;
using System.Runtime.Serialization;

namespace HermEsb.Core
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]

    public class Identification
    {
        /// <summary>
        /// Gets or sets the empty.
        /// </summary>
        /// <value>The empty.</value>
        public static Identification Empty { get; private set; }

        /// <summary>
        /// Initializes the <see cref="Identification"/> class.
        /// </summary>
        static Identification()
        {
            Empty = new Identification();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>

        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>

        public string Type { get; set; }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(Identification other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id) && Equals(other.Type, Type);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Identification)) return false;
            return Equals((Identification)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Id != null ? Id.GetHashCode() : 0) * 397) ^ (Type != null ? Type.GetHashCode() : 0);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Id: {0}, Type: {1}", Id, Type);
        }

        /// <summary>
        /// Determines whether this instance is emtpy.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is emtpy; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty()
        {
            return Equals(Empty);
        }
    }
}