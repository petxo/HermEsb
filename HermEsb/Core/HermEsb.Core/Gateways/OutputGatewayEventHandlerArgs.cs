using System;
using HermEsb.Core.Messages;

namespace HermEsb.Core.Gateways
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public class OutputGatewayEventHandlerArgs<TMessage, THeader> : EventArgs
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public TMessage Message { get; set; }


        /// <summary>
        /// Gets or sets the serialized message.
        /// </summary>
        /// <value>The serialized message.</value>
        public byte[] SerializedMessage { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public THeader Header { get; set; }
    }
}