using System;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public class RabbitSenderChannel : AbstractSenderChannel
    {

        private readonly IRabbitWrapper _rabbitWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitSenderChannel"/> class.
        /// </summary>
        /// <param name="rabbitWrapper">The rabbit wrapper.</param>
        internal RabbitSenderChannel(IRabbitWrapper rabbitWrapper)
        {
            _rabbitWrapper = rabbitWrapper;
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        public override void Send(string message, int priority)
        {
            try
            {
                _rabbitWrapper.Publish(message, priority);
            }
            catch (Exception)
            {
                Logger.Error(string.Format("Error Rabbit Sender Channel, message: {0}", message));
                throw;
            }
            
        }

        /// <summary>
        /// Gets the transport.
        /// </summary>
        /// <value>The transport.</value>
        public override TransportType Transport
        {
            get { return TransportType.RabbitMq; }
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
    }
}