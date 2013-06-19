using HermEsb.Core.Messages;

namespace HermEsb.Core.Handlers.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public interface IServiceMessageHandler<in TMessage> : IMessageHandler<TMessage> where TMessage : IMessage
    {
        /// <summary>
        /// Gets or sets the bus.
        /// </summary>
        /// <value>The bus.</value>
        IBus Bus { get; set; }  
    }
}