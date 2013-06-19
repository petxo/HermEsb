using System;
using RabbitMQ.Client;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    public class QueueInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueueInfo"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public QueueInfo(Uri uri)
        {
            Host = uri.Host;
            QueueName = uri.Segments[2].Replace("/", string.Empty);
            Exchange = uri.Segments[1].Replace("/", string.Empty);
            RoutingKey = uri.Segments[3].Replace("/", string.Empty);
        }

        public string Host { get; set; }
        public string QueueName { get; set; }
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }

        /// <summary>
        /// Gets the publication adress.
        /// </summary>
        /// <returns></returns>
        public PublicationAddress GetPublicationAdress()
        {
            return new PublicationAddress(ExchangeType.Direct, Exchange, RoutingKey);
        }
    }
}