using System;
using HermEsb.Core.Clustering;
using HermEsb.Core.Clustering.Messages;
using HermEsb.Core.Controller.Messages;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Gateways.Router;
using HermEsb.Core.Handlers.Control;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Router;
using HermEsb.Core.Processors.Router.Subscriptors;
using HermEsb.Logging;

namespace HermEsb.Core.Controller.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class SubscriptionHandler : IControlMessageHandler<ISubscriptionMessage>
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

        public IClusterController ClusterController { get; set; }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleMessage(ISubscriptionMessage message)
        {
            if ((Processor is ISubscriber) && (Controller is IRouterController))
            {
                var routerController = (Controller as IRouterController);
                var subscriptor = message.ToSubscriptor(Processor.Identification);
                if (routerController.Subscriptons.Contains(message.Service))
                {
                    routerController.Subscriptons.Remove(message.Service);
                }
                routerController.Subscriptons.Add(subscriptor);

                //Enviar el mensaje de suscripcion completada
                SendSubscriptionCompleted(message);

                if (!ClusterController.IsNull())
                {
                    var newSuscriberClusterMessage = new NewClusterSubscriberMessage
                    {
                        Identification = Processor.Identification,
                        Service = new SubscriptionMessage
                                        {
                                            InputControlGateway = message.InputControlGateway,
                                            InputGateway = message.InputGateway,
                                            Service = message.Service,
                                            Types = message.Types
                                        }
                    };
                    ClusterController.SendMessage(newSuscriberClusterMessage);
                }
            }
        }

        /// <summary>
        /// Sends the subscription completed.
        /// </summary>
        /// <param name="message">The message.</param>
        private void SendSubscriptionCompleted(ISubscriptionMessage message)
        {
            var endPointMessage = new EndPointMessage();
            if (!ClusterController.IsNull())
            {
                endPointMessage.Uri = ClusterController.EndPointClusterInput.Uri.OriginalString;
                endPointMessage.Type = ClusterController.EndPointClusterInput.Transport;
            }
            else
            {
                endPointMessage.Uri = Processor.ReceiverEndPoint.Uri.OriginalString;
                endPointMessage.Type = Processor.ReceiverEndPoint.Transport;
            }

            var subscriptionCompletedMessage = new SubscriptionCompletedMessage
                                                   {
                                                       BusIdentification = Processor.Identification,
                                                       InputGateway = endPointMessage
                                                   };

            ((IRouterController)Controller).Publish(message.Service, subscriptionCompletedMessage);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {

        }
    }
}