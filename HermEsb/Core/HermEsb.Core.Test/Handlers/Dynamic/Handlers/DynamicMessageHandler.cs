using HermEsb.Core.Handlers.Dynamic;
using HermEsb.Core.Handlers.Service;
using HermEsb.Core.Messages;
using HermEsb.Core.Test.Handlers.Dynamic.Domain;

namespace HermEsb.Core.Test.Handlers.Dynamic.Handlers
{
    [DynamicHandler(BaseType = typeof(IDynamicBaseMessage))]
    public class DynamicMessageHandler<TMessage> : IServiceMessageHandler<TMessage> where TMessage : IMessage
    {
        /// <summary>
        /// Gets or sets the param1.
        /// </summary>
        /// <value>The param1.</value>
        public string Param1 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicMessageHandler&lt;TMessage&gt;"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        public DynamicMessageHandler(string param1)
        {
            Param1 = param1;
        }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleMessage(TMessage message)
        {
            
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            
        }

        /// <summary>
        /// Gets or sets the bus.
        /// </summary>
        /// <value>The bus.</value>
        public IBus Bus { get; set; }
    }
}