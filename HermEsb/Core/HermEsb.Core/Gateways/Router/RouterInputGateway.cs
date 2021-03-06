using System;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Messages;
using HermEsb.Core.Serialization;

namespace HermEsb.Core.Gateways.Router
{
    /// <summary>
    /// 
    /// </summary>
    public class RouterInputGateway : AbstractInputGateway<byte[], RouterHeader>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouterInputGateway"/> class.
        /// </summary>
        /// <param name="receiverEndPoint">The receiver end point.</param>
        /// <param name="maxReijections">The max reijections.</param>
        public RouterInputGateway(IReceiverEndPoint receiverEndPoint, int maxReijections)
            : base(receiverEndPoint, maxReijections)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouterInputGateway"/> class.
        /// </summary>
        /// <param name="receiverEndPoint">The receiver end point.</param>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        /// <param name="maxReijections">The max reijections.</param>
        public RouterInputGateway(IReceiverEndPoint receiverEndPoint, IDataContractSerializer dataContractSerializer, int maxReijections)
            : base(receiverEndPoint, dataContractSerializer, maxReijections)
        {
        }

        /// <summary>
        /// Processes the received message.
        /// </summary>
        /// <param name="serializedMessage">The serialized message.</param>
        protected override void ProcessReceivedMessage(byte[] serializedMessage)
        {
            var routerHeader = MessageBusParser.GetHeaderFromBytes(serializedMessage);
            InvokeOnMessage(serializedMessage, serializedMessage, routerHeader);
            InvokeReceivedMessage(routerHeader.BodyType, serializedMessage.Length, routerHeader.CreatedAt);
        }
    }
}