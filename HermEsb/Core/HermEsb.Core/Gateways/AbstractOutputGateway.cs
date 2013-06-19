using System;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Ioc;
using HermEsb.Core.Logging;
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
    public abstract class AbstractOutputGateway<TMessage> : IOutputGateway<TMessage>, ILoggable
    {
        private ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentOutputGateway"/> class.
        /// </summary>
        /// <param name="senderEndPoint">The sender end points.</param>
        protected AbstractOutputGateway(ISenderEndPoint senderEndPoint)
            : this(senderEndPoint, new JsonDataContractSerializer())
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentOutputGateway"/> class.
        /// </summary>
        /// <param name="senderEndPoint">The sender end points.</param>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        protected AbstractOutputGateway(ISenderEndPoint senderEndPoint, IDataContractSerializer dataContractSerializer)
        {
            SenderEndPoint = senderEndPoint;
            DataContractSerializer = dataContractSerializer;
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
        /// Gets or sets the sender end points.
        /// </summary>
        /// <value>The sender end points.</value>
        protected ISenderEndPoint SenderEndPoint { get; private set; }


        /// <summary>
        /// Gets the end point.
        /// </summary>
        /// <value>The end point.</value>
        public IEndPoint EndPoint
        {
            get { return SenderEndPoint; }
        }

        /// <summary>
        /// Gets the sender end point.
        /// </summary>
        /// <returns></returns>
        public ISenderEndPoint GetSenderEndPoint()
        {
            return SenderEndPoint;
        }

        /// <summary>
        /// Gets or sets the data contract serializer.
        /// </summary>
        /// <value>The data contract serializer.</value>
        protected IDataContractSerializer DataContractSerializer { get; private set; }

        
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public abstract int Send(TMessage message);

        /// <summary>
        /// Sends the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="priority">The priority.</param>
        public abstract int Send(TMessage msg, int priority);

        /// <summary>
        /// Sends the specified MSG.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageInfo">The message info.</param>
        /// <returns></returns>
        public abstract int Send(TMessage message, IMessageInfo messageInfo);

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
                SenderEndPoint.Dispose();
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("EndPoint: {0}", SenderEndPoint);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(AbstractOutputGateway<TMessage> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.SenderEndPoint, SenderEndPoint);
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
            if (obj.GetType() != typeof (AbstractOutputGateway<TMessage>)) return false;
            return Equals((AbstractOutputGateway<TMessage>) obj);
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
            return (SenderEndPoint != null ? SenderEndPoint.GetHashCode() : 0);
        }

        /// <summary>
        /// Occurs when [sent message].
        /// </summary>
        public event MessageGatewayEventHandler SentMessage;

        /// <summary>
        /// Invokes the sent message.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="size">The size.</param>
        protected void InvokeSentMessage(string type, int size)
        {
            MessageGatewayEventHandler handler = SentMessage;
            if (handler != null)
            {
                Task.Factory.StartNew(() => handler(this, new MessageGatewayEventHandlerArgs
                                                              {
                                                                  MessageType = type,
                                                                  Size = size
                                                              }));
            }
        }
    }
}