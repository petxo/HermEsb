using System;
using System.Text;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Builders;
using HermEsb.Core.Serialization;
using HermEsb.Core.Test.Fakes.Messages;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Gateway.Agent
{
    [TestFixture]
    public class AgentInputGatewayTest
    {
        private Mock<IReceiverEndPoint> _receiverEndPoint;

        [SetUp]
        public void Setup()
        {
            _receiverEndPoint = new Mock<IReceiverEndPoint>();
            MessageBuilderFactory.CreateDefaultBuilder();
        }

        [Test]
        public void ProcessReceivedMessageInputValidMessageAssertValid()
        {
            var jsonDataContractSerializer = new JsonDataContractSerializer();

            var messageFake = new MessageFake { CreatedAt = DateTime.Now.Date };
            var messageBus = MessageBusFactory.Create(new Identification(), messageFake, jsonDataContractSerializer);

            var inputGateway = AgentGatewayFactory.CreateInputGateway<IMessage>(_receiverEndPoint.Object, 5);

            var eventargs = new EventReceiverEndPointHandlerArgs
                                {
                                    Message = Encoding.UTF8.GetBytes(jsonDataContractSerializer.Serialize(messageBus))
                                };


            _receiverEndPoint.Setup(r => r.Start()).Raises(r => r.OnReceivedMessage += null,
                                                            new object[] { null, eventargs });

            IMessage messageReceived = null;
            inputGateway.OnMessage += (sender, args) => messageReceived = args.Message;


            inputGateway.Start();

            Assert.AreEqual(messageFake.CreatedAt, ((IMessageFake)messageReceived).CreatedAt);

        }
    }
}