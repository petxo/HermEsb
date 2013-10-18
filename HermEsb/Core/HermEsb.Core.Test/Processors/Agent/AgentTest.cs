using System;
using System.Collections.Generic;
using HermEsb.Core.Gateways;
using HermEsb.Core.Handlers;
using HermEsb.Core.Ioc;
using HermEsb.Core.Messages;
using HermEsb.Core.Processors;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Processors.Agent
{
    [TestFixture]
    internal class AgentTest
    {
        private Mock<Identification> _mockIdentification;
        private Mock<IInputGateway<IMessage, MessageHeader>> _mockInputGateway;
        private Mock<IHandlerRepository> _mockHandlerRepository;
        private Mock<IMessageHandler<IMessage>> _mockHandler;
        private OutputGatewayEventHandlerArgs<IMessage, MessageHeader> _outputGatewayEventHandlerArgs;
        private Mock<IIoc> _iocMock;
        private Mock<IOutputGateway<IMessage>> _mockOutputGateway;
        private AgentFake _subject;

        [SetUp]
        public void Setup()
        {
            _mockIdentification = new Mock<Identification>();
            _mockInputGateway = new Mock<IInputGateway<IMessage, MessageHeader>>();
            _mockHandlerRepository = new Mock<IHandlerRepository>();
            _subject = new AgentFake(_mockIdentification.Object, _mockInputGateway.Object, _mockHandlerRepository.Object);

            _mockHandler = new Mock<IMessageHandler<IMessage>>();
            var mockHeader = new Mock<MessageHeader>();
            mockHeader.SetupAllProperties();

            _outputGatewayEventHandlerArgs = new OutputGatewayEventHandlerArgs<IMessage, MessageHeader>
                                                 {
                                                     Message = new Mock<IMessage>().Object,
                                                     Header = mockHeader.Object
                                                 };
            _iocMock = new Mock<IIoc>();

            _mockOutputGateway = new Mock<IOutputGateway<IMessage>>();
        }

        [Test]
        public void Identification_True()
        {
            Assert.AreEqual(_mockIdentification.Object,_subject.Identification);
        }

        [Test]
        public  void InputGateway_True()
        {
            Assert.AreEqual(_mockInputGateway.Object,_subject.InputGateway);
        }

        [Test]
        public void ReceiverEndPoint_True()
        {
            Assert.AreEqual(_mockInputGateway.Object.ReceiverEndPoint,_subject.ReceiverEndPoint);
        }

        [Test]
        public void OutputGateway_True()
        {
            _subject.OutputGateway = _mockOutputGateway.Object;
            Assert.AreEqual(_mockOutputGateway.Object, _subject.OutputGateway);
        }

        [Test]
        public void HandlerRepository_True()
        {
            Assert.AreEqual(_mockHandlerRepository.Object, _subject.HandlerRepository);
        }


        [Test]
        public void OnMessageReceived_True()
        {
            var recivedRaised = false;

            _mockHandlerRepository.Setup(x => x.GetHandlersByMessage(It.IsAny<Type>())).Returns(
                    new List<Type>()
                );

            _subject.OnMessageReceived += (sender, args) => recivedRaised = true;

            _mockInputGateway.Raise(m => m.OnMessage += null, _outputGatewayEventHandlerArgs);

            System.Threading.SpinWait.SpinUntil(() => recivedRaised);


            Assert.IsTrue(recivedRaised);
        }

        [Test]
        public void OnCreatedHandler_True()
        {
            var recivedRaised = false;

            _mockHandlerRepository.Setup(x => x.GetHandlersByMessage(It.IsAny<Type>())).Returns(
                    new List<Type>(new List<Type>(new[] { new Mock<IMessage>().Object.GetType() }))
                );

            ContextManager.Create(_iocMock.Object);
            _iocMock.Setup(x => x.Resolve(It.IsAny<Type>())).Returns(_mockHandler.Object);
            
            _subject.OnCreatedHandler += (sender, args) => recivedRaised = true;

            _mockInputGateway.Raise(m => m.OnMessage += null, _outputGatewayEventHandlerArgs);

            System.Threading.SpinWait.SpinUntil(() => recivedRaised);

            Assert.IsTrue(recivedRaised);
        }

        [Test]
        public void OnDestoyedHandler_True()
        {
            var recivedRaised = false;

            _mockHandlerRepository.Setup(x => x.GetHandlersByMessage(It.IsAny<Type>())).Returns(
                    new List<Type>(new List<Type>(new[] { new Mock<IMessage>().Object.GetType() }))
                );

            ContextManager.Create(_iocMock.Object);
            _iocMock.Setup(x => x.Resolve(It.IsAny<Type>())).Returns(_mockHandler.Object);

            _subject.OnDestoyedHandler += (sender, args) => recivedRaised = true;

            _mockInputGateway.Raise(m => m.OnMessage += null, _outputGatewayEventHandlerArgs);

            System.Threading.SpinWait.SpinUntil(() => recivedRaised);

            Assert.IsTrue(recivedRaised);
        }

        [Test(Description = "Verificar que cuando se inicia el Agent también se inicia el InputGateway")]
        public void Start_InputGateWayStart_True()
        {
            _subject.OutputGateway=_mockOutputGateway.Object;
            _subject.Start();
            _mockInputGateway.Verify(x => x.Start());
            _subject.Stop();
        }

        [Test(Description = "Verificar que cuando se inicia el Agent su estado es Started")]
        public void Start_StatusStarted_True()
        {
            _subject.OutputGateway=_mockOutputGateway.Object;
            _subject.Start();
            Assert.IsTrue(_subject.Status == ProcessorStatus.Started);
        }

        [Test]
        public void OnStart_True()
        {
            var recivedRaised = false;

            _subject.OutputGateway=_mockOutputGateway.Object;
            _subject.Stop();
            _subject.OnStart += (sender, args) => recivedRaised = true;
            _subject.Start();

            System.Threading.SpinWait.SpinUntil(() => recivedRaised);

            Assert.IsTrue(recivedRaised);
        }

        [Test]
        public void OnStop_True()
        {
            var recivedRaised = false;

            _subject.OutputGateway=_mockOutputGateway.Object;
            _subject.Start();
            _subject.OnStop += (sender, args) => recivedRaised = true;
            _subject.Stop();

            System.Threading.SpinWait.SpinUntil(() => recivedRaised);

            Assert.IsTrue(recivedRaised);
        }

        [Test]
        public void OnMessageSent_IMesage_No_HasCurrentContext_True()
        {
            var recivedRaised = false;

            _subject.OnMessageSent += (sender, args) => recivedRaised = true;
            _subject.OutputGateway=_mockOutputGateway.Object;

            ContextManager.Create(_iocMock.Object);
            _iocMock.Setup(x => x.Resolve(It.IsAny<Type>())).Returns(_mockHandler.Object);

            _subject.Publish(new Mock<IMessage>().Object);

            System.Threading.SpinWait.SpinUntil(() => recivedRaised);

            Assert.IsTrue(recivedRaised);
        }

        [Test]
        public void OnMessageSent_RequestMessage_True()
        {
            var recivedRaised = false;

            _subject.OnMessageSent += (sender, args) => recivedRaised = true;
            _subject.OutputGateway = _mockOutputGateway.Object;

            ContextManager.Create(_iocMock.Object);
            _iocMock.Setup(x => x.Resolve(It.IsAny<Type>())).Returns(_mockHandler.Object);

            _subject.Publish(new Mock<IMessage>().Object);

            System.Threading.SpinWait.SpinUntil(() => recivedRaised);

            Assert.IsTrue(recivedRaised);
        }

        [Test]
        public void OnMessageSent_IMesage_HasCurrentContext_True()
        {
            var recivedRaised = false;

            _subject.OnMessageSent += (sender, args) => recivedRaised = true;
            _subject.OutputGateway = _mockOutputGateway.Object;

            ContextManager.Create(_iocMock.Object);
            _iocMock.Setup(x => x.Resolve(It.IsAny<Type>())).Returns(_mockHandler.Object);

            ContextManager.Instance.CreateNewContext();
            _subject.Publish(new Mock<IMessage>().Object);

            System.Threading.SpinWait.SpinUntil(() => recivedRaised);

            Assert.IsTrue(recivedRaised);
        }

        [Test(Description = "Cuando se recibe un mensaje,se debe invocar al handler correspondiente")]
        public void InvokeMethodHandle_True()
        {
            _mockHandlerRepository.Setup(x => x.GetHandlersByMessage(It.IsAny<Type>())).Returns(
                    new List<Type>(new List<Type>(new[] { new Mock<IMessage>().Object.GetType() }))
                );
            
            ContextManager.Create(_iocMock.Object);
            _iocMock.Setup(x => x.Resolve(It.IsAny<Type>())).Returns(_mockHandler.Object);
            
            _mockInputGateway.Raise(m => m.OnMessage += null, _outputGatewayEventHandlerArgs);
            _mockHandler.Verify(x => x.HandleMessage(It.IsAny<IMessage>()));
        }


        [Test(Description = "Cuando se hace un dispose se debe hacer un dispose de todas las dependencias IDisposables")]
        public void Dispose_DisposeAll_True()
        {
            _subject.OutputGateway = _mockOutputGateway.Object;
            _subject.Dispose();

            _mockOutputGateway.Verify(x => x.Dispose());
            _mockInputGateway.Verify(x => x.Dispose());
        }

    }
}
