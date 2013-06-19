using System;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRabbitWrapper : IDisposable
    {
        ///// <summary>
        ///// Gets or sets the name of the queue.
        ///// </summary>
        ///// <value>The name of the queue.</value>
        //string QueueName { get; }

        ///// <summary>
        ///// Gets or sets the connection.
        ///// </summary>
        ///// <value>The connection.</value>
        //IConnection Connection { get; }

        ///// <summary>
        ///// Gets or sets the channel.
        ///// </summary>
        ///// <value>The channel.</value>
        //IModel Channel { get; }

        ///// <summary>
        ///// Gets or sets the publication address.
        ///// </summary>
        ///// <value>The publication address.</value>
        //PublicationAddress PublicationAddress { get; }

        ///// <summary>
        ///// Gets the basic consumer.
        ///// </summary>
        ///// <returns></returns>
        //IQueueBasicConsumer BasicConsumer { get; }

        /// <summary>
        /// Creates the consumer.
        /// </summary>
        void CreateBasicConsumer();

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Purges the queue.
        /// </summary>
        void Purge();

        /// <summary>
        /// Publishes the specified serialized message.
        /// </summary>
        /// <param name="serializedMessage">The serialized message.</param>
        /// <param name="priority">The priority.</param>
        void Publish(string serializedMessage, int priority);


        /// <summary>
        /// Dequeues the specified milliseconds timeout.
        /// </summary>
        /// <param name="millisecondsTimeout">The milliseconds timeout.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        bool Dequeue(int millisecondsTimeout, out object result);

        /// <summary>
        /// Basics the ack.
        /// </summary>
        /// <param name="deliveryTag">The delivery tag.</param>
        /// <param name="multiple">if set to <c>true</c> [multiple].</param>
        void BasicAck(ulong deliveryTag, bool multiple);
    }
}