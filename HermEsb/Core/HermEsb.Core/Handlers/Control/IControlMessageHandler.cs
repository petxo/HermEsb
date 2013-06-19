using HermEsb.Core.Messages;
using HermEsb.Core.Processors;

namespace HermEsb.Core.Handlers.Control
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public interface IControlMessageHandler<TMessage> : IMessageHandler<TMessage> where TMessage : IMessage
    {
        /// <summary>
        /// Gets or sets the processor.
        /// </summary>
        /// <value>The processor.</value>
        IProcessor Processor { get; set; }

        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>The controller.</value>
        IController Controller { get; set; }
    }
}