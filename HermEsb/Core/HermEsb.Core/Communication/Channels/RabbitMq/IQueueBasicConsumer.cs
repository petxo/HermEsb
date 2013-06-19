using RabbitMQ.Client;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public interface IQueueBasicConsumer
    {
        /// <summary>
        /// Handles the basic consume ok.
        /// </summary>
        /// <param name="consumerTag">The consumer tag.</param>
        void HandleBasicConsumeOk(string consumerTag);

        /// <summary>
        /// Handles the basic cancel ok.
        /// </summary>
        /// <param name="consumerTag">The consumer tag.</param>
        void HandleBasicCancelOk(string consumerTag);

        /// <summary>
        /// Handles the basic cancel.
        /// </summary>
        /// <param name="consumerTag">The consumer tag.</param>
        void HandleBasicCancel(string consumerTag);

        /// <summary>
        /// Handles the model shutdown.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="reason">The <see cref="RabbitMQ.Client.ShutdownEventArgs"/> instance containing the event data.</param>
        void HandleModelShutdown(IModel model, ShutdownEventArgs reason);

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        IModel Model { get; set; }

        /// <summary>
        /// Gets or sets the consumer tag.
        /// </summary>
        /// <value>The consumer tag.</value>
        string ConsumerTag { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        bool IsRunning { get; }

        /// <summary>
        /// Gets the shutdown reason.
        /// </summary>
        /// <value>The shutdown reason.</value>
        ShutdownEventArgs ShutdownReason { get; }

        /// <summary>
        /// Gets the queue.
        /// </summary>
        /// <value>The queue.</value>
        ISharedQueue Queue { get; }

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        void OnCancel();

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
        void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body);
    }
}