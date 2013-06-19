using HermEsb.Core.Messages;

namespace HermEsb.Core.Ioc
{
    /// <summary>
    /// Current message information
    /// </summary>
    public class MessageInfo : IMessageInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageInfo"/> class.
        /// </summary>
        public MessageInfo()
        {
            Header = new MessageHeader();
        }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public MessageHeader Header { get; set; }


        /// <summary>
        /// Gets or sets the current session.
        /// </summary>
        /// <value>The current session.</value>
        public Session CurrentSession { get; set; }

        /// <summary>
        /// Gets or sets the current call context.
        /// </summary>
        /// <value>The current call context.</value>
        public Session CurrentCallContext { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is reinjected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is reinjected; otherwise, <c>false</c>.
        /// </value>
        public bool IsReinjected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is reply.
        /// </summary>
        /// <value><c>true</c> if this instance is reply; otherwise, <c>false</c>.</value>
        public bool IsReply { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public IMessage Body { get; set; }
    }
}