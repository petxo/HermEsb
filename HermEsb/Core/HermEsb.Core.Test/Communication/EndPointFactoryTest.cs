using System;
using HermEsb.Core.Communication;
using HermEsb.Core.Communication.EndPoints;
using NUnit.Framework;

namespace HermEsb.Core.Test.Communication
{
    [TestFixture]
    public class EndPointFactoryTest
    {

        private Uri _uri;
        private TransportType _transportType;

        [SetUp]
        public void Setup()
        {
            _uri = new Uri("http://someUrl");
            //_transportType = new TransportType();
            _transportType = TransportType.Msmq;
        }

        [Test]
        public void CreateReceiverEndPointFromUriAndTransportTypeMsmq()
        {
            var ep = EndPointFactory.CreateReceiverEndPoint(_uri, _transportType, 100);

            Assert.IsInstanceOf(typeof(IReceiverEndPoint), ep);
            Assert.AreEqual(ep.Uri, _uri);
            Assert.AreEqual(ep.Transport, _transportType);
        }

        [Test]
        public void CreateSenderEndPointFromUriAndTransportTypeMsmq()
        {
            var ep = EndPointFactory.CreateSenderEndPoint(_uri, _transportType);

            Assert.IsInstanceOf(typeof(ISenderEndPoint), ep);
            Assert.AreEqual(ep.Uri, _uri);
            Assert.AreEqual(ep.Transport, _transportType);
        }

    }
}
