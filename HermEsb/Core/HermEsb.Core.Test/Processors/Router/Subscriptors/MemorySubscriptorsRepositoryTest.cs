using System.Linq;
using HermEsb.Core.Gateways;
using HermEsb.Core.Messages.Control;
using HermEsb.Core.Processors.Router.Subscriptors;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Processors.Router.Subscriptors
{
    [TestFixture]
    public class MemorySubscriptorsRepositoryTest
    {
        private MemorySubscriptorsRepository _subject;
        private Mock<IOutputGateway<string>> _mockGateWayMessageBus;
        private Mock<IOutputGateway<IControlMessage>> _mockGateWayControl;

        [SetUp]
        public void SetUp()
        {
            _subject = new MemorySubscriptorsRepository();
            _mockGateWayMessageBus = new Mock<IOutputGateway<string>>();
            _mockGateWayControl = new Mock<IOutputGateway<IControlMessage>>();
        }

        [Test]
        public void Add_Get_Susbscriptor()
        {
            var identification = new Identification { Id = "A", Type = "B" };
            var subscriptor = new Subscriptor
                                  {
                                      Service = identification,
                                      ServiceInputControlGateway = _mockGateWayControl.Object,
                                      ServiceInputGateway = _mockGateWayMessageBus.Object
                                  };

            using (var subject = new MemorySubscriptorsRepository())
            {
                subject.Add(subscriptor);

                var subs = subject.Get(identification);
                Assert.IsTrue(subs == subscriptor);
            }
        }

        [Test]
        public void Remove_Get_Susbscriptor()
        {
            var identification = new Identification { Id = "A", Type = "B" };
            var subscriptor = new Subscriptor
                                  {
                                      Service = identification,
                                      ServiceInputControlGateway = _mockGateWayControl.Object,
                                      ServiceInputGateway = _mockGateWayMessageBus.Object
                                  };

            using (var subject = new MemorySubscriptorsRepository())
            {
                subject.Add(subscriptor);
                Assert.IsTrue(subject.Contains(identification));

                var subs = subject.Get(identification);
                Assert.IsTrue(subs == subscriptor);

                subject.Remove(subscriptor);
                Assert.IsFalse(subject.Contains(identification));

                subs = subject.Get(identification);
                Assert.IsTrue(subs == null);
            }
        }

        [Test]
        public void Contains_Susbscriptor()
        {
            var identification = new Identification { Id = "A", Type = "B" };
            var subscriptor = new Subscriptor
            {
                Service = identification,
                ServiceInputControlGateway = _mockGateWayControl.Object,
                ServiceInputGateway = _mockGateWayMessageBus.Object
            };

            using (var subject = new MemorySubscriptorsRepository())
            {
                subject.Add(subscriptor);
                Assert.IsTrue(subject.Contains(identification));
            }
        }

        [Test]
        public void GetAll_Susbscriptor()
        {
            var identification = new Identification { Id = "A", Type = "B" };
            var subscriptor = new Subscriptor
            {
                Service = identification,
                ServiceInputControlGateway = _mockGateWayControl.Object,
                ServiceInputGateway = _mockGateWayMessageBus.Object
            };

            var identification2 = new Identification { Id = "C", Type = "C" };
            var subscriptor2 = new Subscriptor
            {
                Service = identification2,
                ServiceInputControlGateway = _mockGateWayControl.Object,
                ServiceInputGateway = _mockGateWayMessageBus.Object
            };

            using (var subject = new MemorySubscriptorsRepository())
            {
                subject.Add(subscriptor);
                subject.Add(subscriptor2);

                var subscriptors = subject.GetAll();

                Assert.IsTrue(subscriptors.Count() == 2);
            }
        }

        [Test]
        public void Dispose_Susbscriptor()
        {
            var identification = new Identification { Id = "A", Type = "B" };
            var subscriptor = new Subscriptor
            {
                Service = identification,
                ServiceInputControlGateway = _mockGateWayControl.Object,
                ServiceInputGateway = _mockGateWayMessageBus.Object
            };

            using (var subject = new MemorySubscriptorsRepository())
            {
                subject.Add(subscriptor);
            }

            _mockGateWayMessageBus.Verify(x => x.Dispose());
            _mockGateWayControl.Verify(x => x.Dispose());
        }
    }
}