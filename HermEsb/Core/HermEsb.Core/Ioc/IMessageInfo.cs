using HermEsb.Core.Messages;

namespace HermEsb.Core.Ioc
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMessageInfo
    {
        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        MessageHeader Header { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is reinjected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is reinjected; otherwise, <c>false</c>.
        /// </value>
        bool IsReinjected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is reply.
        /// </summary>
        /// <value><c>true</c> if this instance is reply; otherwise, <c>false</c>.</value>
        bool IsReply { get; set; }

        /// <summary>
        /// Gets or sets the current session.
        /// </summary>
        /// <value>The current session.</value>
        Session CurrentSession { get; set; }

        /// <summary>
        /// Gets or sets the current call context.
        /// </summary>
        /// <value>The current call context.</value>
        Session CurrentCallContext { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        IMessage Body { get; set; }
    }
}