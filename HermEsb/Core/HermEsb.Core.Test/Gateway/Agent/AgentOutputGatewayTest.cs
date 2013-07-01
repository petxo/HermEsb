using System;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Messages;
using HermEsb.Core.Serialization;
using HermEsb.Core.Test.Fakes.Messages;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Gateway.Agent
{
    [TestFixture]
    public class AgentOutputGatewayTest
    {
        private Mock<ISenderEndPoint> _senderEndPoint;
        private Identification _identification;

        [SetUp]
        public void SetUp()
        {
            _senderEndPoint =  new Mock<ISenderEndPoint>();
            _identification = new Identification();
        }


        [Test]
        public void SendMessageAssertTrue()
        {
            var jsonDataContractSerializer = new JsonDataContractSerializer();

            var messageFake = new MessageFake { CreatedAt = DateTime.Now.Date };
            var messageBus = MessageBusFactory.Create(new Identification(), messageFake, jsonDataContractSerializer);

            var message = string.Empty;

            _senderEndPoint.Setup(s => s.Send(It.IsAny<string>(), It.IsAny<int>())).Callback<string, int>((s,p) => message = s);

            var outputGateway = AgentGatewayFactory.CreateOutputGateway(_identification, _senderEndPoint.Object);

            outputGateway.Send(messageFake);

            var deserialized = jsonDataContractSerializer.Deserialize<MessageBus>(message);

            Assert.AreEqual(messageBus.Body, deserialized.Body);
            Assert.AreEqual(messageBus.Header.Type, deserialized.Header.Type);
            Assert.AreEqual(messageBus.Header.BodyType, deserialized.Header.BodyType);
        }
    }
}