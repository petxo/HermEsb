using HermEsb.Core.Gateways;
using HermEsb.Core.Gateways.Router;
using HermEsb.Core.Handlers;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Builders;
using HermEsb.Core.Messages.Control;
using HermEsb.Core.Processors.Agent;
using HermEsb.Core.Processors.Agent.Reinjection;
using HermEsb.Core.Processors.Router;
using HermEsb.Core.Processors.Router.Outputs;
using HermEsb.Core.Processors.Router.Subscriptors;
using HermEsb.Logging;

namespace HermEsb.Core.Processors
{
    /// <summary>
    /// </summary>
    public static class ProcessorsFactory
    {
        /// <summary>
        ///     Creates the control processor.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="inputGateway">The input gateway.</param>
        /// <param name="outputGateway">The output gateway.</param>
        /// <param name="handlerRepository">The handler repository.</param>
        /// <returns></returns>
        public static IController CreateControlProcessor(Identification identification,
                                                         IInputGateway<IControlMessage, MessageHeader> inputGateway,
                                                         IOutputGateway<IControlMessage> outputGateway,
                                                         IHandlerRepository handlerRepository)
        {
            return new ControlProcessor(identification,
                                        inputGateway,
                                        outputGateway,
                                        handlerRepository,
                                        MessageBuilderFactory.CreateDefaultBuilder(),
                                        ReinjectionEngineFactory.CreateDefaultEngine(inputGateway))
                {
                    Logger = LoggerManager.Instance
                };
        }


        /// <summary>
        ///     Creates the service processor.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="inputGateway">The input gateway.</param>
        /// <param name="handlerRepository">The handler repository.</param>
        /// <param name="messageBuilder">The message builder.</param>
        /// <returns></returns>
        public static IProcessor CreateServiceProcessor(Identification identification,
                                                        IInputGateway<IMessage, MessageHeader> inputGateway,
                                                        IHandlerRepository handlerRepository,
                                                        IMessageBuilder messageBuilder)
        {
            return new ServiceProcessor(identification,
                                        inputGateway,
                                        handlerRepository,
                                        messageBuilder, ReinjectionEngineFactory.CreateDefaultEngine(inputGateway))
                {
                    Logger = LoggerManager.Instance
                };
        }

        /// <summary>
        ///     Creates the router control processor.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="inputGateway">The input gateway.</param>
        /// <param name="handlerRepository">The handler repository.</param>
        /// <param name="subscriptorsHelper">The subscriptors helper.</param>
        /// <returns></returns>
        public static IController CreateRouterControlProcessor(Identification identification,
                                                               IInputGateway<IControlMessage, MessageHeader> inputGateway,
                                                               IHandlerRepository handlerRepository,
                                                               ISubscriptorsHelper subscriptorsHelper)
        {
            IMessageBuilder defaultObjectBuilder = MessageBuilderFactory.CreateDefaultBuilder();

            return new RouterControlProcessor(identification, inputGateway, handlerRepository, subscriptorsHelper,
                                              defaultObjectBuilder)
                {
                    Logger = LoggerManager.Instance
                };
        }

        /// <summary>
        ///     Creates the router processor.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="inputGateway">The input gateway.</param>
        /// <param name="routerOutputHelper">The router output helper.</param>
        /// <returns></returns>
        public static IProcessor CreateRouterProcessor(Identification identification,
                                                       IInputGateway<byte[], RouterHeader> inputGateway,
                                                       IRouterOutputHelper routerOutputHelper)
        {
            return new RouterProcessor(identification, inputGateway, routerOutputHelper)
                {
                    Logger = LoggerManager.Instance
                };
        }
    }
}