using HermEsb.Logging;
using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    public class ChannelProvider : IChannelProvider
    {
        private readonly QueueInfo queueInfo;
        private bool configureChannel;
        public IModel Channel { get; set; }
        public IConnection Connection { get; set; }

        public event ConnectionLost OnConnectionLost;
        public event UnableToConnect OnConnectionProblem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelProvider"/> class.
        /// </summary>
        /// <param name="queueInfo">The queue URI.</param>
        public ChannelProvider(QueueInfo queueInfo)
        {
            this.queueInfo = queueInfo;
            configureChannel = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelProvider"/> class.
        /// </summary>
        public ChannelProvider()
        {
            configureChannel = false;
        }

        /// <summary>
        /// Determines whether [is channel available].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is channel available]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsChannelAvailable()
        {
            return Connection != null && Connection.IsOpen && Channel != null && Channel.IsOpen;
        }

        /// <summary>
        /// Determines whether this instance is connected.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </returns>
        public bool IsConnected()
        {
            return Connection != null && Connection.IsOpen;
        }

        /// <summary>
        /// Creates the channel.
        /// </summary>
        public void CreateChannel()
        {
            var factory = new ConnectionFactory { Protocol = Protocols.FromEnvironment(), HostName = queueInfo.Host };

            try
            {
                if (Connection != null)
                    Connection.ConnectionShutdown -= ConnectionLost;

                Connection = factory.CreateConnection();
                Connection.ConnectionShutdown += ConnectionLost;

                Channel = Connection.CreateModel();

                if (configureChannel)
                {
                    Channel.ExchangeDeclare(queueInfo.Exchange, ExchangeType.Direct);
                    Channel.QueueDeclare(queueInfo.QueueName, true, false, false, null);
                    Channel.QueueBind(queueInfo.QueueName, queueInfo.Exchange, queueInfo.RoutingKey);
                }
            }
            catch (BrokerUnreachableException ex)
            {
                LoggerManager.Instance.Error("Unable to create RabbitMQ channel", ex);
                if (OnConnectionProblem != null)
                    OnConnectionProblem();
            }
        }

        /// <summary>
        /// Connections the lost.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="reason">The <see cref="RabbitMQ.Client.ShutdownEventArgs"/> instance containing the event data.</param>
        private void ConnectionLost(IConnection connection, ShutdownEventArgs reason)
        {
            if (OnConnectionLost != null)
                OnConnectionLost();
        }

        #region Dispose

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            Connection.ConnectionShutdown -= ConnectionLost;
            Channel.Close();
            Connection.Close();

            Channel.Dispose();
            Connection.Dispose();
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ChannelProvider"/> is reclaimed by garbage collection.
        /// </summary>
        ~ChannelProvider()
        {
            Dispose(false);
        }

        #endregion
    }
}