using HermEsb.Core.Handlers.Service;
using HermEsb.Core.Test.Fakes.Messages;


namespace HermEsb.Core.Test.Service.Hanlders
{
    public class HandlerMessageFake : IServiceMessageHandler<MessageFake>
    {
        /// <summary>
        /// Gets or sets the bus.
        /// </summary>
        /// <value>The bus.</value>
        public IBus Bus { get; set; }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleMessage(MessageFake message)
        {

        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            
        }
    }
}