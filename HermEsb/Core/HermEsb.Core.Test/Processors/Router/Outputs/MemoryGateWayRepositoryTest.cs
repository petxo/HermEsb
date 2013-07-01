using System.Linq;
using HermEsb.Core.Controller.Messages;
using HermEsb.Core.Gateways;
using HermEsb.Core.Processors.Router.Outputs;
using HermEsb.Core.Processors.Router.Subscriptors;
using HermEsb.Core.Test.Fakes.Messages;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Processors.Router.Outputs
{
    [TestFixture]
    public class MemoryGateWayRepositoryTest
    {
        private Mock<IOutputGateway<string>> _senderMessageFake;
        private Mock<IOutputGateway<string>> _senderIMessageFake;
        private Mock<IOutputGateway<string>> _senderIMessage;
        private SubscriptionKey _subscriptionKey;
        private string _key;
        private Identification _identification;

        [SetUp]
        public void SetUp()
        {
            _identification = new Identification { Id = "Test", Type = "Test_Type" };
            _senderMessageFake = new Mock<IOutputGateway<string>>();
            _senderIMessage = new Mock<IOutputGateway<string>>();
            _senderIMessageFake = new Mock<IOutputGateway<string>>();
            _subscriptionKey = SubscriptionKeyMessageFactory.CreateFromType(typeof(MessageFake)).ToSubscriptorKey();
            _key = string.Format("{0},{1}", typeof(MessageFake).FullName, typeof(MessageFake).Assembly.GetName().Name);
        }

        [Test]
        public void AddSenderSimpleMessageTypeTest()
        {
            var senderRepository = new MemoryGatewaysRepository();

            senderRepository.AddSender(_subscriptionKey, _identification, _senderMessageFake.Object);

            var messageSenders = senderRepository.GetMessageSenders(_key);

            Assert.IsTrue(messageSenders.Count(messageSender => messageSender == _senderMessageFake.Object) > 0);
        }


        [Test]
        public void AddSenderBaseMessageTypeTest()
        {
            var senderRepository = new MemoryGatewaysRepository();

            senderRepository.AddSender(_subscriptionKey, _identification, _senderIMessage.Object);
            senderRepository.AddSender(SubscriptionKeyMessageFactory.CreateFromType(typeof(IMessageFake)).ToSubscriptorKey(), _identification, _senderIMessageFake.Object);


            var messageSenders = senderRepository.GetMessageSenders(_key);

            Assert.IsTrue(messageSenders.Count() == 2);
            Assert.IsTrue(messageSenders.Contains(_senderIMessage.Object));
            Assert.IsTrue(messageSenders.Contains(_senderIMessageFake.Object));
        }

        [Test]
        public void RemoveSenderSimpleMessageTypeTest()
        {
            var senderRepository = new MemoryGatewaysRepository();

            senderRepository.AddSender(_subscriptionKey, _identification, _senderMessageFake.Object);

            senderRepository.RemoveSender(_subscriptionKey, _identification, _senderMessageFake.Object);

            var messageSenders = senderRepository.GetMessageSenders(_key);

            Assert.IsTrue(messageSenders.Count(messageSender => messageSender == _senderMessageFake.Object) == 0);
        }
    }
}