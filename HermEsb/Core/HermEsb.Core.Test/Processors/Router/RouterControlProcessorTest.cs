using System;
using System.Collections.Generic;
using HermEsb.Core.Gateways;
using HermEsb.Core.Handlers;
using HermEsb.Core.Ioc;
using HermEsb.Core.Messages.Builders;
using HermEsb.Core.Messages.Control;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Router;
using HermEsb.Core.Processors.Router.Subscriptors;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Processors.Router
{
    [TestFixture]
    public class RouterControlProcessorTest
    {
        private RouterControlProcessor _subject;
        private Mock<ISubscriptorsHelper> _subscriptonsHelper;
        private Mock<Identification> _identification;
        private Mock<IInputGateway<IControlMessage>> _inputGateway;
        private Mock<IHandlerRepository> _handlerRepository;
        private Mock<IProcessor> _processor;
        private Mock<IMessageBuilder> _messageBuilder;

        [SetUp]
        public void SetUp()
        {
            _subscriptonsHelper = new Mock<ISubscriptorsHelper>();
            _identification = new Mock<Identification>();
            _inputGateway = new Mock<IInputGateway<IControlMessage>>();
            _handlerRepository = new Mock<IHandlerRepository>();
            _processor = new Mock<IProcessor>();
            _messageBuilder = new Mock<IMessageBuilder>();

            _subject = new RouterControlProcessor(_identification.Object,
                                                  _inputGateway.Object,
                                                  _handlerRepository.Object,
                                                  _subscriptonsHelper.Object,
                                                  _messageBuilder.Object) 
                                                  {Processor = _processor.Object};
        }

        [Test(Description = "Cuando se inicia el RouterControlProcessor se deben purgar las colas de control")]
        public void Start_InputGateWayPurgue_True()
        {
            _subject.Start();

            _inputGateway.Verify(x => x.Purge());
        }

        [Test(Description = "Cuando se inicia el RouterControlProcessor se debe iniciar también el InputGateWay")]
        public void Start_InputGateWayStart_True()
        {
            _subject.Start();

            _inputGateway.Verify(x => x.Start());
        }

        [Test(Description = "Cuando se inicia el RouterControlProcessor se debe iniciar también el Monitor")]
        public void Start_MonitorStart_True()
        {
            var mock = new Mock<IMonitor>();
            _subject.Monitor = mock.Object;

            _subject.Start();

            mock.Verify(x => x.Start());
        }

        [Test(Description = "Cuando se inicia el RouterControlProcessor se debe notificar mediante OnStart")]
        public void Start_OnStart_True()
        {
            var started = false;
            _subject.OnStart += (sender, args) => started = true;

            _subject.Start();

            Assert.IsTrue(started);
        }

        [Test(Description = "Cuando se detiene el RouterControlProcessor se debe detiener también el InputGateWay")]
        public void Stop_InputGateWayStop_True()
        {
            _subject.Start();
            _subject.Stop();
            _inputGateway.Verify(x => x.Stop());
        }

        [Test(Description = "Cuando se detiene el RouterControlProcessor se debe detiener también el Monitor")]
        public void Stop_MonitorStop_True()
        {
            var mock = new Mock<IMonitor>();
            _subject.Monitor = mock.Object;

            _subject.Start();
            _subject.Stop();

            mock.Verify(x => x.Stop());
        }

        [Test(Description = "Cuando se detiene el RouterControlProcessor se debe notificar mediante OnStop")]
        public void Stop_OnStop_True()
        {
            var stoped = false;
            _subject.OnStop += (sender, args) => stoped = true;

            _subject.Start();
            _subject.Stop();

            Assert.IsTrue(stoped);
        }

        [Test(Description = "Cuando se publica un mensaje con un identificador de servicio se debe enviar mediante el SubscriptionHelper")]
        public void Publish_With_Identification_SubscriptionHelper_Send_True()
        {
            var identification = new Mock<Identification>();
            var controlMessage = new Mock<IControlMessage>();

            _subject.Publish(identification.Object, controlMessage.Object);

            _subscriptonsHelper.Verify(x => x.Send(identification.Object, controlMessage.Object));
        }

        [Test(Description = "Cuando se publica un mensajese debe enviar mediante el SubscriptionHelper")]
        public void Publish_SubscriptionHelper_Send_True()
        {
            var controlMessage = new Mock<IControlMessage>();

            _subject.Publish(controlMessage.Object);

            _subscriptonsHelper.Verify(x => x.Send(controlMessage.Object));
        }

        [Test(Description = "Cuando se publica un mensaje se debe enviar mediante el SubscriptionHelper")]
        public void Replay_SubscriptionHelper_Send_True()
        {
            var identification = new Identification {Id = "A",Type = "B"};
            var replyMessage = new FakeReplayMessage();

            _subject.Reply(replyMessage);

            _subscriptonsHelper.Verify(x => x.Send(It.IsAny<IControlMessage>()));
        }

        [Test(Description = "Cuando se hace un dispose se debe hacer un dispose de todas las dependencias IDisposables")]
        public void Dispose_DisposeAll_True()
        {
           _subject.Dispose();
           _inputGateway.Verify(x => x.Dispose());
           _subscriptonsHelper.Verify(x => x.Dispose());
        }

        [Test(Description = "Cuando se recibe un mensaje, mediante HandlerRepository se obtiene el handler correspondiente")]
        public void OnMessageRecived_GetHandlersByMessage()
        {
            var outputGatewayEventHandlerArgs = new OutputGatewayEventHandlerArgs<IControlMessage>
                                                    {Message = new Mock<IControlMessage>().Object};

            _inputGateway.Raise(x => x.OnMessage += null, outputGatewayEventHandlerArgs);

            _handlerRepository.Verify(x => x.GetHandlersByMessage(It.IsAny<Type>()));
        }

        [Test(Description = "Cuando se recibe un mensaje,se obtiene la instancia del tipo de handler mediante el Ioc configurado")]
        public void OnMessageRecived_ResolveHandler_True()
        {
            var outputGatewayEventHandlerArgs = new OutputGatewayEventHandlerArgs<IControlMessage> { Message = new Mock<IControlMessage>().Object };
           
            _handlerRepository.Setup(x => x.GetHandlersByMessage(It.IsAny<Type>())).Returns(
                new List<Type>(new[] { new Mock<IControlMessage>().Object.GetType() }));

            var iocMock = new Mock<IIoc>();
            ContextManager.Create(iocMock.Object);

            iocMock.Setup(x => x.Resolve(It.IsAny<Type>())).Returns(new object());

            _inputGateway.Raise(x => x.OnMessage += null, outputGatewayEventHandlerArgs);

            iocMock.Verify(x => x.Resolve(It.IsAny<Type>()));
        }

        [Test(Description = "Cuando se recibe un mensaje,se debe invocar al handler correspondiente")]
        public void OnMessageRecived_InvokeMethodHandle_True()
        {
            var outputGatewayEventHandlerArgs = new OutputGatewayEventHandlerArgs<IControlMessage> { Message = new Mock<IControlMessage>().Object };

            _handlerRepository.Setup(x => x.GetHandlersByMessage(It.IsAny<Type>())).Returns(
                new List<Type>(new[] { new Mock<IControlMessage>().Object.GetType() }));

            var iocMock = new Mock<IIoc>();
            ContextManager.Create(iocMock.Object);

            var handler = new Mock<IMessageHandler<IControlMessage>>();

            iocMock.Setup(x => x.Resolve(It.IsAny<Type>())).Returns(handler.Object);

            _inputGateway.Raise(x => x.OnMessage += null, outputGatewayEventHandlerArgs);

            handler.Verify(x => x.HandleMessage(It.IsAny<IControlMessage>()));
        }
    }
}