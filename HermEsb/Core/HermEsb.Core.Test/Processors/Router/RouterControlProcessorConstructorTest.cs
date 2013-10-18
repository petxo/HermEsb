using HermEsb.Core.Gateways;
using HermEsb.Core.Handlers;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Builders;
using HermEsb.Core.Messages.Control;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Router;
using HermEsb.Core.Processors.Router.Subscriptors;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Processors.Router
{
    [TestFixture]
    public class RouterControlProcessorConstructorTest
    {
        private Mock<ISubscriptorsHelper> _subscriptonsHelper;
        private Mock<Identification> _identification;
        private Mock<IInputGateway<IControlMessage, MessageHeader>> _inputGateway;
        private Mock<IHandlerRepository> _handlerRepository;
        private Mock<IMessageBuilder> _messageBuilder;

        [SetUp]
        public void SetUp()
        {
            _subscriptonsHelper = new Mock<ISubscriptorsHelper>();
            _identification = new Mock<Identification>();
            _inputGateway = new Mock<IInputGateway<IControlMessage, MessageHeader>>();
            _handlerRepository = new Mock<IHandlerRepository>();
            _messageBuilder = new Mock<IMessageBuilder>();
        }

        [Test(Description = "Cuando se instancia RouterControlProcesso su Status es Initializing")]
        public void Contructor_Status_Initializing_True()
        {
            var subject = new RouterControlProcessor(_identification.Object,
                                                 _inputGateway.Object,
                                                 _handlerRepository.Object,
                                                 _subscriptonsHelper.Object,
                                                 _messageBuilder.Object);

            Assert.IsTrue(subject.Status == ProcessorStatus.Initializing);

            subject.Processor = new Mock<IProcessor>().Object;

            Assert.IsTrue(subject.Status == ProcessorStatus.Configured);
        }

        [Test(Description = "Cuando se asigna un Processor el status de  RouterControlProcessor es Configured")]
        public void Processor_Status_Configured_True()
        {
            var subject = new RouterControlProcessor(_identification.Object,
                                                 _inputGateway.Object,
                                                 _handlerRepository.Object,
                                                 _subscriptonsHelper.Object,
                                                 _messageBuilder.Object);

            subject.Processor = new Mock<IProcessor>().Object;

            Assert.IsTrue(subject.Status == ProcessorStatus.Configured);
        }

        [Test(Description = "Cuando status de  RouterControlProcessor es Configured se cargan los Suscriptores")]
        public void Configured_LoadStoredSubscriptors_True()
        {

            var subject = new RouterControlProcessor(_identification.Object,
                                                 _inputGateway.Object,
                                                 _handlerRepository.Object,
                                                 _subscriptonsHelper.Object,
                                                 _messageBuilder.Object);

            subject.Processor = new Mock<IProcessor>().Object;

            _subscriptonsHelper.Verify(x => x.LoadStoredSubscriptors(It.IsAny<Identification>()));
        }
    }
}