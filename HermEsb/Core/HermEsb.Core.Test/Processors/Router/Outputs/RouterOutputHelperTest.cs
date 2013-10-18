using System;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;
using HermEsb.Core.Processors.Router.Outputs;
using HermEsb.Core.Processors.Router.Subscriptors;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Processors.Router.Outputs
{
    [TestFixture]
    public class RouterOutputHelperTest
    {
        private Mock<IGatewaysRepository> _mockGateWaysRepository;
        private RouterOutputHelper _subject;
        private Identification _identification;

        [SetUp]
        public void SetUp()
        {
            _mockGateWaysRepository = new Mock<IGatewaysRepository>();
            _subject = new RouterOutputHelper(_mockGateWaysRepository.Object);
            _identification = new Identification { Id = "Test", Type = "Test_Type" };
        }

        [Test(Description = "Cuando se suscribe un nuevo tipo se añade al repositorio")]
        public void Susbscribe_GateWayRepository_AddSender()
        {
            _subject.Subscribe(It.IsAny<SubscriptionKey>(), _identification, It.IsAny<IOutputGateway<byte[]>>());

            _mockGateWaysRepository.Verify(x => x.AddSender(It.IsAny<SubscriptionKey>(), _identification, It.IsAny<IOutputGateway<byte[]>>()));
        }

        [Test(Description = "Cuando se desuscribe un nuevo tipo se quita del repositorio")]
        public void Unsubscribe_GateWayRepository_RemoveSender()
        {
            _subject.Unsubscribe(It.IsAny<SubscriptionKey>(), _identification, It.IsAny<IOutputGateway<byte[]>>());

            _mockGateWaysRepository.Verify(x => x.RemoveSender(It.IsAny<SubscriptionKey>(), _identification, It.IsAny<IOutputGateway<byte[]>>()));
        }

        [Test(Description = "Cuando se realiza un publish se obtienen los OutputsGateWays del repositorio")]
        public void Publish_GateWayRepository_GetMessageSenders()
        {
            var message = new MessageBus
                              {
                                  Body = "A",
                                  Header = new MessageHeader
                                               {
                                                   BodyType = "B"
                                               }
                              };

            _subject.Publish(message);

            _mockGateWaysRepository.Verify(x => x.GetMessageSenders(It.IsAny<string>()));
        }

        [Test(Description = "Cuando se realiza un publish se obtienen los OutputsGateWays del repositorio y se realiza un send con los mismos")]
        public void Publish_GateWayRepository_Send()
        {
            var mockOutput = new Mock<IOutputGateway<byte[]>>();
            var mockEndPoint = new Mock<IEndPoint>();
            mockEndPoint.SetupGet(point => point.Uri).Returns(new Uri("http://localhost"));
            mockOutput.SetupGet(gateway => gateway.EndPoint).Returns(mockEndPoint.Object);

            _mockGateWaysRepository.Setup(x => x.GetMessageSenders(It.IsAny<string>())).Returns(new[] {mockOutput.Object});

            var message = new MessageBus
            {
                Body = "A",
                Header = new MessageHeader
                {
                    BodyType = "B"
                }
            };

            _subject.Publish(message);

            mockOutput.Verify(x => x.Send(It.IsAny<byte[]>(), It.IsAny<int>()));
        }

        [Test(Description = "Cuando se quieren obtener los tipos de mensajes registados se obtienen del repositorio")]
        public void GetMessageTypes_GateWayRepository_GetMessageTypes()
        {
            _subject.GetMessageTypes();
            _mockGateWaysRepository.Verify(x => x.GetMessageTypes());
        }

        [Test(Description = "Cuando se hace un dispose del RouterHelper se realiza también el repositorio")]
        public void Dispose_GateWayRepository_Dispose()
        {
            _subject.Dispose();
            _mockGateWaysRepository.Verify(x => x.Dispose());
        }
    }
}