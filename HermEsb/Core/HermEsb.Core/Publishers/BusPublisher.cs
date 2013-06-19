using System;
using HermEsb.Core.Gateways;
using HermEsb.Core.Ioc;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Builders;

namespace HermEsb.Core.Publishers
{
    /// <summary>
    /// 
    /// </summary>
    public class BusPublisher : IBusPublisher
    {
        private readonly IOutputGateway<IMessage> _outputGateway;
        private readonly IMessageBuilder _messageBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusPublisher"/> class.
        /// </summary>
        /// <param name="outputGateway">The output gateway.</param>
        /// <param name="messageBuilder">The message builder.</param>
        public BusPublisher(IOutputGateway<IMessage> outputGateway, IMessageBuilder messageBuilder)
        {
            _outputGateway = outputGateway;
            _messageBuilder = messageBuilder;
        }


        /// <summary>
        /// Publishes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Publish(IMessage message)
        {
            _outputGateway.Send(message);
        }

        /// <summary>
        /// Publishes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        public void Publish(IMessage message, int priority)
        {
            _outputGateway.Send(message, priority);
        }

        /// <summary>
        /// Publishes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="callContext">The call context.</param>
        public void Publish(IMessage message, int priority, Session callContext)
        {
            var messageInfo = new MessageInfo { Header = { Priority = priority, CallContext = callContext }, CurrentCallContext = callContext};
            _outputGateway.Send(message, messageInfo);
        }

        /// <summary>
        /// Gets the message builder.
        /// </summary>
        /// <value>The message builder.</value>
        public IMessageBuilder MessageBuilder
        {
            get { return _messageBuilder; }
        }

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
                _outputGateway.Dispose();
            }
        }
    }
}