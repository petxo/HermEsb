using System.Text;
using HermEsb.Core.Communication.Channels.RabbitMq;
using Moq;
using NUnit.Framework;
using RabbitMQ.Client;

namespace HermEsb.Core.Test.Communication.Channels.RabbitChannels
{
    public class DadoUnMensajeCuandoSeEnvia
    {
        private RabbitReceiverChannel _subject;
        private Mock<IRabbitWrapper> _rabbitWrapper;
        private Mock<IQueueBasicConsumer> _basicConsumer;
        private Mock<IBasicProperties> _basicProperties;

        private Mock<ISharedQueue> _sharedQueue;
        private Mock<IModel> _model;

        private PublicationAddress _publicationAddress;

        private byte[] _message;

        //[SetUp]
        public void Setup()
        {
            _rabbitWrapper = new Mock<IRabbitWrapper>();
            _basicConsumer = new Mock<IQueueBasicConsumer>();
            _basicProperties = new Mock<IBasicProperties>();
            _publicationAddress = new PublicationAddress("test", "test", "test");


            _subject = new RabbitReceiverChannel(100, _rabbitWrapper.Object);
            _sharedQueue = new Mock<ISharedQueue>();
            _model = new Mock<IModel>();

            _basicConsumer.SetupGet(q => q.Queue).Returns(_sharedQueue.Object);
            //_rabbitWrapper.SetupGet(rw => rw.Channel).Returns(_model.Object);
            //_rabbitWrapper.SetupGet(rw => rw.BasicConsumer).Returns(_basicConsumer.Object);
            //_rabbitWrapper.SetupGet(rw => rw.PublicationAddress).Returns(_publicationAddress);
        }

        //[Test]
        public void EntoncesElWrapperEnviaElMensaje()
        {
            var message = "Test";

            FluentAssert.Test.Given(message)
                .When(SendMessage)
                .Expect(WrapperConfiguration)
                .Should(ChannelPublishMessage)
                .Should(PriorityIsRight)
                .Should(MessageIsUtf8)
                .Verify();
        }

        private void SendMessage(string message)
        {
            _subject.Send(message, 0);
        }

        private void WrapperConfiguration()
        {
            _model.Setup(m => m.CreateBasicProperties()).Returns(_basicProperties.Object);
            _model.Setup(m => m.BasicPublish(_publicationAddress, _basicProperties.Object, It.IsAny<byte[]>()))
                .Callback<PublicationAddress, IBasicProperties, byte[]>((pa, bp, b) => _message = b);
        }

        private void ChannelPublishMessage()
        {
            _model.VerifyAll();
        }

        private void PriorityIsRight()
        {
            _basicProperties.VerifySet(bp => bp.Priority = 0);
        }

        private void MessageIsUtf8()
        {
            var strMessage = Encoding.UTF8.GetString(_message);
            Assert.AreEqual("Test", strMessage);
        }
    }
}