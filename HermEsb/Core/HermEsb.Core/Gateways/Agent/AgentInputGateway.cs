using System.Text;
using HermEsb.Core.Communication;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Builders;
using HermEsb.Core.Serialization;

namespace HermEsb.Core.Gateways.Agent
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public class AgentInputGateway<TMessage> : AbstractInputGateway<TMessage>
        where TMessage : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgentInputGateway&lt;TMessage&gt;"/> class.
        /// </summary>
        /// <param name="receiverEndPoint">The receiver end point.</param>
        /// <param name="maxReijections">The max reijections.</param>
        internal AgentInputGateway(IReceiverEndPoint receiverEndPoint, int maxReijections)
            : base(receiverEndPoint, maxReijections)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentInputGateway&lt;TMessage&gt;"/> class.
        /// </summary>
        /// <param name="receiverEndPoint">The receiver end point.</param>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        /// <param name="maxReijections">The max reijections.</param>
        internal AgentInputGateway(IReceiverEndPoint receiverEndPoint, IDataContractSerializer dataContractSerializer, int maxReijections)
            : base(receiverEndPoint, dataContractSerializer, maxReijections)
        {
        }

        /// <summary>
        /// Processes the received message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="serializedMessage">The serialized message.</param>
        protected override void ProcessReceivedMessage(MessageBus message, string serializedMessage)
        {
            Logger.Debug("OnMessageBusReceived");
            var split = message.Header.BodyType.Split(',');
            if (split.Length < 2)
            {
                Logger.Error(string.Format("El mensaje no tiene el bodytype correcto {0}", serializedMessage));
                return;
            }

            var bodyType = split[0];
            var assemblyName = split[1];

            var type = TypeFinder.GetType(bodyType, assemblyName);
            var body = MessageBuilder.Instance.CreateInstance(DataContractSerializer, type, message.Body,
                                                   Encoding.GetEncoding(message.Header.EncodingCodePage));

            InvokeOnMessage((TMessage)body, serializedMessage, message.Header);
        }
    }
}