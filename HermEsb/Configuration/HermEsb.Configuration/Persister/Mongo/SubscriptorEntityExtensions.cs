using System;
using System.Linq;
using HermEsb.Core;
using HermEsb.Core.Communication;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Gateways.Router;
using HermEsb.Core.Processors.Router.Subscriptors;

namespace HermEsb.Configuration.Persister.Mongo
{
    public static class SubscriptorEntityExtensions
    {
        /// <summary>
        /// Froms the specified subscriptor entity.
        /// </summary>
        /// <param name="subscriptorEntity">The subscriptor entity.</param>
        /// <param name="subscriptor">The subscriptor.</param>
        public static void From(this SubscriptorEntity subscriptorEntity, Subscriptor subscriptor)
        {
            subscriptorEntity.InputGateway = new GatewayEntity
                                                 {
                                                     Uri = subscriptor.ServiceInputGateway.EndPoint.Uri.OriginalString,
                                                     Transport =
                                                         (int) subscriptor.ServiceInputGateway.EndPoint.Transport
                                                 };

            subscriptorEntity.InputControlGateway = new GatewayEntity
                                                        {
                                                            Uri =
                                                                subscriptor.ServiceInputControlGateway.EndPoint.Uri.
                                                                OriginalString,
                                                            Transport =
                                                                (int)
                                                                subscriptor.ServiceInputControlGateway.EndPoint.
                                                                    Transport
                                                        };

            subscriptorEntity.Service = subscriptor.Service;
            
            subscriptorEntity.SubcriptionTypes = subscriptor.SubcriptionTypes.Where(t => t != null).Select(t => t.ToSubscriptorType()).ToList();
        }

        /// <summary>
        /// Toes the type of the subscriptor.
        /// </summary>
        /// <param name="subscriptionKey">The subscription key.</param>
        /// <returns></returns>
        public static SubscriptionType ToSubscriptorType(this SubscriptionKey subscriptionKey)
        {
            var subscriptionType = new SubscriptionType
                {
                    Key = subscriptionKey.Key,
                    ParentKeys = subscriptionKey.ParentKeys.Select(pK => pK.ToSubscriptorType()).ToList()
                };
            return subscriptionType;
        }

        /// <summary>
        /// Toes the subscriptor.
        /// </summary>
        /// <param name="subscriptorEntity">The subscriptor entity.</param>
        /// <param name="processor">The processor.</param>
        /// <returns></returns>
        public static Subscriptor ToSubscriptor(this SubscriptorEntity subscriptorEntity, Identification processor)
        {
            var subscriptor = new Subscriptor
            {
                Service = subscriptorEntity.Service,
                ServiceInputGateway =
                    RouterGatewayFactory.CreateOutputGateway(
                                                          new Uri(subscriptorEntity.InputGateway.Uri),
                                                          (TransportType)subscriptorEntity.InputGateway.Transport),
                ServiceInputControlGateway =
                    AgentGatewayFactory.CreateOutputGateway(processor,
                                                            new Uri(subscriptorEntity.InputControlGateway.Uri),
                                                            (TransportType)subscriptorEntity.InputControlGateway.Transport),
            };


            foreach (var subscriptionTypeMessage in subscriptorEntity.SubcriptionTypes)
            {
                subscriptor.SubcriptionTypes.Add(subscriptionTypeMessage.ToSubscriptorKey());
            }
            return subscriptor;
        }

        /// <summary>
        /// Toes the subscriptor key.
        /// </summary>
        /// <param name="subscriptionType">Type of the subscription.</param>
        /// <returns></returns>
        public static SubscriptionKey ToSubscriptorKey(this SubscriptionType subscriptionType)
        {
            var subscriptionKey = new SubscriptionKey
            {
                Key = subscriptionType.Key,
                ParentKeys = subscriptionType.ParentKeys.Select(pK => ToSubscriptorKey(pK)).ToList()
            };
            return subscriptionKey;
        }
    }
}