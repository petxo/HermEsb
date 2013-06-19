using System;
using HermEsb.Configuration.MessageBuilder;
using HermEsb.Core;
using HermEsb.Core.Gateways;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Messages;
using HermEsb.Core.Publishers;

namespace HermEsb.Configuration.Publishers
{
    /// <summary>
    /// 
    /// </summary>
    public class PublisherConfigurator
    {
        private readonly PublisherConfig _publisherConfig;
        private Identification _identification;
        private IOutputGateway<IMessage> _output;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherConfigurator"/> class.
        /// </summary>
        /// <param name="publisherConfig">The publisher config.</param>
        public PublisherConfigurator(PublisherConfig publisherConfig)
        {
            _publisherConfig = publisherConfig;
        }

        /// <summary>
        /// Creates the bus publisher.
        /// </summary>
        /// <returns></returns>
        public BusPublisher CreateBusPublisher()
        {
            LoadIdentification().CreateOutput();
            return PublisherFactory.CreateBusPublisher(_output, ConfigurationMessageBuilder.MessageBuilder);
        }

        /// <summary>
        /// Creates the output.
        /// </summary>
        /// <returns></returns>
        private PublisherConfigurator CreateOutput()
        {
            var uri = new Uri(_publisherConfig.Output.Uri);
            _output = AgentGatewayFactory.CreateOutputGateway(_identification, uri, _publisherConfig.Output.Transport);
            return this;
        }

        /// <summary>
        /// Loads the identification.
        /// </summary>
        /// <returns></returns>
        private PublisherConfigurator LoadIdentification()
        {
            _identification = new Identification
            {
                Id = _publisherConfig.Identification.Id,
                Type = _publisherConfig.Identification.Type
            };
            return this;
        }
    }
}