using System;
using HermEsb.Core.Communication;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Serialization;
using HermEsb.Logging;

namespace HermEsb.Core.Gateways.Router
{
    /// <summary>
    /// 
    /// </summary>
    public static class RouterGatewayFactory
    {
        /// <summary>
        /// Creates the agent input gateway.
        /// </summary>
        /// <param name="receiverEndPoint">The receiver end point.</param>
        /// <param name="maxReijections">The max reijections.</param>
        /// <returns></returns>
        public static RouterInputGateway CreateInputGateway(IReceiverEndPoint receiverEndPoint, int maxReijections)
        {
            return new RouterInputGateway(receiverEndPoint, maxReijections)
                       {
                           Logger = LoggerManager.Instance
                       };
        }


        /// <summary>
        /// Creates the input gateway.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="transportType">The supported transports.</param>
        /// <param name="numberOfParallelTasks">The number of parallel tasks.</param>
        /// <param name="maxReijections">The max reijections.</param>
        /// <returns></returns>
        public static RouterInputGateway CreateInputGateway(Uri uri, TransportType transportType, int numberOfParallelTasks, int maxReijections)
        {
            return new RouterInputGateway(EndPointFactory.CreateReceiverEndPoint(uri, transportType, numberOfParallelTasks), maxReijections)
            {
                Logger = LoggerManager.Instance
            };

        }

        /// <summary>
        /// Creates the agent input gateway.
        /// </summary>
        /// <param name="receiverEndPoint">The receiver end point.</param>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        /// <param name="maxReijections">The max reijections.</param>
        /// <returns></returns>
        public static RouterInputGateway CreateInputGateway(IReceiverEndPoint receiverEndPoint, 
                                                            IDataContractSerializer dataContractSerializer, 
                                                            int maxReijections)
        {
            return new RouterInputGateway(receiverEndPoint, dataContractSerializer, maxReijections)
            {
                Logger = LoggerManager.Instance
            };

        }

        /// <summary>
        /// Creates the input gateway.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="transportType">The supported transports.</param>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        /// <param name="numberOfParallelTasks">The number of parallel tasks.</param>
        /// <param name="maxReijections">The max reijections.</param>
        /// <returns></returns>
        public static RouterInputGateway CreateInputGateway(Uri uri, 
                                                            TransportType transportType, 
                                                            IDataContractSerializer dataContractSerializer,
                                                            int numberOfParallelTasks, int maxReijections)
        {
            return CreateInputGateway(EndPointFactory.CreateReceiverEndPoint(uri, transportType, numberOfParallelTasks), 
                                        dataContractSerializer,
                                        maxReijections);

        }


        /// <summary>
        /// Creates the output gateway.
        /// </summary>
        /// <param name="senderEndPoint">The sender end point.</param>
        /// <returns></returns>
        public static RouterOutputGateway CreateOutputGateway(ISenderEndPoint senderEndPoint)
        {
            return new RouterOutputGateway(senderEndPoint)
            {
                Logger = LoggerManager.Instance
            };

        }

        /// <summary>
        /// Creates the output gateway.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="transportType">The supported transports.</param>
        /// <returns></returns>
        public static RouterOutputGateway CreateOutputGateway(Uri uri, TransportType transportType)
        {
            return new RouterOutputGateway(EndPointFactory.CreateSenderEndPoint(uri, transportType))
            {
                Logger = LoggerManager.Instance
            };

        }

        /// <summary>
        /// Creates the output gateway.
        /// </summary>
        /// <param name="senderEndPoint">The sender end points.</param>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        /// <returns></returns>
        public static RouterOutputGateway CreateOutputGateway(ISenderEndPoint senderEndPoint, IDataContractSerializer dataContractSerializer)
        {
            return new RouterOutputGateway(senderEndPoint, dataContractSerializer)
            {
                Logger = LoggerManager.Instance
            };

        }

        /// <summary>
        /// Creates the output gateway.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="transportType">The supported transports.</param>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        /// <returns></returns>
        public static RouterOutputGateway CreateOutputGateway(Uri uri, TransportType transportType, IDataContractSerializer dataContractSerializer)
        {
            return new RouterOutputGateway(EndPointFactory.CreateSenderEndPoint(uri, transportType), dataContractSerializer)
            {
                Logger = LoggerManager.Instance
            };

        }
    }
}