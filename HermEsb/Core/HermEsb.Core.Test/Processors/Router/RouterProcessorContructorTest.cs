using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Router.Outputs;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Processors.Router
{
    [TestFixture]
    public class RouterProcessorContructorTest
    {
        private Mock<IInputGateway<byte[], RouterHeader>> _mockInputGateway;
        private Mock<IRouterOutputHelper> _mockRouterOutputHelper;
        private Mock<Identification> _mockIdentification;

        [SetUp]
        public void Setup()
        {
            _mockInputGateway = new Mock<IInputGateway<byte[], RouterHeader>>();
            _mockIdentification = new Mock<Identification>();
            _mockRouterOutputHelper = new Mock<IRouterOutputHelper>();
        }

        [Test(Description = "Verificar que cuando se instancia el RouterProcessor su estado es Configured")]
        public void Constructor_State_Initializing_True()
        {
            var subject = ProcessorsFactory.CreateRouterProcessor(_mockIdentification.Object,
                                                                  _mockInputGateway.Object,
                                                                  _mockRouterOutputHelper.Object);

            Assert.IsTrue(subject.Status == ProcessorStatus.Configured);
        }
    }
}
