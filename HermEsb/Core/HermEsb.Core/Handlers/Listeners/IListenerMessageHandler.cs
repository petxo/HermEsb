using HermEsb.Core.Messages;

namespace HermEsb.Core.Handlers.Listeners
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public interface IListenerMessageHandler<TMessage> : IMessageHandler<TMessage> where TMessage : IMessage{}
}