using HermEsb.Core.Messages;

namespace HermEsb.Core.Handlers.Context
{
    /// <summary>
    /// 
    /// </summary>
    public interface IContextHandler
    {
        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>The session.</value>
        Session Session { get; set; }

        /// <summary>
        /// Gets or sets the call context.
        /// </summary>
        /// <value>The call context.</value>
        Session CallContext { get; set; }
    }
}