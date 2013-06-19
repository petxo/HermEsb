using System;
using RabbitMQ.Client;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    public interface IRabbitConnection : IDisposable
    {
        /// <summary>
        /// Gets or sets the name of the queue.
        /// </summary>
        /// <value>The name of the queue.</value>
        string QueueName { get; }

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        IConnection Connection { get; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        IModel Channel { get; }

        /// <summary>
        /// Gets or sets the publication address.
        /// </summary>
        /// <value>The publication address.</value>
        PublicationAddress PublicationAddress { get; }
    }
}