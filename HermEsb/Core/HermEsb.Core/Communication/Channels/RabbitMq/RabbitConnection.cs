using HermEsb.Logging;
using System;
using RabbitMQ.Client;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public class RabbitConnection : IRabbitConnection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitConnection"/> class.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="publicationAddress">The publication address.</param>
        public RabbitConnection(string queueName, IConnection connection, IModel channel, PublicationAddress publicationAddress)
        {
            QueueName = queueName;
            Connection = connection;
            Channel = channel;
            PublicationAddress = publicationAddress;
        }

        /// <summary>
        /// Gets or sets the name of the queue.
        /// </summary>
        /// <value>The name of the queue.</value>
        public string QueueName { get; private set; }

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public IConnection Connection { get; private set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        public IModel Channel { get; private set; }

        /// <summary>
        /// Gets or sets the publication address.
        /// </summary>
        /// <value>The publication address.</value>
        public PublicationAddress PublicationAddress { get; private set; }

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
                try
                {
                    Channel.Dispose();
                    if (Connection.IsOpen)
                    {
                        Connection.Close();
                    }
                    Connection.Dispose();
                }
                catch (Exception exception)
                {
                    LoggerManager.Instance.Error("Error al cerrar la conexion, el socket esta muerto", exception);
                }
            }
        }
    }
}