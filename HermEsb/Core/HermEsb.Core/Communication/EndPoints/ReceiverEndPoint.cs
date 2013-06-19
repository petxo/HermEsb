using HermEsb.Core.Communication.Channels;
using HermEsb.Core.Logging;
using HermEsb.Logging;
using System;

namespace HermEsb.Core.Communication.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public class ReceiverEndPoint : IReceiverEndPoint, ILoggable
    {
        private readonly IChannelController _channelController;
        private readonly IReceiverChannel _receiverChannel;

        private ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiverEndPoint"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="channelController">The channel controller.</param>
        /// <param name="receiverChannel">The channel.</param>
        public ReceiverEndPoint(Uri uri, IChannelController channelController, IReceiverChannel receiverChannel)
        {
            Uri = uri;
            _channelController = channelController;
            _receiverChannel = receiverChannel;

            _receiverChannel = receiverChannel;
            _receiverChannel.OnReceivedMessage += OnReceiverChannelMessageReceived;
            _receiverChannel.OnStop += OnReceiverChannelStoped;
            _receiverChannel.OnStart += OnReceiverChannelStarted;
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
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger
        {
            get { return _logger ?? new NullLogger(); }
            set { _logger = value; }
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public EndPointStatus Status
        {
            get { return _receiverChannel.Status; }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            _receiverChannel.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            _receiverChannel.Stop();
        }

        /// <summary>
        /// Occurs when [on start].
        /// </summary>
        public event EventHandler OnStart;

        /// <summary>
        /// Invokes the on start.
        /// </summary>
        public void InvokeOnStart()
        {
            EventHandler handler = OnStart;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when [on stop].
        /// </summary>
        public event EventHandler OnStop;

        /// <summary>
        /// Invokes the on stop.
        /// </summary>
        private void InvokeOnStop()
        {
            EventHandler handler = OnStop;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when [channel started].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnReceiverChannelStarted(object sender, EventArgs e)
        {
            InvokeOnStart();
        }

        /// <summary>
        /// Called when [channel stoped].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnReceiverChannelStoped(object sender, EventArgs e)
        {
            InvokeOnStop();
        }


        /// <summary>
        /// Occurs when [on received message].
        /// </summary>
        public event EventReceiverEndPointHandler OnReceivedMessage;

        /// <summary>
        /// Invokes the on received message.
        /// </summary>
        /// <param name="args">The args.</param>
        private void InvokeOnReceivedMessage(EventReceiverEndPointHandlerArgs args)
        {
            EventReceiverEndPointHandler handler = OnReceivedMessage;
            if (handler != null) handler(this, args);
        }

        /// <summary>
        /// Messages the received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        private void OnReceiverChannelMessageReceived(object sender, EventReceiverEndPointHandlerArgs args)
        {
            Logger.Debug("End Point Receiver Message Started");
            InvokeOnReceivedMessage(args);
        }


        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _channelController.Count();
        }


        /// <summary>
        /// Purges this instance.
        /// </summary>
        public void Purge()
        {
            _channelController.Purge();
        }

        /// <summary>
        /// Reinjects the specified serialized message.
        /// </summary>
        /// <param name="serializedMessage">The serialized message.</param>
        /// <param name="priority"></param>
        public void Reinject(string serializedMessage, int priority)
        {
            _receiverChannel.Send(serializedMessage, priority);
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
                _receiverChannel.OnReceivedMessage -= OnReceiverChannelMessageReceived;
                _receiverChannel.OnStop -= OnReceiverChannelStoped;
                _receiverChannel.OnStart -= OnReceiverChannelStarted;
                _receiverChannel.Dispose();
            }
        }
    }
}