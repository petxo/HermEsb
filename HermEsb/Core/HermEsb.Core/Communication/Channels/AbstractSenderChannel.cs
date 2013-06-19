using HermEsb.Core.Logging;
using HermEsb.Logging;
using System;

namespace HermEsb.Core.Communication.Channels
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractSenderChannel : ISenderChannel, ILoggable
    {
        private ILogger _logger;

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger
        {
            get { return _logger ?? new NullLogger(); }
            set { _logger = value; }
        }

        /// <summary>
        /// Gets the transport.
        /// </summary>
        /// <value>The transport.</value>
        public abstract TransportType Transport { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected abstract void Dispose(bool disposing);

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        public abstract void Send(string message, int priority);
    }
}