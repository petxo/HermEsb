using RabbitMQ.Client;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public class QueueBasicConsumerDecorator : IQueueBasicConsumer
    {
        private readonly QueueingBasicConsumer _basicConsumer;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueBasicConsumerDecorator"/> class.
        /// </summary>
        /// <param name="basicConsumer">The basic consumer.</param>
        /// <param name="sharedQueue">The shared queue.</param>
        internal QueueBasicConsumerDecorator(QueueingBasicConsumer basicConsumer, ISharedQueue sharedQueue)
        {
            _basicConsumer = basicConsumer;
            Queue = sharedQueue;
        }

        /// <summary>
        /// Handles the basic consume ok.
        /// </summary>
        /// <param name="consumerTag">The consumer tag.</param>
        public void HandleBasicConsumeOk(string consumerTag)
        {
            _basicConsumer.HandleBasicConsumeOk(consumerTag);
        }

        /// <summary>
        /// Handles the basic cancel ok.
        /// </summary>
        /// <param name="consumerTag">The consumer tag.</param>
        public void HandleBasicCancelOk(string consumerTag)
        {
            _basicConsumer.HandleBasicCancelOk(consumerTag);
        }

        /// <summary>
        /// Handles the basic cancel.
        /// </summary>
        /// <param name="consumerTag">The consumer tag.</param>
        public void HandleBasicCancel(string consumerTag)
        {
            _basicConsumer.HandleBasicCancel(consumerTag);
        }

        /// <summary>
        /// Handles the model shutdown.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="reason">The <see cref="RabbitMQ.Client.ShutdownEventArgs"/> instance containing the event data.</param>
        public void HandleModelShutdown(IModel model, ShutdownEventArgs reason)
        {
            _basicConsumer.HandleModelShutdown(model, reason);
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public IModel Model
        {
            get { return _basicConsumer.Model; }
            set { _basicConsumer.Model = value; }
        }

        /// <summary>
        /// Gets or sets the consumer tag.
        /// </summary>
        /// <value>The consumer tag.</value>
        public string ConsumerTag
        {
            get { return _basicConsumer.ConsumerTag; }
            set { _basicConsumer.ConsumerTag = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunning
        {
            get { return _basicConsumer.IsRunning; }
        }

        /// <summary>
        /// Gets the shutdown reason.
        /// </summary>
        /// <value>The shutdown reason.</value>
        public ShutdownEventArgs ShutdownReason
        {
            get { return _basicConsumer.ShutdownReason; }
        }

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        public void OnCancel()
        {
            _basicConsumer.OnCancel();
        }

        /// <summary>
        /// Handles the basic deliver.
        /// </summary>
        /// <param name="consumerTag">The consumer tag.</param>
        /// <param name="deliveryTag">The delivery tag.</param>
        /// <param name="redelivered">if set to <c>true</c> [redelivered].</param>
        /// <param name="exchange">The exchange.</param>
        /// <param name="routingKey">The routing key.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="body">The body.</param>
        public void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            _basicConsumer.HandleBasicDeliver(consumerTag, deliveryTag, redelivered, exchange, routingKey, properties, body);
        }

        /// <summary>
        /// Gets the queue.
        /// </summary>
        /// <value>The queue.</value>
        public ISharedQueue Queue { get; internal set; }
    }
}