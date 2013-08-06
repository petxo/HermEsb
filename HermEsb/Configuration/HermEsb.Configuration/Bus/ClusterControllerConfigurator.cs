using HermEsb.Configuration.Publishers;
using HermEsb.Configuration.Services;
using HermEsb.Core;
using HermEsb.Core.Clustering;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.ErrorHandling;
using HermEsb.Core.Gateways;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Messages;
using System;

namespace HermEsb.Configuration.Bus
{
    public class ClusterControllerConfigurator
    {
        private readonly ClusterControllerConfig _clusterControllerConfig;
        private readonly Identification _identification;
        private IOutputGateway<IMessage> _output;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherConfigurator" /> class.
        /// </summary>
        /// <param name="clusterControllerConfig">The cluster controller config.</param>
        /// <param name="identification">The identification.</param>
        public ClusterControllerConfigurator(ClusterControllerConfig clusterControllerConfig, Identification identification)
        {
            _clusterControllerConfig = clusterControllerConfig;
            _identification = identification;
            
        }

        /// <summary>
        /// Creates the bus publisher.
        /// </summary>
        /// <returns></returns>
        public IClusterController Create()
        {
            CreateOutput();
            var endPointInfoInput = EndPointFactory.CreateEndPointInfo(new Uri(_clusterControllerConfig.ClusterInput.Uri),
                                               _clusterControllerConfig.ClusterInput.Transport);
            return ClusterControllerFactory.Create(_output, _identification, endPointInfoInput);
        }

        /// <summary>
        /// Creates the output.
        /// </summary>
        /// <returns></returns>
        private ClusterControllerConfigurator CreateOutput()
        {
            var uri = new Uri(_clusterControllerConfig.ClusterControlOutput.Uri);
            _output = AgentGatewayFactory.CreateOutputGateway(_identification, uri, 
                                                _clusterControllerConfig.ClusterControlOutput.Transport);
            return this;
        }
    }
}