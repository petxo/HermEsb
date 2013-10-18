using System;

namespace HermEsb.Core.Communication.EndPoints
{
    ///<summary>
    ///</summary>
    public class EventReceiverEndPointHandlerArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public byte[] Message { get; set; }
    }
}