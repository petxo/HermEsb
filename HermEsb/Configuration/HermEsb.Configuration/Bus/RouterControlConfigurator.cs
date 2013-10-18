using System;
using System.Collections.Generic;
using System.Linq;
using BteamMongoDB;
using BteamMongoDB.Config;
using HermEsb.Configuration.Bus.Persisters.Mongo;
using HermEsb.Configuration.Persister.Mongo;
using HermEsb.Configuration.Services;
using HermEsb.Core;
using HermEsb.Core.Gateways;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Handlers;
using HermEsb.Core.Handlers.Control;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Control;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Router.Subscriptors;
using HermEsb.Core.Processors.Router.Subscriptors.Persisters;

namespace HermEsb.Configuration.Bus
{
    public class RouterControlConfigurator
    {
        private readonly Identification _identification;
        private readonly HermEsbConfig _hermEsbConfig;
        private IHandlerRepository _handlerRepository;
        private IInputGateway<IControlMessage, MessageHeader> _input;
        private ISubscriptorsHelper _subscriptorsHelpers;
        private ISubscriptorsPersister _subscriptorsPersiter;


        /// <summary>
        ///     Initializes a new instance of the <see cref="RouterControlConfigurator" /> class.
        /// </summary>
        /// <param name="hermEsbConfig">The HermEsb bus config.</param>
        /// <param name="identification">The identification.</param>
        public RouterControlConfigurator(HermEsbConfig hermEsbConfig, Identification identification)
        {
            _hermEsbConfig = hermEsbConfig;
            _identification = identification;
        }

        /// <summary>
        ///     Creates the control processor.
        /// </summary>
        /// <returns></returns>
        public IController CreateControlProcessor()
        {
            CreateInput()
                .LoadHandlers()
                .LoadPersisters()
                .LoadHelpers();

            return ProcessorsFactory.CreateRouterControlProcessor(_identification, _input, _handlerRepository,
                                                                  _subscriptorsHelpers);
        }

        /// <summary>
        ///     Creates the input.
        /// </summary>
        /// <returns></returns>
        private RouterControlConfigurator CreateInput()
        {
            var uri = new Uri(_hermEsbConfig.RouterControlProcessor.Input.Uri);
            _input = AgentGatewayFactory.CreateInputGateway<IControlMessage>(uri,
                                                                             _hermEsbConfig.RouterControlProcessor
                                                                                             .Input.Transport,
                                                                             _hermEsbConfig.RouterControlProcessor
                                                                                             .NumberOfParallelTasks,
                                                                             _hermEsbConfig.RouterControlProcessor
                                                                                             .MaxReijections);

            return this;
        }

        /// <summary>
        ///     Loads the handlers.
        /// </summary>
        /// <returns></returns>
        private RouterControlConfigurator LoadHandlers()
        {
            IHandlerRepositoryFactory handlerRepositoryFactory =
                HandlerRepositoryFactory.GetFactory<ControlHandlerRepositoryFactory>();

            List<string> list =
                (from HandlerAssemblyConfig handlersAssembly in
                     _hermEsbConfig.RouterControlProcessor.HandlersAssemblies
                 select handlersAssembly.Assembly).ToList();

            string coreAssembly = string.Format("{0}.dll", handlerRepositoryFactory.GetType().Assembly.GetName().Name);
            if (!list.Contains(coreAssembly))
            {
                list.Add(coreAssembly);
            }

            _handlerRepository = handlerRepositoryFactory.Create(list);

            return this;
        }

        /// <summary>
        ///     Loads the helpers.
        /// </summary>
        /// <returns></returns>
        private RouterControlConfigurator LoadHelpers()
        {
            _subscriptorsHelpers = new SubscriptorsHelper(new MemorySubscriptorsRepository(), _subscriptorsPersiter);
            return this;
        }

        /// <summary>
        ///     Loads the persisters.
        /// </summary>
        /// <returns></returns>
        private RouterControlConfigurator LoadPersisters()
        {
            _subscriptorsPersiter = new NullSubscriptorPersister();

            MongoPersiterConfig mongoPersister = _hermEsbConfig.RouterControlProcessor.Persister.MongoPersister;

            if (mongoPersister.Connection.Servers.Count > 0)
            {
                //Cargamos la configuracion de mongo
                var settings = new MongoSettingsExtended
                    {
                        ConnectionMode = mongoPersister.Connection.ConnectionMode,
                        ConnectTimeout = mongoPersister.Connection.ConnectTimeout,
                        MaxConnectionIdleTime = mongoPersister.Connection.MaxConnectionIdleTime,
                        MaxConnectionLifeTime = mongoPersister.Connection.MaxConnectionLifeTime,
                        MaxConnectionPoolSize = mongoPersister.Connection.MaxConnectionPoolSize,
                        MinConnectionPoolSize = mongoPersister.Connection.MinConnectionPoolSize,
                        ReplicaSetName = mongoPersister.Connection.ReplicaSetName,
                        SlaveOk = mongoPersister.Connection.SlaveOk,
                        SocketTimeout = mongoPersister.Connection.SocketTimeout,
                        WaitQueueSize = mongoPersister.Connection.WaitQueueSize,
                        WaitQueueTimeout = mongoPersister.Connection.WaitQueueTimeout,
                        Database = mongoPersister.Connection.Database
                    };

                foreach (ServerConnection server in mongoPersister.Connection.Servers)
                {
                    settings.AddServer(server.Server, server.Port);
                }

                _subscriptorsPersiter = MongoSubscriptorsPersisterFactory.Create(settings, mongoPersister.Collection);
            }

            return this;
        }
    }
}