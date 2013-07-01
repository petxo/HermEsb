using HermEsb.Core.Controller.Messages;
using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Router;
using HermEsb.Core.Processors.Router.Outputs;
using HermEsb.Core.Processors.Router.Subscriptors;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Processors.Router
{
    [TestFixture]
    public class RouterProcessorTest
    {
        private Mock<IInputGateway<MessageBus>> _mockInputGateway;
        private Mock<IRouterOutputHelper> _mockRouterOutputHelper;
        private Mock<Identification> _mockIdentification;
        private RouterProcessor _subject;
        private Identification _identification;

        [SetUp]
        public void Setup()
        {
            _mockInputGateway = new Mock<IInputGateway<MessageBus>>();
            _mockIdentification = new Mock<Identification>();
            _mockRouterOutputHelper = new Mock<IRouterOutputHelper>();

            _subject = ProcessorsFactory.CreateRouterProcessor(_mockIdentification.Object,
                                                               _mockInputGateway.Object,
                                                               _mockRouterOutputHelper.Object) as RouterProcessor;
            _identification = new Identification { Id = "Test", Type = "Test_Type" };
        }

        [Test(Description = "Verificar que cuando se inicia el RouterProcess también se inicia el InputGateway")]
        public void Start_InputGateWayStart_True()
        {
            _subject.Start();
            _mockInputGateway.Verify(x => x.Start());
            _subject.Stop();
        }

        [Test(Description = "Verificar que cuando se inicia el RouterProcess su estado es Started")]
        public void Start_StatusStarted_True()
        {
            _subject.Start();
            Assert.IsTrue(_subject.Status == ProcessorStatus.Started);
        }

        [Test(Description = "Verificar que cuando se para el RouterProcess también se para el InputGateway")]
        public void Stop_InputGateWayStop_True()
        {
            _subject.Start();
            _subject.Stop();
            _mockInputGateway.Verify(x => x.Stop());
        }

        [Test(Description = "Verificar que cuando se para el RouterProcess su estado es Stopped")]
        public void Stop_StatusStopped_True()
        {
            _subject.Start();
            _subject.Stop();
            Assert.IsTrue(_subject.Status == ProcessorStatus.Stopped);
        }

        [Test(Description = "Verificar que cuando se recibe un mensaje del InputGateway se realiza un Publish mediante el RouterOutputHelper")]
        public void MessageRecived_RouterOutputHelperPublish_True()
        {
            var sendedRaised = false;
            var recivedRaised = false;

            _subject.OnMessageReceived += (sender, args) => recivedRaised = true;
            _subject.OnMessageSent += (sender, args) => sendedRaised = true;

            _mockInputGateway.Raise(m => m.OnMessage += null, new OutputGatewayEventHandlerArgs<MessageBus>
                {
                    Header = new MessageHeader(){BodyType = "test"}, Message = new MessageBus(), SerializedMessage = string.Empty
                });

            _mockRouterOutputHelper.Verify(x => x.Publish(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()));

             System.Threading.SpinWait.SpinUntil(() => sendedRaised && recivedRaised);

             Assert.IsTrue(sendedRaised);
             Assert.IsTrue(recivedRaised);
        }

        [Test(Description = "Verificar que se lanza el evento OnMessageReceived cuando se recibe un mensaje del InputGateway")]
        public void MessageRecived_OnMessageReceivedRaised_True()
        {
            var recivedRaised = false;

            _subject.OnMessageReceived += (sender, args) => recivedRaised = true;

            _mockInputGateway.Raise(m => m.OnMessage += null, new OutputGatewayEventHandlerArgs<MessageBus>());

            System.Threading.SpinWait.SpinUntil(() => recivedRaised);

            Assert.IsTrue(recivedRaised);
        }

        [Test(Description = "Verificar que se lanza el evento OnMessageSended cuando se recibe un mensaje del InputGateway")]
        public void MessageRecived_OnMessageSendedRaised_True()
        {
            var sendedRaised = false;

            _subject.OnMessageSent += (sender, args) => sendedRaised = true;

            _mockInputGateway.Raise(m => m.OnMessage += null, new OutputGatewayEventHandlerArgs<MessageBus>());

            System.Threading.SpinWait.SpinUntil(() => sendedRaised);

            Assert.IsTrue(sendedRaised);
        }

        [Test(Description = "Verificar que se llama al metodo Subscribe de RouterOutputHelper")]
        public void Subscribe_IRouterOutputHelper_Subscribe_True()
        {
            var mockOutputGateway = new Mock<IOutputGateway<string>>();

            _subject.Subscribe(SubscriptionKeyMessageFactory.CreateFromType(typeof(IMessage)).ToSubscriptorKey(), _identification, mockOutputGateway.Object);

            _mockRouterOutputHelper.Verify(x => x.Subscribe(It.IsAny<SubscriptionKey>(), _identification, It.IsAny<IOutputGateway<string>>()));
        }

        [Test(Description = "Verificar que se llama al metodo Unsubscribe de RouterOutputHelper")]
        public void Unsubscribe_IRouterOutputHelper_Unsubscribe_True()
        {
            var mockOutputGateway = new Mock<IOutputGateway<string>>();

            _subject.Unsubscribe(SubscriptionKeyMessageFactory.CreateFromType(typeof(IMessage)).ToSubscriptorKey(), _identification, mockOutputGateway.Object);

            _mockRouterOutputHelper.Verify(x => x.Unsubscribe(It.IsAny<SubscriptionKey>(), _identification, It.IsAny<IOutputGateway<string>>()));
        }

        [Test(Description = "Verificar que se llama al metodo GetMessageTypes de RouterOutputHelper")]
        public void GetMessageTypes_IRouterOutputHelper_GetMessageTypes_True()
        {
            _subject.GetMessageTypes();
            _mockRouterOutputHelper.Verify(x => x.GetMessageTypes());
        }

        [Test(Description = "Cuando se hace un dispose se debe hacer un dispose de todas las dependencias IDisposables")]
        public void Dispose_DisposeAll_True()
        {
            _subject.Dispose();
            _mockRouterOutputHelper.Verify(x => x.Dispose());
            _mockInputGateway.Verify(x => x.Dispose());
        }
    }
}