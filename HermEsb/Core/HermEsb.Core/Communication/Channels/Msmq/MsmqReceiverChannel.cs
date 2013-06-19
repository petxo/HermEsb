using System;
using System.Messaging;
using HermEsb.Core.Communication.Channels.Specifications;

namespace HermEsb.Core.Communication.Channels.Msmq
{
    /// <summary>
    /// 
    /// </summary>
    public class MsmqReceiverChannel : AbstractReceiverChannel
    {
        private readonly IMessageQueue _messageQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsmqReceiverChannel"/> class.
        /// </summary>
        /// <param name="numberOfParallelTasks">The number of parallel tasks.</param>
        /// <param name="messageQueue">The message queue.</param>
        public MsmqReceiverChannel(int numberOfParallelTasks, IMessageQueue messageQueue)
            : base(numberOfParallelTasks)
        {
            _messageQueue = messageQueue;
        }


        /// <summary>
        /// Initializes the end point.
        /// </summary>
        protected override void InitializeEndPoint()
        {
            _messageQueue.ReceiveCompleted += OnMsmqReceivedCompleted;
        }

        /// <summary>
        /// Called when [received completed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Messaging.ReceiveCompletedEventArgs"/> instance containing the event data.</param>
        private void OnMsmqReceivedCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var messageQueue = (MessageQueue)sender;
            try
            {
                var message = messageQueue.EndReceive(e.AsyncResult);
                InvokeOnReceivedCompleted((string) message.Body);
                StartReceive(TimeSpan.FromSeconds(10));
            }
            catch (MessageQueueException)
            {
                //Es un timeout con suerte
                ReceivedUncompleted();
                StartReceive(TimeSpan.FromSeconds(10));
            }
        }

        protected override void StartReceive(TimeSpan fromSeconds)
        {
            if (RunningChannelSpec.Instance.IsSatisfiedBy(this))
            {
                _messageQueue.BeginReceive(fromSeconds);
            }
            else
            {
                ReadingQueue = false;
            }
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
        /// Gets the transport.
        /// </summary>
        /// <value>The transport.</value>
        public override TransportType Transport
        {
            get { return TransportType.Msmq; }
        }
    }
}