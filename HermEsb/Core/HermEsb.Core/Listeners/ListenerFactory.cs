using HermEsb.Core.Gateways;
using HermEsb.Core.Handlers;
using HermEsb.Core.Messages;
using HermEsb.Logging;

namespace HermEsb.Core.Listeners
{
    /// <summary>
    /// 
    /// </summary>
    public static class ListenerFactory
    {
        /// <summary>
        /// Creates the specified input gateway.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="inputGateway">The input gateway.</param>
        /// <param name="handlerRepository">The handler repository.</param>
        /// <returns></returns>
        public static Listener<TMessage> Create<TMessage>(IInputGateway<TMessage, MessageHeader> inputGateway, IHandlerRepository handlerRepository) where TMessage : IMessage
        {
            var listener = new Listener<TMessage>(inputGateway, handlerRepository);
            listener.Logger = LoggerManager.Instance;
            return listener;
        }
    }
}