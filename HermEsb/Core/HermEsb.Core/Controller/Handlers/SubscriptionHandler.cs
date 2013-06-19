using System;
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

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleMessage(ISubscriptionMessage message)
        {
            if ((Processor is ISubscriber) && (Controller is IRouterController))
            {
                var routerController = (Controller as IRouterController);
                var subscriptor = GetSubscriptor(message);
                if (routerController.Subscriptons.Contains(message.Service))
                {
                    routerController.Subscriptons.Remove(message.Service);
                }
                routerController.Subscriptons.Add(subscriptor);

                //Enviar el mensaje de suscripcion completada
                SendSubscriptionCompleted(message);
            }
        }

        /// <summary>
        /// Sends the subscription completed.
        /// </summary>
        /// <param name="message">The message.</param>
        private void SendSubscriptionCompleted(ISubscriptionMessage message)
        {
            var subscriptionCompletedMessage = new SubscriptionCompletedMessage
                                                   {
                                                       BusIdentification = Processor.Identification,
                                                       InputGateway = new EndPointMessage
                                                           {
                                                               Uri = Processor.ReceiverEndPoint.Uri.OriginalString,
                                                               Type = Processor.ReceiverEndPoint.Transport
                                                           }
                                                   };

            ((IRouterController) Controller).Publish(message.Service, subscriptionCompletedMessage);
        }

        /// <summary>
        /// Gets the subscriptor.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private Subscriptor GetSubscriptor(ISubscriptionMessage message)
        {
            var subscriptor = new Subscriptor
                                  {
                                      Service = message.Service,
                                      ServiceInputGateway =
                                          RouterGatewayFactory.CreateOutputGateway(
                                                                                new Uri(message.InputGateway.Uri),
                                                                                message.InputGateway.Type),
                                      ServiceInputControlGateway =
                                          AgentGatewayFactory.CreateOutputGateway(Processor.Identification,
                                                                                  new Uri(message.InputControlGateway.Uri),
                                                                                  message.InputControlGateway.Type),
                                  };


            foreach (var subscriptionTypeMessage in message.Types)
            {
                LoggerManager.Instance.Debug(string.Format("Adding Type {0}", subscriptionTypeMessage.Key));

                subscriptor.SubcriptionTypes.Add(subscriptionTypeMessage.ToSubscriptorKey());
            }
            return subscriptor;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            
        }
    }
}