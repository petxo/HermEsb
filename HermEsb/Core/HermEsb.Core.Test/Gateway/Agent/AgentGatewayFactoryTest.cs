using System;
using HermEsb.Core.Communication;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Messages;
using HermEsb.Core.Serialization;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Gateway.Agent
{
    [TestFixture]
    public class AgentGatewayFactoryTest
    {

        private Mock<IReceiverEndPoint> _fakeIReceiverEndPoint;
        private Uri _uri;
        private TransportType _transportType;
        private Mock<IDataContractSerializer> _fakeDataContractSerializer;
        private Mock<Identification> _fakeIdentification;
        private Mock<ISenderEndPoint> _fakeISenderEndPoint;

        [SetUp]
        public void Setup()
        {
            _fakeIReceiverEndPoint = new Mock<IReceiverEndPoint>();
            _uri = new Uri("http://someUrl");
            //_transportType = new TransportType();
            _transportType = TransportType.Msmq;
            _fakeDataContractSerializer = new Mock<IDataContractSerializer>();
            _fakeIdentification = new Mock<Identification>();
            _fakeISenderEndPoint = new Mock<ISenderEndPoint>();
        }

        #region "CreateInputGateway"

        [Test]
        public void CreateInputGatewayFromReceiverEndPoint()
        {
            var aig = AgentGatewayFactory.CreateInputGateway<IMessage>(_fakeIReceiverEndPoint.Object, 5);

            Assert.IsInstanceOf(typeof(AgentInputGateway<IMessage>), aig);
            Assert.AreEqual(aig.ReceiverEndPoint.Uri, _fakeIReceiverEndPoint.Object.Uri);
            Assert.AreEqual(aig.ReceiverEndPoint.Transport, _fakeIReceiverEndPoint.Object.Transport);
        }

        [Test]
        public void CreateInputGatewayFromUriAndTransportTypeMsmq()
        {
            var aig = AgentGatewayFactory.CreateInputGateway<IMessage>(_uri, _transportType, 100, 5);

            Assert.IsInstanceOf(typeof(AgentInputGateway<IMessage>), aig);
            Assert.AreEqual(aig.ReceiverEndPoint.Uri, _uri);
            Assert.AreEqual(aig.ReceiverEndPoint.Transport, _transportType);
        }

        [Test]
        public void CreateInputGatewayFromReceiverEndPointAndIDataContractSerializer()
        {
            var aig = AgentGatewayFactory.CreateInputGateway<IMessage>(_fakeIReceiverEndPoint.Object, _fakeDataContractSerializer.Object, 5);

            Assert.IsInstanceOf(typeof(AgentInputGateway<IMessage>), aig);
            Assert.AreEqual(aig.ReceiverEndPoint.Uri, _fakeIReceiverEndPoint.Object.Uri);
            Assert.AreEqual(aig.ReceiverEndPoint.Transport, _fakeIReceiverEndPoint.Object.Transport);
        }

        [Test]
        public void CreateInputGatewayFromUriAndTransportTypeMsmqAndIDataContractSerializer()
        {
            var aig = AgentGatewayFactory.CreateInputGateway<IMessage>(_uri, _transportType, _fakeDataContractSerializer.Object, 100, 5);
            Assert.IsInstanceOf(typeof(AgentInputGateway<IMessage>), aig);
            Assert.AreEqual(aig.ReceiverEndPoint.Uri, _uri);
            Assert.AreEqual(aig.ReceiverEndPoint.Transport, _transportType);

        }

        #endregion


        #region "CreateOutputGateway"

        [Test]
        public void CreateOutputGatewayFromIIdentificationAndSenderEndPoint()
        {
            var aog = AgentGatewayFactory.CreateOutputGateway(_fakeIdentification.Object, _fakeISenderEndPoint.Object);

            Assert.IsInstanceOf(typeof(AgentOutputGateway), aog);
            Assert.AreEqual(aog.GetIdentification(), _fakeIdentification.Object);
            Assert.AreEqual(aog.GetSenderEndPoint().Uri, _fakeISenderEndPoint.Object.Uri);
            Assert.AreEqual(aog.GetSenderEndPoint().Transport, _fakeISenderEndPoint.Object.Transport);
        }


        [Test]
        public void CreateOutputGatewayFromIIdentificationAndUriAndTransportTypeMsmq()
        {
            var aog = AgentGatewayFactory.CreateOutputGateway(_fakeIdentification.Object, _uri, _transportType);

            Assert.IsInstanceOf(typeof(AgentOutputGateway), aog);
            Assert.AreEqual(aog.GetIdentification(), _fakeIdentification.Object);
            Assert.AreEqual(aog.GetSenderEndPoint().Uri, _uri);
            Assert.AreEqual(aog.GetSenderEndPoint().Transport, _transportType);
        }

        [Test]
        public void CreateOutputGatewayFromIIdentificationAndISenderEndPointAndIDataContractSerializer()
        {
            var aog = AgentGatewayFactory.CreateOutputGateway(_fakeIdentification.Object, _fakeISenderEndPoint.Object, _fakeDataContractSerializer.Object);

            Assert.IsInstanceOf(typeof(AgentOutputGateway), aog);
            Assert.AreEqual(aog.GetIdentification(), _fakeIdentification.Object);
            Assert.AreEqual(aog.GetSenderEndPoint().Uri, _fakeISenderEndPoint.Object.Uri);
            Assert.AreEqual(aog.GetSenderEndPoint().Transport, _fakeISenderEndPoint.Object.Transport);
        }


        [Test]
        public void CreateOutputGatewayFromIIdentificationAndUriAndTransportTypeMsmqAndIDataContractSerializer()
        {
            var aog = AgentGatewayFactory.CreateOutputGateway(_fakeIdentification.Object, _uri, _transportType, _fakeDataContractSerializer.Object);
            Assert.IsInstanceOf(typeof(AgentOutputGateway), aog);
            Assert.AreEqual(aog.GetIdentification(), _fakeIdentification.Object);
            Assert.AreEqual(aog.GetSenderEndPoint().Uri, _uri);
            Assert.AreEqual(aog.GetSenderEndPoint().Transport, _transportType);
        }

        #endregion

    }
}
