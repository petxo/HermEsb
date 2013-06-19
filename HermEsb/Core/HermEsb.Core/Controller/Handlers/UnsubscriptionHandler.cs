using HermEsb.Core.Controller.Messages;
using HermEsb.Core.Handlers.Control;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Router;

namespace HermEsb.Core.Controller.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class UnsubscriptionHandler : IControlMessageHandler<UnsubscriptionMessage>
    {
        /// <summary>
        /// Gets or sets the processor.
        /// </summary>
        /// <value>The processor.</value>
        public IProcessor Processor { get; set; }

        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>The controller.</value>
        public IController Controller { get; set; }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleMessage(UnsubscriptionMessage message)
        {
            if ((Processor is ISubscriber) && (Controller is IRouterController))
            {
                var routerController = (Controller as IRouterController);
                if (!routerController.Subscriptons.Contains(message.Service))
                {
                    routerController.Subscriptons.Remove(message.Service);
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            
        }
    }
}