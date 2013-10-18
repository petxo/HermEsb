using System;
using System.Collections.Generic;
using HermEsb.Core.Gateways;
using HermEsb.Core.Handlers;
using HermEsb.Core.Handlers.Context;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Builders;
using HermEsb.Core.Processors.Agent.Reinjection;

namespace HermEsb.Core.Processors.Agent
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceProcessor : Agent<IMessage>, IConfigurableProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceProcessor"/> class.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="inputGateway">The input gateway.</param>
        /// <param name="handlerRepository">The handler repository.</param>
        /// <param name="messageBuilder">The message builder.</param>
        /// <param name="reinjectionEngine">The reinjection engine.</param>
        internal ServiceProcessor(Identification identification,
                                IInputGateway<IMessage, MessageHeader> inputGateway, 
                                IHandlerRepository handlerRepository, IMessageBuilder messageBuilder, IReinjectionEngine reinjectionEngine)
            : base(identification, inputGateway, handlerRepository, messageBuilder, reinjectionEngine)
        {
        }

        /// <summary>
        /// Initializes the message handler.
        /// </summary>
        /// <param name="messageHandler">The message handler.</param>
        /// <param name="contextHandler"></param>
        protected override void InitializeMessageHandler(object messageHandler, IContextHandler contextHandler)
        {
            SetPropertyToHandler(messageHandler, "Bus", this);

            SetPropertyToHandler(messageHandler, "Context", contextHandler);
        }

        /// <summary>
        /// Configures the output gateway.
        /// </summary>
        /// <param name="outputGateway">The output gateway.</param>
        public void ConfigureOutputGateway(IOutputGateway<IMessage> outputGateway)
        {
            OutputGateway = outputGateway;
        }

        /// <summary>
        /// Gets the message types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Type> GetMessageTypes()
        {
            return HandlerRepository.GetMessageTypes();
        }
    }
}