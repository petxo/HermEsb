using HermEsb.Core.Clustering;
using HermEsb.Core.Controller.Messages;
using HermEsb.Core.Handlers.Control;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Router;

namespace HermEsb.Core.Controller.Handlers
{
    public class RemoveSubscriberFromClusterHandler : IControlMessageHandler<IRemoveSubscriberFromClusterMessage>
    {
        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleMessage(IRemoveSubscriberFromClusterMessage message)
        {
            if (!message.Trigger.Equals(Processor.Identification))
            {
                if ((Processor is ISubscriber) && (Controller is IRouterController))
                {
                    var routerController = (Controller as IRouterController);
                    if (routerController.Subscriptons.Contains(message.Service))
                    {
                        routerController.Subscriptons.Remove(message.Service);
                    }
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            
        }

        public IProcessor Processor { get; set; }
        public IController Controller { get; set; }
        public IClusterController ClusterController { get; set; }
    }
}