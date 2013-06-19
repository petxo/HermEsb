using System;
using HermEsb.Core.Communication;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Messages;
using HermEsb.Core.Serialization;
using HermEsb.Logging;

namespace HermEsb.Core.Gateways.Agent
{
    /// <summary>
    /// 
    /// </summary>
    public static class AgentGatewayFactory
    {
        /// <summary>
        /// Creates the agent input gateway.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="receiverEndPoint">The receiver end point.</param>
        /// <param name="maxReijections">The max reijections.</param>
        /// <returns></returns>
        public static AgentInputGateway<TMessage> CreateInputGateway<TMessage>(IReceiverEndPoint receiverEndPoint, int maxReijections) where TMessage : IMessage
        {
            return new AgentInputGateway<TMessage>(receiverEndPoint, maxReijections)
            {
                Logger = LoggerManager.Instance
            };

        }

        /// <summary>
        /// Creates the input gateway.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="transportType">The supported transports.</param>
        /// <param name="numberOfParallelTasks">The number of parallel tasks.</param>
        /// <param name="maxReijections">The max reijections.</param>
        /// <returns></returns>
        public static AgentInputGateway<TMessage> CreateInputGateway<TMessage>(Uri uri, 
                                                                                TransportType transportType, 
                                                                                int numberOfParallelTasks, int maxReijections) 
            where TMessage : IMessage
        {
            return CreateInputGateway<TMessage>(EndPointFactory.CreateReceiverEndPoint(uri, transportType, numberOfParallelTasks), maxReijections);
        }


        /// <summary>
        /// Creates the agent input gateway.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="receiverEndPoint">The receiver end point.</param>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        /// <param name="maxReijections">The max reijections.</param>
        /// <returns></returns>
        public static AgentInputGateway<TMessage> CreateInputGateway<TMessage>(IReceiverEndPoint receiverEndPoint, 
                                IDataContractSerializer dataContractSerializer, 
                                int maxReijections) 
            where TMessage : IMessage
        {
            return new AgentInputGateway<TMessage>(receiverEndPoint, dataContractSerializer, maxReijections)
            {
                Logger = LoggerManager.Instance
            };

        }

        /// <summary>
        /// Creates the input gateway.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="transportType">The supported transports.</param>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        /// <param name="numberOfParallelTasks">The number of parallel tasks.</param>
        /// <param name="maxReijections">The max reijections.</param>
        /// <returns></returns>
        public static AgentInputGateway<TMessage> CreateInputGateway<TMessage>(Uri uri, 
                                                                               TransportType transportType, 
                                                                               IDataContractSerializer dataContractSerializer, 
                                                                               int numberOfParallelTasks, int maxReijections) 
            where TMessage : IMessage
        {
            return CreateInputGateway<TMessage>(EndPointFactory.CreateReceiverEndPoint(uri, transportType, numberOfParallelTasks),
                                                dataContractSerializer,
                                                maxReijections);
        }

        /// <summary>
        /// Creates the output gateway.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="senderEndPoint">The sender end points.</param>
        /// <returns></returns>
        public static AgentOutputGateway CreateOutputGateway(Identification identification, ISenderEndPoint senderEndPoint)
        {
            return new AgentOutputGateway(identification, senderEndPoint)
            {
                Logger = LoggerManager.Instance
            };

        }


        /// <summary>
        /// Creates the output gateway.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="transportType">The supported transports.</param>
        /// <returns></returns>
        public static AgentOutputGateway CreateOutputGateway(Identification identification, Uri uri, TransportType transportType)
        {
            return new AgentOutputGateway(identification, EndPointFactory.CreateSenderEndPoint(uri, transportType))
            {
                Logger = LoggerManager.Instance
            };

        }

        /// <summary>
        /// Creates the output gateway.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="senderEndPoint">The sender end points.</param>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        /// <returns></returns>
        public static AgentOutputGateway CreateOutputGateway(Identification identification, ISenderEndPoint senderEndPoint, IDataContractSerializer dataContractSerializer)
        {
            return new AgentOutputGateway(identification, senderEndPoint, dataContractSerializer)
            {
                Logger = LoggerManager.Instance
            };

        }

        /// <summary>
        /// Creates the output gateway.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="transportType">The supported transports.</param>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        /// <returns></returns>
        public static AgentOutputGateway CreateOutputGateway(Identification identification,
                                                    Uri uri,
                                                    TransportType transportType,
                                                    IDataContractSerializer dataContractSerializer)
        {
            return new AgentOutputGateway(identification, EndPointFactory.CreateSenderEndPoint(uri, transportType), dataContractSerializer)
            {
                Logger = LoggerManager.Instance
            };

        }
    }
}