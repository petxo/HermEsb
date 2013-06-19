using HermEsb.Core.Messages;

namespace HermEsb.Core.Handlers.Context
{
    /// <summary>
    /// 
    /// </summary>
    public static class ContextHandlerFactory
    {
        /// <summary>
        /// Creates the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="callContext">The call context.</param>
        /// <returns></returns>
        public static IContextHandler Create(Session session, Session callContext)
        {
            return new ContextHandler { Session = session, CallContext = callContext };
        }
    }
}