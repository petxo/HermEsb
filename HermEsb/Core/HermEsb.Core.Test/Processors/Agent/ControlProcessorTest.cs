using HermEsb.Core.Gateways;
using HermEsb.Core.Handlers;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Control;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Agent;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Processors.Agent
{
    [TestFixture]    
    class ControlProcessorTest
    {
        private Mock<Identification> _mockIdentification;
        private Mock<IInputGateway<IControlMessage, MessageHeader>> _mockInputGateway;
        private Mock<IOutputGateway<IControlMessage>> _mockOutputGateway;
        private Mock<IHandlerRepository> _mockHandlerRepository;
        private ControlProcessor _subject;

        [SetUp]
        public void Setup()
        {
            _mockIdentification = new Mock<Identification>();
            _mockInputGateway = new Mock<IInputGateway<IControlMessage, MessageHeader>>();
            _mockOutputGateway = new Mock<IOutputGateway<IControlMessage>>();
            _mockHandlerRepository = new Mock<IHandlerRepository>();

            _subject = ProcessorsFactory.CreateControlProcessor(_mockIdentification.Object, _mockInputGateway.Object,
                _mockOutputGateway.Object, _mockHandlerRepository.Object) as ControlProcessor;
        }

        [Test(Description = "Cuando se inicia el ControlProcessor se deben purgar las colas")]
        public void Start_InputGateWayPurgue_True()
        {
            _subject.Start();
            _mockInputGateway.Verify(x => x.Purge());
        }

        [Test(Description = "Cuando se inicia el ControlProcessor se debe iniciar también el InputGateWay")]
        public void Start_InputGateWayStart_True()
        {
            _subject.Start();
            _mockInputGateway.Verify(x => x.Start());
        }

        [Test(Description = "Cuando se inicia el ControlProcessor se debe iniciar también el Monitor")]
        public void Start_MonitorStart_True()
        {
            var mock = new Mock<IMonitor>();
            _subject.Monitor = mock.Object;
            _subject.Start();

            mock.Verify(x => x.Start());
        }

        [Test(Description = "Cuando se inicia el ControlProcessor se debe notificar mediante OnStart")]
        public void Start_OnStart_True()
        {
            var started = false;
            _subject.OnStart += (sender, args) => started = true;
            _subject.Start();

            Assert.IsTrue(started);
        }

        [Test(Description = "Cuando se detiene el ControlProcessor se debe detener el InputGateWay")]
        public void Stop_InputGateWayStop_True()
        {
            _subject.Start();
            _subject.Stop();
            _mockInputGateway.Verify(x => x.Stop());
        }

        [Test(Description = "Cuando se detiene el ControlProcessor se debe detener el Monitor")]
        public void Stop_MonitorStop_True()
        {
            var mock = new Mock<IMonitor>();
            _subject.Monitor = mock.Object;
            _subject.Start();
            _subject.Stop();

            mock.Verify(x => x.Stop());
        }

        [Test(Description = "Cuando se detiene el ControlProcessor se debe notificar mediante OnStop")]
        public void Stop_OnStop_True()
        {
            var stoped = false;
            _subject.OnStop += (sender, args) => stoped = true;

            _subject.Start();
            _subject.Stop();

            Assert.IsTrue(stoped);
        }





    }
}
