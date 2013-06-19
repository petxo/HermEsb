using System;
using RabbitMQ.Client;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    public interface IChannelProvider : IDisposable
    {
        /// <summary>
        /// Creates the channel.
        /// </summary>
        void CreateChannel();

        /// <summary>
        /// Determines whether this instance is connected.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </returns>
        bool IsConnected();

        /// <summary>
        /// Determines whether [is channel available].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is channel available]; otherwise, <c>false</c>.
        /// </returns>
        bool IsChannelAvailable();

        /// <summary>
        /// Occurs when [on connection lost].
        /// </summary>
        event ConnectionLost OnConnectionLost;
        /// <summary>
        /// Occurs when [on connection problem].
        /// </summary>
        event UnableToConnect OnConnectionProblem;

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        IModel Channel { get; set; }

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        IConnection Connection { get; set; }
    }
}