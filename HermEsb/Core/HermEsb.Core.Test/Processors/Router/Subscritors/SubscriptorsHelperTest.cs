using System.Collections.Generic;
using HermEsb.Core.Gateways;
using HermEsb.Core.Messages.Control;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Router.Subscriptors;
using HermEsb.Core.Processors.Router.Subscriptors.Persisters;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Processors.Router.Subscritors
{
    [TestFixture]
    public class SubscriptorsHelperTest
    {
        private Mock<ISubscriptorsRepository> _subscriptorsRepository;
        private Mock<ISubscriptorsPersister> _subscriptorPersister;
        private Mock<IRouterController> _controller;
        private Mock<IProcessorFake> _processor;
        private Mock<IOutputGateway<IControlMessage>> _outputControlGateway;
        private Mock<IOutputGateway<byte[]>> _outputGateway;
        private Identification _identification;
        private Subscriptor _subscriptor;
        private SubscriptionKey _subscriptionKey;

        [SetUp]
        public void SetUp()
        {
            _subscriptorsRepository = new Mock<ISubscriptorsRepository>(MockBehavior.Strict);
            _subscriptorPersister = new Mock<ISubscriptorsPersister>(MockBehavior.Strict);
            _controller = new Mock<IRouterController>(MockBehavior.Strict);
            _processor = new Mock<IProcessorFake>(MockBehavior.Strict);
            _outputControlGateway = new Mock<IOutputGateway<IControlMessage>>(MockBehavior.Strict);
            _outputGateway = new Mock<IOutputGateway<byte[]>>(MockBehavior.Strict);

            _controller.SetupGet(c => c.Processor).Returns(_processor.Object);
            _identification = new Identification { Id = "Test", Type = "Test_Type" };
            _subscriptionKey = new SubscriptionKey
                {
                    Key = string.Format("{0},{1}", typeof(string).FullName, typeof(string).Assembly.GetName().Name)
                };

            _subscriptor = new Subscriptor()
                               {
                                   Service = new Identification { Id = "Service", Type = "Service" },
                                   ServiceInputControlGateway = _outputControlGateway.Object,
                                   ServiceInputGateway = _outputGateway.Object,
                                   SubcriptionTypes = new List<SubscriptionKey> { _subscriptionKey }
                               };

            _subscriptorsRepository.Setup(r => r.Dispose());

        }

        [Test]
        public void LoadStoredSubscriptors_ValidSubscriptors_AssertTrue()
        {

            _subscriptorPersister.Setup(p => p.GetSubscriptors(_identification)).Returns(new List<Subscriptor> { _subscriptor });
            _processor.Setup(p => p.Subscribe(_subscriptionKey, _subscriptor.Service, _outputGateway.Object));

            _subscriptorsRepository.Setup(r => r.Add(_subscriptor));

            using (var subscriptorsHelper = new SubscriptorsHelper(_subscriptorsRepository.Object, _subscriptorPersister.Object))
            {
                subscriptorsHelper.Controller = _controller.Object;
                subscriptorsHelper.LoadStoredSubscriptors(_identification);
            }

            _subscriptorPersister.VerifyAll();
            _processor.VerifyAll();
        }

        [Test]
        public void AddSubscriptors_ValidSubscriptors_AssertTrue()
        {
            _subscriptorPersister.Setup(p => p.Add(_subscriptor));
            _processor.Setup(p => p.Subscribe(_subscriptionKey, _subscriptor.Service, _outputGateway.Object));

            _subscriptorsRepository.Setup(r => r.Add(_subscriptor));


            using (var subscriptorsHelper = new SubscriptorsHelper(_subscriptorsRepository.Object, _subscriptorPersister.Object))
            {
                subscriptorsHelper.Controller = _controller.Object;
                subscriptorsHelper.Add(_subscriptor);
            }


            _subscriptorPersister.VerifyAll();
            _processor.VerifyAll();
        }

        [Test]
        public void RemoveSubscriptors_ValidSubscriptors_AssertTrue()
        {
            _subscriptorsRepository.Setup(r => r.Get(_subscriptor.Service)).Returns(_subscriptor);
            _subscriptorsRepository.Setup(r => r.Remove(_subscriptor));

            _processor.Setup(p => p.Unsubscribe(_subscriptionKey, _subscriptor.Service, _outputGateway.Object));
            _subscriptorPersister.Setup(p => p.Remove(_subscriptor.Service));

            _outputControlGateway.Setup(o => o.Dispose());
            _outputGateway.Setup(o => o.Dispose());

            using (var subscriptorsHelper = new SubscriptorsHelper(_subscriptorsRepository.Object, _subscriptorPersister.Object))
            {
                subscriptorsHelper.Controller = _controller.Object;
                subscriptorsHelper.Remove(_subscriptor.Service);
            }

            _subscriptorPersister.VerifyAll();
            _processor.VerifyAll();
        }
    }
}