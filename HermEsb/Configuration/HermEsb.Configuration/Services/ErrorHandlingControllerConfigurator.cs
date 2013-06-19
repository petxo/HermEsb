using HermEsb.Configuration.Publishers;
using HermEsb.Core;
using HermEsb.Core.ErrorHandling;
using HermEsb.Core.Gateways;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Messages;
using System;

namespace HermEsb.Configuration.Services
{
    public class ErrorHandlingControllerConfigurator
    {
        private readonly ErrorHandlingControllerConfig _errorHandlingControllerConfig;
        private readonly Identification _identification;
        private IOutputGateway<IMessage> _output;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherConfigurator"/> class.
        /// </summary>
        /// <param name="publisherConfig">The publisher config.</param>
        /// <param name="identification">The identification.</param>
        public ErrorHandlingControllerConfigurator(ErrorHandlingControllerConfig publisherConfig, Identification identification)
        {
            _errorHandlingControllerConfig = publisherConfig;
            _identification = identification;
        }

        /// <summary>
        /// Creates the bus publisher.
        /// </summary>
        /// <returns></returns>
        public IErrorHandlingController Create()
        {
            CreateOutput();
            return ErrorHandlingControllerFactory.Create(_output, _identification);
        }

        /// <summary>
        /// Creates the output.
        /// </summary>
        /// <returns></returns>
        private ErrorHandlingControllerConfigurator CreateOutput()
        {
            var uri = new Uri(_errorHandlingControllerConfig.Output.Uri);
            _output = AgentGatewayFactory.CreateOutputGateway(_identification, uri, _errorHandlingControllerConfig.Output.Transport);
            return this;
        }
    }
}