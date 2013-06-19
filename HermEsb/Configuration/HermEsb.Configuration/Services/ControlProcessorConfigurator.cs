using System;
using System.Linq;
using HermEsb.Core;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Handlers;
using HermEsb.Core.Handlers.Control;
using HermEsb.Core.Messages.Control;
using HermEsb.Core.Processors;

namespace HermEsb.Configuration.Services
{
    public class ControlProcessorConfigurator
    {
        private readonly HermEsbServiceConfig _HermEsbServiceConfig;
        private readonly Identification _identification;
        private AgentInputGateway<IControlMessage> _input;
        private AgentOutputGateway _output;
        private IHandlerRepository _handlerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlProcessorConfigurator"/> class.
        /// </summary>
        /// <param name="HermEsbServiceConfig">The HermEsb service config.</param>
        /// <param name="identification">The identification.</param>
        public ControlProcessorConfigurator(HermEsbServiceConfig HermEsbServiceConfig, Identification identification)
        {
            _HermEsbServiceConfig = HermEsbServiceConfig;
            _identification = identification;
        }

        /// <summary>
        /// Creates the control processor.
        /// </summary>
        /// <returns></returns>
        public IController CreateControlProcessor()
        {
            this.CreateInput()
                .CreateOutput()
                .LoadHandlers();
                
            return ProcessorsFactory.CreateControlProcessor(_identification, _input, _output, _handlerRepository);
        }

        /// <summary>
        /// Inputs the specified control processor config.
        /// </summary>
        /// <returns></returns>
        private ControlProcessorConfigurator CreateInput()
        {
            var uri = new Uri(_HermEsbServiceConfig.ControlProcessor.Input.Uri);
            _input = AgentGatewayFactory.CreateInputGateway<IControlMessage>(uri, 
                                                                             _HermEsbServiceConfig.ControlProcessor.Input.Transport,
                                                                             _HermEsbServiceConfig.ControlProcessor.NumberOfParallelTasks,
                                                                             _HermEsbServiceConfig.ControlProcessor.MaxReijections);
            return this;
        }

        /// <summary>
        /// Outputs the specified bus config.
        /// </summary>
        /// <returns></returns>
        private ControlProcessorConfigurator CreateOutput()
        {
            var uri = new Uri(_HermEsbServiceConfig.Bus.ControlInput.Uri);
            _output = AgentGatewayFactory.CreateOutputGateway(_identification, uri, _HermEsbServiceConfig.Bus.ControlInput.Transport);
            return this;
        }


        /// <summary>
        /// Loads the handlers.
        /// </summary>
        /// <returns></returns>
        private ControlProcessorConfigurator LoadHandlers()
        {
            var handlerRepositoryFactory = HandlerRepositoryFactory.GetFactory<ControlHandlerRepositoryFactory>();

            var list = (from HandlerAssemblyConfig handlersAssembly in _HermEsbServiceConfig.ControlProcessor.HandlersAssemblies
                        select handlersAssembly.Assembly).ToList();

            var coreAssembly = string.Format("{0}.dll", handlerRepositoryFactory.GetType().Assembly.GetName().Name);
            if (!list.Contains(coreAssembly))
            {
                list.Add(coreAssembly);
            }

            _handlerRepository = handlerRepositoryFactory.Create(list);

            return this;
        }
    }


}