using System;
using System.Linq;
using HermEsb.Configuration.Services;
using HermEsb.Core.Gateways;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Handlers;
using HermEsb.Core.Handlers.Listeners;
using HermEsb.Core.Listeners;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Builders;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Service;
using HermEsb.Logging;

namespace HermEsb.Configuration.Listeners
{
    public class MonitorListenerConfigurator : IConfigurator
    {
        private readonly ListenerConfig _listenerConfig;
        private IInputGateway<IMessage, MessageHeader> _input;
        private IHandlerRepository _handlerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorListenerConfigurator"/> class.
        /// </summary>
        /// <param name="listenerConfig">The listener config.</param>
        public MonitorListenerConfigurator(ListenerConfig listenerConfig)
        {
            _listenerConfig = listenerConfig;
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public IService Create()
        {
            MessageBuilderFactory.CreateDefaultBuilder();
            var listener = ListenerFactory.Create(_input, _handlerRepository);
            return new MonitorListener(listener);
        }

        /// <summary>
        /// Configures this instance.
        /// </summary>
        public void Configure()
        {
            CreateHandlerRepository()
            .CreateInput();
        }

        /// <summary>
        /// Creates the handler repository.
        /// </summary>
        /// <returns></returns>
        private MonitorListenerConfigurator CreateHandlerRepository()
        {
            var handlerRepositoryFactory = HandlerRepositoryFactory.GetFactory<ListenersHandlerRepositoryFactory>();
            var assemblies = _listenerConfig.HandlersAssemblies.Cast<HandlerAssemblyConfig>().Select(ha => ha.Assembly).ToList();
            _handlerRepository = handlerRepositoryFactory.Create(assemblies);
            return this;
        }

        /// <summary>
        /// Creates the input.
        /// </summary>
        /// <returns></returns>
        private MonitorListenerConfigurator CreateInput()
        {
            var uri = new Uri(_listenerConfig.Input.Uri);
            _input = AgentGatewayFactory.CreateInputGateway<IMessage>(uri, _listenerConfig.Input.Transport, 
                                                                                _listenerConfig.NumberOfParallelTasks,
                                                                                _listenerConfig.MaxReijections);
            return this;
        }
    }
}