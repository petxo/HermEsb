using System.Collections.Generic;

namespace HermEsb.Core.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public static class SessionFactory
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static Session Create()
        {
            return new Session();
        }

        /// <summary>
        /// Creates the specified current session.
        /// </summary>
        /// <param name="currentSession">The current session.</param>
        /// <returns></returns>
        public static Session Create(IDictionary<string, object> currentSession)
        {
            return new Session(currentSession);
        }
    }
}