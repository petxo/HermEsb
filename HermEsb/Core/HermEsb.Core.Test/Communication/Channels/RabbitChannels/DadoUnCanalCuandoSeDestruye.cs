using HermEsb.Core.Communication.Channels;
using HermEsb.Core.Communication.Channels.RabbitMq;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Communication.Channels.RabbitChannels
{
    [TestFixture]
    public class DadoUnCanalCuandoSeDestruye
    {
        private RabbitReceiverChannel _subject;
        private Mock<IRabbitWrapper> _rabbitWrapper;

        [SetUp]
        public void Setup()
        {
            _rabbitWrapper = new Mock<IRabbitWrapper>();
            _subject = new RabbitReceiverChannel(100, _rabbitWrapper.Object);
        }

        [Test]
        public void EntoncesSeDestruyeElWrapper()
        {
            FluentAssert.Test.Given(_subject)
                .When(DisposeChannel)
                .Should(WrapperIsDisposed)
                .Verify();
        }

        private void DisposeChannel(IReceiverChannel receiverChannel)
        {
            receiverChannel.Dispose();
        }

        private void WrapperIsDisposed()
        {
            _rabbitWrapper.Verify(rw => rw.Dispose());
        }
    }
}