using Bteam.SimpleStateMachine;
using HermEsb.Core.Gateways;
using HermEsb.Core.Handlers;
using HermEsb.Core.Handlers.Context;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Builders;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Agent.Reinjection;

namespace HermEsb.Core.Test.Processors.Agent
{
    /// <summary>
    /// 
    /// </summary>
    class AgentFake : Core.Processors.Agent.Agent<IMessage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgentFake"/> class.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="inputGateway">The input gateway.</param>
        /// <param name="handlerRepository">The handler repository.</param>
        public AgentFake(Identification identification, IInputGateway<IMessage> inputGateway, IHandlerRepository handlerRepository)
            : base(identification, inputGateway, handlerRepository, MessageBuilderFactory.CreateDefaultBuilder(), ReinjectionEngineFactory.CreateDefaultEngine(inputGateway))
        {
        }

        public new IOutputGateway<IMessage> OutputGateway
        {
            get { return base.OutputGateway; }
            set { base.OutputGateway = value; }
        }

        public new IHandlerRepository HandlerRepository
        {
            get { return base.HandlerRepository; }
        }

        public new IStateMachine<ProcessorStatus> StateMachine
        {
            get { return base.StateMachine; }
            set { base.StateMachine = value; }
        }

        #region Overrides of Agent<IMessage>

        protected override void InitializeMessageHandler(object messageHandler, IContextHandler contextHandler)
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}
