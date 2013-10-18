using System;
using System.Linq;
using HermEsb.Configuration.MessageBuilder;
using HermEsb.Core;
using HermEsb.Core.Gateways;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Handlers;
using HermEsb.Core.Handlers.Service;
using HermEsb.Core.Messages;
using HermEsb.Core.Processors;

namespace HermEsb.Configuration.Services
{
    public class ServiceProcessorConfigurator
    {
        private readonly ServiceProcessorConfig _serviceProcessorConfig;
        private readonly Identification _identification;
        private IInputGateway<IMessage, MessageHeader> _input;
        private IHandlerRepository _handlerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceProcessorConfigurator"/> class.
        /// </summary>
        /// <param name="serviceProcessorConfig">The service processor config.</param>
        /// <param name="identification">The identification.</param>
        public ServiceProcessorConfigurator(ServiceProcessorConfig serviceProcessorConfig, Identification identification)
        {
            _serviceProcessorConfig = serviceProcessorConfig;
            _identification = identification;
        }

        /// <summary>
        /// Creates the service processor.
        /// </summary>
        /// <returns></returns>
        public IProcessor CreateServiceProcessor()
        {
            CreateInput().CreateHandlerRepository();
            return ProcessorsFactory.CreateServiceProcessor(_identification, _input, _handlerRepository, ConfigurationMessageBuilder.MessageBuilder);
        }

        /// <summary>
        /// Creates the handler repository.
        /// </summary>
        /// <returns></returns>
        private ServiceProcessorConfigurator CreateHandlerRepository()
        {
            var handlerRepositoryFactory = HandlerRepositoryFactory.GetFactory<ServiceHandlerRepositoryFactory>();
            var assemblies = _serviceProcessorConfig.HandlersAssemblies.Cast<HandlerAssemblyConfig>().Select(ha => ha.Assembly);
            _handlerRepository = handlerRepositoryFactory.Create(assemblies);
            return this;
        }

        /// <summary>
        /// Creates the input.
        /// </summary>
        /// <returns></returns>
        private ServiceProcessorConfigurator CreateInput()
        {
            var uri = new Uri(_serviceProcessorConfig.Input.Uri);
            _input = AgentGatewayFactory.CreateInputGateway<IMessage>(uri, 
                                                                _serviceProcessorConfig.Input.Transport, 
                                                                _serviceProcessorConfig.NumberOfParallelTasks,
                                                                _serviceProcessorConfig.MaxReijections);
            return this;
        }
    }
}