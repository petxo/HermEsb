using System.Text;
using System.Threading;
using HermEsb.Core.Communication.Channels.RabbitMq;
using Moq;
using RabbitMQ.Client.Events;

namespace HermEsb.Core.Test.Communication.Channels.RabbitChannels
{
    public class DadoUnMensajeCuandoArrancaElChannel
    {
        private RabbitReceiverChannel _subject;
        private Mock<IRabbitWrapper> _rabbitWrapper;
        private Mock<IQueueBasicConsumer> _basicConsumer;

        private Mock<ISharedQueue> _sharedQueue;

        //[SetUp]
        public void Setup()
        {
            _rabbitWrapper = new Mock<IRabbitWrapper>();
            _basicConsumer = new Mock<IQueueBasicConsumer>();

            _subject = new RabbitReceiverChannel(100, _rabbitWrapper.Object);
            _sharedQueue = new Mock<ISharedQueue>();

            _basicConsumer.SetupGet(q => q.Queue).Returns(_sharedQueue.Object);
            //_rabbitWrapper.SetupGet(rw => rw.Channel).Returns(_model.Object);
            //_rabbitWrapper.SetupGet(rw => rw.BasicConsumer).Returns(_basicConsumer.Object);
        }

        //[Test]
        public void EntoncesSeRecibeElMensaje()
        {
            var message = "Test";
            FluentAssert.Test.Given(message)
                .When(StartChannel)
                .Expect(CallToDequeue)
                .Should(DequeueMessageFromConsumer)
                .Verify();

        }

        private void StartChannel()
        {
            var waiting = true;
            _subject.Start();
            _subject.OnReceivedMessage += (sender, e) => waiting = false;

            while (waiting)
            {
                Thread.Sleep(500);
            }

            _subject.Stop();

        }

        private void CallToDequeue()
        {
            var result = new BasicDeliverEventArgs
            {
                Body = Encoding.UTF8.GetBytes("test")
            };
            int calls = 0;
            _sharedQueue.Setup(sq => sq.Dequeue(10000, out result)).Returns(() => true)
                                                                   .Callback(() =>
                                                                   {
                                                                       if (calls > 0) Thread.Sleep(2000);
                                                                       calls++;
                                                                   });
        }

        private void DequeueMessageFromConsumer()
        {
            _rabbitWrapper.VerifyAll();
            _sharedQueue.VerifyAll();
        }
    }
}