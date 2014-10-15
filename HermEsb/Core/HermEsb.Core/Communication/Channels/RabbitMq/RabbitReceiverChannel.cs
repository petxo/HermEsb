using System;
using HermEsb.Core.Communication.Channels.Specifications;
using RabbitMQ.Client.Events;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public class RabbitReceiverChannel : AbstractReceiverChannel
    {
        private readonly IRabbitWrapper _rabbitWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitReceiverChannel"/> class.
        /// </summary>
        /// <param name="numberOfParallelTasks">The number of parallel tasks.</param>
        /// <param name="rabbitWrapper">The rabbit wrapper.</param>
        public RabbitReceiverChannel(int numberOfParallelTasks, IRabbitWrapper rabbitWrapper)
            : base(numberOfParallelTasks)
        {
            _rabbitWrapper = rabbitWrapper;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _rabbitWrapper.Dispose();
            }
        }

        /// <summary>
        /// Initializes the end point.
        /// </summary>
        protected override void InitializeEndPoint()
        {
            Logger.Debug("InitializeEndPoint, Create Basic Consumer");
            _rabbitWrapper.CreateBasicConsumer();
        }

        /// <summary>
        /// Starts the receive.
        /// </summary>
        /// <param name="fromSeconds"></param>
        protected override void StartReceive(TimeSpan fromSeconds)
        {
            Logger.Debug("Rabbit Receiver, Start Receive");
            while (RunningChannelSpec.Instance.IsSatisfiedBy(this))
            {
                BasicDeliverEventArgs result;
                if (_rabbitWrapper.Dequeue((int)fromSeconds.TotalMilliseconds, out result))
                {
                    _rabbitWrapper.BasicAck(result.DeliveryTag, true);
                    InvokeOnReceivedCompleted(result.Body);
                }
            }
            ReadingQueue = false;
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        public override void Send(string message, int priority)
        {
            _rabbitWrapper.Publish(message, priority);
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        public override void Send(byte[] message, int priority)
        {
            _rabbitWrapper.Publish(message, priority);
        }

        /// <summary>
        /// Gets the transport.
        /// </summary>
        /// <value>The transport.</value>
        public override TransportType Transport
        {
            get { return TransportType.RabbitMq; }
        }
    }
}