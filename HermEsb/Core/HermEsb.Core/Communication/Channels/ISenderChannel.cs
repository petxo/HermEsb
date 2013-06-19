using System;

namespace HermEsb.Core.Communication.Channels
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISenderChannel : IDisposable
    {
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        void Send(string message, int priority);

        /// <summary>
        /// Gets the transport.
        /// </summary>
        /// <value>The transport.</value>
        TransportType Transport { get; }
    }
}