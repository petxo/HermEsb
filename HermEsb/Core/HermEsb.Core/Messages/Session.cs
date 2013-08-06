using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HermEsb.Core.Messages
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Session : Dictionary<string, object>, ISession
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        /// <param name="currentSession">The current session.</param>
        public Session(IDictionary<string, object> currentSession) : base(currentSession)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        public Session()
        {
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            var session = SessionFactory.Create();
            foreach (var item in this)
            {
                session.Add(item.Key, item.Value);
            }
            return session;
        }
    }
}