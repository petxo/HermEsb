using System;
using System.Text;
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
    public class RouterOutputGatewayTest
    {
        private Mock<ISenderEndPoint> _senderEndPoint;

        [SetUp]
        public void SetUp()
        {
            _senderEndPoint = new Mock<ISenderEndPoint>();
        }


        [Test]
        public void SendMessageAssertTrue()
        {
            var jsonDataContractSerializer = new JsonDataContractSerializer();

            var messageFake = new MessageFake { CreatedAt = DateTime.Now.Date };
            var messageBus = MessageBusFactory.Create(new Identification(), messageFake, jsonDataContractSerializer);

            var message = string.Empty;

            _senderEndPoint.Setup(s => s.Send(It.IsAny<string>(),It.IsAny<int>())).Callback<string,int>((s,p) => message = s);

            var outputGateway = RouterGatewayFactory.CreateOutputGateway(_senderEndPoint.Object);
            var serialize = jsonDataContractSerializer.Serialize(messageBus);
            outputGateway.Send(Encoding.UTF8.GetBytes(serialize));
            Assert.AreEqual(serialize, message);
        }
    }
}