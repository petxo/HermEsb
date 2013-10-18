using System;
using System.Text;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Logging;
using HermEsb.Core.Messages;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Serialization;
using HermEsb.Logging;
using System.Threading.Tasks;

namespace HermEsb.Core.Gateways
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <typeparam name="THeader">The type of the header.</typeparam>
    public abstract class AbstractInputGateway<TMessage, THeader> : IInputGateway<TMessage, THeader>, ILoggable
    {
        private readonly IReceiverEndPoint _receiverReceiverEndPoint;
        /// <summary>
        /// 
        /// </summary>
        protected readonly IDataContractSerializer DataContractSerializer;
        private ILogger _logger;
        private readonly int _maxReijections;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractInputGateway&lt;TMessage&gt;"/> class.
        /// </summary>
        /// <param name="receiverEndPoint">The receiver end point.</param>
        /// <param name="maxReijections">The max reijections.</param>
        protected AbstractInputGateway(IReceiverEndPoint receiverEndPoint, int maxReijections)
            : this(receiverEndPoint, new JsonDataContractSerializer(), maxReijections)
        {
            _maxReijections = maxReijections;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractInputGateway&lt;TMessage&gt;"/> class.
        /// </summary>
        /// <param name="receiverReceiverEndPoint">The receiver receiver end point.</param>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        /// <param name="maxReijections">The max reijections.</param>
        protected AbstractInputGateway(IReceiverEndPoint receiverReceiverEndPoint, IDataContractSerializer dataContractSerializer, int maxReijections)
        {
            _receiverReceiverEndPoint = receiverReceiverEndPoint;
            _maxReijections = maxReijections;
            DataContractSerializer = dataContractSerializer;

            _receiverReceiverEndPoint.OnReceivedMessage += OnReceiverEndPointHandler;
        }

        /// <summary>
        /// Called when [receiver end point handler].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        private void OnReceiverEndPointHandler(object sender, EventReceiverEndPointHandlerArgs args)
        {
            try
            {
                ProcessReceivedMessage(args.Message);
            }
            catch (Exception ex)
            {
                Logger.Fatal(string.Format("Error Intput Gateway {0}", _receiverReceiverEndPoint.Uri), ex);
                Logger.Fatal(string.Format("MessageBus: {0}", args.Message));
                //TODO: Tratamiento de errores en el InputGateWay
            }
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
        /// Processes the received message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="serializedMessage">The serialized message.</param>
        protected abstract void ProcessReceivedMessage(byte[] serializedMessage);

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public EndPointStatus Status
        {
            get { return _receiverReceiverEndPoint.Status; }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            Logger.Debug("Starting Input Gateway...");
            _receiverReceiverEndPoint.Start();
            Logger.Debug("Input Gateway Started");
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            Logger.Debug("Stoping Input Gateway...");
            _receiverReceiverEndPoint.Stop();
            Logger.Debug("Input Gateway Stoped");
        }

        /// <summary>
        /// Occurs when [on start].
        /// </summary>
        public event EventHandler OnStart;
        /// <summary>
        /// Occurs when [on stop].
        /// </summary>
        public event EventHandler OnStop;

        /// <summary>
        /// Occurs when a message is retrieved form the associated queue.
        /// </summary>
        public event OutputGatewayEventHandler<TMessage, THeader> OnMessage;

        /// <summary>
        /// Purges this instance.
        /// </summary>
        public void Purge()
        {
            _receiverReceiverEndPoint.Purge();
        }

        /// <summary>
        /// Invokes the on message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="serializedMessage">The serialized message.</param>
        /// <param name="header">The header.</param>
        protected void InvokeOnMessage(TMessage message, byte[] serializedMessage, THeader header)
        {
            Logger.Debug("Input Gateway Received Message");
            var args = new OutputGatewayEventHandlerArgs<TMessage, THeader> { Message = message, Header = header, SerializedMessage = serializedMessage };
            var handler = OnMessage;
            if (handler != null) handler(this, args);
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
                _receiverReceiverEndPoint.Dispose();
            }
        }

        /// <summary>
        /// Gets the end point.
        /// </summary>
        /// <value>The end point.</value>
        public IEndPoint ReceiverEndPoint
        {
            get { return _receiverReceiverEndPoint; }
        }

        public override string ToString()
        {
            return string.Format("EndPoint: {0}", _receiverReceiverEndPoint);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(AbstractInputGateway<TMessage, THeader> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._receiverReceiverEndPoint, _receiverReceiverEndPoint);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(AbstractInputGateway<TMessage, THeader>)) return false;
            return Equals((AbstractInputGateway<TMessage, THeader>)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return (_receiverReceiverEndPoint != null ? _receiverReceiverEndPoint.GetHashCode() : 0);
        }

        /// <summary>
        /// Counts the items in the endpoint.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _receiverReceiverEndPoint.Count();
        }

        /// <summary>
        /// Occurs when [received message].
        /// </summary>
        public event MessageGatewayEventHandler OnMessageBusReceived;

        /// <summary>
        /// Invokes the received message.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="size">The size.</param>
        /// <param name="createdAt">The created at.</param>
        protected void InvokeReceivedMessage(string type, int size, DateTime createdAt)
        {
            MessageGatewayEventHandler handler = OnMessageBusReceived;
            if (handler != null){ 
                Task.Factory.StartNew(() => handler(this, new MessageGatewayEventHandlerArgs
                                                              {
                                                                  CreatedAt = createdAt,
                                                                  MessageType = type,
                                                                  Size = size
                                                              }));
            }
        }

        /// <summary>
        /// Reinjects the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="header">The header.</param>
        public void Reinject(IMessage message, MessageHeader header)
        {
            if (header.ReinjectionNumber > _maxReijections)
                return;

            header.ReinjectionNumber++;

            var messageBus = MessageBusFactory.Create(header.IdentificationService, message, DataContractSerializer);
            messageBus.Header = header;
            var serializedMessage = DataContractSerializer.Serialize(messageBus);
            _receiverReceiverEndPoint.Reinject(serializedMessage, header.Priority);
        }
    }
}
