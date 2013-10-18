using System.Collections.Generic;
using HermEsb.Core.Controller.Messages;
using HermEsb.Core.Gateways;
using HermEsb.Core.Handlers;
using HermEsb.Core.Handlers.Context;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Builders;
using HermEsb.Core.Messages.Control;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Processors.Agent.Reinjection;

namespace HermEsb.Core.Processors.Agent
{
    /// <summary>
    /// 
    /// </summary>
    public class ControlProcessor : Agent<IControlMessage>, IController 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlProcessor"/> class.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="inputGateway">The input gateway.</param>
        /// <param name="outputGateway">The output gateway.</param>
        /// <param name="handlerRepository">The handler repository.</param>
        /// <param name="messageBuilder">The message builder.</param>
        /// <param name="reinjectionEngine">The reinjection engine.</param>
        internal ControlProcessor(Identification identification,
                                IInputGateway<IControlMessage, MessageHeader> inputGateway, 
                                IOutputGateway<IControlMessage> outputGateway, 
                                IHandlerRepository handlerRepository, IMessageBuilder messageBuilder, IReinjectionEngine reinjectionEngine)
            : base(identification, inputGateway, handlerRepository, messageBuilder, reinjectionEngine)
        {
            OutputGateway = outputGateway;
        }

        /// <summary>
        /// Initializes the message handler.
        /// </summary>
        /// <param name="messageHandler">The message handler.</param>
        /// <param name="contextHandler"></param>
        protected override void InitializeMessageHandler(object messageHandler, IContextHandler contextHandler)
        {
            SetPropertyToHandler(messageHandler, "Controller", this);
            SetPropertyToHandler(messageHandler, "Processor", Processor);
            SetPropertyToHandler(messageHandler, "Context", contextHandler);
        }

        /// <summary>
        /// Gets or sets the proccesor.
        /// </summary>
        /// <value>The proccesor.</value>
        public IProcessor Processor { get; set; }

        /// <summary>
        /// Gets or sets the monitor.
        /// </summary>
        /// <value>The monitor.</value>
        public IMonitor Monitor { get; set; }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public override void Start()
        {
            InputGateway.Purge();

            base.Start();

            if (Monitor != null)
            {
                Monitor.Start();
            }

            SendSubscriptionMessage();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
            base.Stop();

            if (Monitor != null)
            {
                Monitor.Stop();
            }
        }

        /// <summary>
        /// Sends the subscription message.
        /// </summary>
        private void SendSubscriptionMessage()
        {
            if(Processor is ServiceProcessor)
            {
                var serviceProcessor = Processor as ServiceProcessor;

                var subscriptionMessage = new SubscriptionMessage
                                              {
                                                  Service = (Identification) serviceProcessor.Identification,
                                                  InputGateway = new EndPointMessage
                                                                     {
                                                                         Type =
                                                                             serviceProcessor.InputGateway.
                                                                             ReceiverEndPoint.Transport,
                                                                         Uri =
                                                                             serviceProcessor.InputGateway.
                                                                             ReceiverEndPoint.Uri.OriginalString
                                                                     },
                                                  InputControlGateway = new EndPointMessage
                                                                            {
                                                                                Type =
                                                                                    InputGateway.ReceiverEndPoint.
                                                                                    Transport,
                                                                                Uri =
                                                                                    InputGateway.ReceiverEndPoint.Uri.
                                                                                    OriginalString
                                                                            },
                                                  Types = new List<SubscriptionKeyMessage>()
                                              };

                foreach (var messageType in serviceProcessor.GetMessageTypes())
                {
                    subscriptionMessage.Types.Add(SubscriptionKeyMessageFactory.CreateFromType(messageType));
                }

                Publish(subscriptionMessage);
            }
        }
    }
}