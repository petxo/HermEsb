using System;
using HermEsb.Core;
using HermEsb.Core.Gateways;
using HermEsb.Core.Gateways.Router;
using HermEsb.Core.Messages;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Router.Outputs;

namespace HermEsb.Configuration.Bus
{
    public class RouterProcessorConfigurator
    {
        private readonly HermEsbConfig _hermEsbConfig;
        private readonly Identification _identification;
        private IInputGateway<MessageBus> _input;
        private IRouterOutputHelper _routerOutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouterProcessorConfigurator"/> class.
        /// </summary>
        /// <param name="hermEsbConfig">The HermEsb bus config.</param>
        /// <param name="identification">The identification.</param>
        public RouterProcessorConfigurator(HermEsbConfig hermEsbConfig, Identification identification)
        {
            _hermEsbConfig = hermEsbConfig;
            _identification = identification;
        }


        /// <summary>
        /// Creates the router processor.
        /// </summary>
        /// <returns></returns>
        public IProcessor CreateRouterProcessor()
         {
             this.CreateInput()
                 .LoadHelpers();

            return ProcessorsFactory.CreateRouterProcessor(_identification, _input, _routerOutputHelper);
         }

        private RouterProcessorConfigurator LoadHelpers()
        {
            var uri = new Uri(_hermEsbConfig.RouterProcessor.Input.Uri);
            _input = RouterGatewayFactory.CreateInputGateway(uri, 
                                                            _hermEsbConfig.RouterProcessor.Input.Transport, 
                                                            _hermEsbConfig.RouterProcessor.NumberOfParallelTasks,
                                                            _hermEsbConfig.RouterProcessor.MaxReijections);
            return this;
        }

        private RouterProcessorConfigurator CreateInput()
        {
            _routerOutputHelper = new RouterOutputHelper(new MemoryGatewaysRepository());
            return this;
        }
    }
}