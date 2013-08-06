using System;
using HermEsb.Core.Controller.Messages;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Gateways.Router;
using HermEsb.Logging;

namespace HermEsb.Core.Processors.Router.Subscriptors
{
    public static class SubscriptorExtensions
    {

        /// <summary>
        /// To the subscriptor.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="identification">The identification.</param>
        /// <returns></returns>
        public static Subscriptor ToSubscriptor(this ISubscriptionMessage message, Identification identification)
        {
            var subscriptor = new Subscriptor
            {
                Service = message.Service,
                ServiceInputGateway =
                    RouterGatewayFactory.CreateOutputGateway(
                                                          new Uri(message.InputGateway.Uri),
                                                          message.InputGateway.Type),
                ServiceInputControlGateway =
                    AgentGatewayFactory.CreateOutputGateway(identification,
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
         
    }
}