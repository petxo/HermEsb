using System;
using HermEsb.Core.Controller.Messages;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Handlers.Control;
using HermEsb.Core.Processors;

namespace HermEsb.Core.Controller.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class SubscriptionCompletedHandler : IControlMessageHandler<ISubscriptionCompletedMessage>
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
        public void HandleMessage(ISubscriptionCompletedMessage message)
        {
            Processor.JoinedBusInfo.Identification = message.BusIdentification;

            if (Processor is IConfigurableProcessor)
            {
                var outputGateway = AgentGatewayFactory.CreateOutputGateway(Processor.Identification,
                                                        new Uri(message.InputGateway.Uri),
                                                        message.InputGateway.Type);
                //TODO: Hacer el dispose del gateway viejoñ
                (Processor as IConfigurableProcessor).ConfigureOutputGateway(outputGateway);
                Processor.Start();
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