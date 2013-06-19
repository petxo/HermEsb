using System;

namespace HermEsb.Core.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageGatewayEventHandlerArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>The type of the message.</value>
        public string MessageType { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public int Size { get; set; }
    }
}