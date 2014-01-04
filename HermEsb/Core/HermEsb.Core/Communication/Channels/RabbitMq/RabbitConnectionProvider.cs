using HermEsb.Logging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using RabbitMQ.Client;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public class RabbitConnectionProvider : IConnectionProvider<IRabbitConnection>
    {

        private static Regex _regex = new Regex(@"/(?<exch>.*)/(?<queue>.*)/(?<key>.*)");
        private readonly int _maxReconnections;

        private int _reconnections;

        private ILogger _logger;

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
        /// Initializes a new instance of the <see cref="RabbitConnectionProvider"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public RabbitConnectionProvider(Uri uri)
            : this(uri, 10)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitConnectionProvider"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="maxReconnections">The max reconnections.</param>
        public RabbitConnectionProvider(Uri uri, int maxReconnections)
        {
            Uri = uri;
            _maxReconnections = maxReconnections;
        }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>The URI.</value>
        public Uri Uri { get; private set; }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        /// <returns></returns>
        public IRabbitConnection Connect()
        {
            while (_reconnections < _maxReconnections)
            {
                try
                {
                    Logger.Info(string.Format("Reconectando a {1}... {0}", _reconnections, Uri));
                    _reconnections++;
                    var rabbitConnection = CreateConnection();

                    _reconnections = 0;
                    return rabbitConnection;
                }
                catch (Exception exception)
                {
                    Logger.Error("Error al conectar", exception);
                    Thread.Sleep(1000);
                    Connect();
                }
            }
            Logger.Info(string.Format("Se ha excedido el numero maximo de reconexiones a {1}... {0}", _reconnections, Uri));
            throw new RabbitReconnectionException();
        }

        private RabbitConnection CreateConnection()
        {
            var queue = string.Empty;
            var exch = string.Empty;
            var key = string.Empty;
            var port = Uri.Port !=-1 ? Uri.Port : 5672;

            var match = _regex.Match(Uri.LocalPath);
            if (match.Success)
            {
                exch = match.Groups["exch"].Value;
                queue = match.Groups["queue"].Value;
                key = match.Groups["key"].Value;
            }

            var factory = new ConnectionFactory 
                { Protocol = Protocols.FromEnvironment(), HostName = Uri.Host, Port = port, RequestedHeartbeat = 50 };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exch, ExchangeType.Direct, true);
            
            channel.QueueDeclare(queue, true, false, false, null);
            channel.QueueBind(queue, exch, key);

            var publicationAddress = new PublicationAddress(ExchangeType.Direct, exch, key);

            return new RabbitConnection(queue, connection, channel, publicationAddress);
        }
    }
}