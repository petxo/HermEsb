using HermEsb.Core.Messages;

namespace HermEsb.Core.Handlers.Context
{
    /// <summary>
    /// 
    /// </summary>
    public class ContextHandler : IContextHandler
    {
        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>The session.</value>
        public Session Session { get; set; }


        /// <summary>
        /// Gets or sets the call context.
        /// </summary>
        /// <value>The call context.</value>
        public Session CallContext { get; set; }
    }
}