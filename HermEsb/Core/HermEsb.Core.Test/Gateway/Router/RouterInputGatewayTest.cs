using System;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Gateways.Router;
using HermEsb.Core.Messages;
using HermEsb.Core.Serialization;
using HermEsb.Core.Test.Fakes.Messages;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Gateway.Router
{
    [TestFixture]
    public class RouterInputGatewayTest
    {
        private Mock<IReceiverEndPoint> _receiverEndPoint;

        [SetUp]
        public void Setup()
        {
            _receiverEndPoint = new Mock<IReceiverEndPoint>();
        }

        [Test]
        public void ProcessReceivedMessageInputValidMessageAssertValid()
        {
            var jsonDataContractSerializer = new JsonDataContractSerializer();

            var messageFake = new MessageFake { CreatedAt = DateTime.Now.Date };
            var messageBus = MessageBusFactory.Create(new Identification(), messageFake, jsonDataContractSerializer);

            var inputGateway = RouterGatewayFactory.CreateInputGateway(_receiverEndPoint.Object, 5);

            var eventargs = new EventReceiverEndPointHandlerArgs
            {
                Message = jsonDataContractSerializer.Serialize(messageBus)
            };


            _receiverEndPoint.Setup(r => r.Start()).Raises(r => r.OnReceivedMessage += null, new object[] { null, eventargs });

            MessageBus messageReceived = null;
            inputGateway.OnMessage += (sender, args) => messageReceived = args.Message;


            inputGateway.Start();

            Assert.AreEqual(messageBus.Body, messageReceived.Body);
            Assert.AreEqual(messageBus.Header.Type, messageReceived.Header.Type);
            Assert.AreEqual(messageBus.Header.BodyType, messageReceived.Header.BodyType);

        }
    }
}