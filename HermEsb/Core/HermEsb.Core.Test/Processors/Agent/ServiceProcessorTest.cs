using HermEsb.Core.Gateways;
using HermEsb.Core.Handlers;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Builders;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Agent;
using HermEsb.Core.Processors.Agent.Reinjection;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Processors.Agent
{
    [TestFixture]
    class ServiceProcessorTest
    {
        private Mock<Identification> _mockIdentification;
        private Mock<IInputGateway<IMessage, MessageHeader>> _mockInputGateway;
        private Mock<IHandlerRepository> _mockHandlerRepository;
        private ServiceProcessor _subject;
        private Mock<IOutputGateway<IMessage>> _mockOutputGateway;
        private Mock<IMessageBuilder> _messageBuilder;

        [SetUp]
        public void Setup()
        {
            _mockIdentification = new Mock<Identification>();
            _mockInputGateway = new Mock<IInputGateway<IMessage, MessageHeader>>();
            _mockHandlerRepository = new Mock<IHandlerRepository>();
            _messageBuilder = new Mock<IMessageBuilder>();

            _subject = ProcessorsFactory.CreateServiceProcessor(_mockIdentification.Object, 
                                _mockInputGateway.Object, 
                                _mockHandlerRepository.Object, 
                                _messageBuilder.Object) as ServiceProcessor;

            _mockOutputGateway = new Mock<IOutputGateway<IMessage>>();
        }

        [Test(Description="Cuando se configura el OutputGateWay cambia el estado del ServiceProcessor")]
        public void ConfigureOutputGateway_True()
        {
            _subject = new ServiceProcessor(_mockIdentification.Object, 
                                            _mockInputGateway.Object, 
                                            _mockHandlerRepository.Object,
                                            _messageBuilder.Object,
                                            ReinjectionEngineFactory.CreateDefaultEngine(_mockInputGateway.Object));
            _subject.ConfigureOutputGateway(_mockOutputGateway.Object);
            Assert.AreEqual(_subject.Status, ProcessorStatus.Configured);
        }

        [Test]
        public void GetMessageTypes_True()
        {
            _subject.GetMessageTypes();
            _mockHandlerRepository.Verify(x=>x.GetMessageTypes());

        }

    }
}
