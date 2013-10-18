using HermEsb.Core.Communication.Channels;
using HermEsb.Core.Logging;
using HermEsb.Logging;
using System;

namespace HermEsb.Core.Communication.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public class SenderEndPoint : ISenderEndPoint, ILoggable
    {
        private readonly ISenderChannel _receiverChannel;

        /// <summary>
        /// Initializes a new instance of the <see cref="SenderEndPoint"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="receiverChannel">The channel.</param>
        public SenderEndPoint(Uri uri, ISenderChannel receiverChannel)
        {
            Uri = uri;
            _receiverChannel = receiverChannel;
        }


        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <value>The URI.</value>
        public Uri Uri { get; private set; }

        /// <summary>
        /// Gets the transport.
        /// </summary>
        /// <value>The transport.</value>
        public TransportType Transport
        {
            get { return _receiverChannel.Transport; }
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(string message)
        {
            try
            {
                _receiverChannel.Send(message, 0);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Error Sender End Point {1} - Message: {0}", message, Uri), ex);
                throw;
            }
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        public void Send(string message, int priority)
        {
            try
            {
                _receiverChannel.Send(message, priority);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Error Sender End Point {1} - Message: {0}", message, Uri), ex);
                throw;
            }
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        public void Send(byte[] message, int priority)
        {
            try
            {
                _receiverChannel.Send(message, priority);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Error Sender End Point {1} - Message: {0}", message, Uri), ex);
                throw;
            }
        }

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
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _receiverChannel.Dispose();
            }
        }
    }
}