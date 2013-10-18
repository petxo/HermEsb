using System.Messaging;

namespace HermEsb.Core.Communication.Channels.Msmq
{
    /// <summary>
    /// 
    /// </summary>
    public class MsmqSenderChannel : AbstractSenderChannel
    {
        private readonly IMessageQueue _messageQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsmqSenderChannel"/> class.
        /// </summary>
        /// <param name="messageQueue">The message queue.</param>
        internal MsmqSenderChannel(IMessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
        }


        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        public override void Send(string message, int priority)
        {
            var msmqMessage = new Message(message, _messageQueue.Formatter)
            {
                Recoverable = true,
                Priority = (MessagePriority)priority,
            };

            _messageQueue.Send(msmqMessage);
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        public override void Send(byte[] message, int priority)
        {
            var msmqMessage = new Message(message, _messageQueue.Formatter)
            {
                Recoverable = true,
                Priority = (MessagePriority)priority,
            };

            _messageQueue.Send(msmqMessage);
        }

        /// <summary>
        /// Gets the transport.
        /// </summary>
        /// <value>The transport.</value>
        public override TransportType Transport
        {
            get { return TransportType.Msmq; }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _messageQueue.Dispose();
            }
        }
    }
}