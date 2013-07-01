using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Monitoring;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Monitoring.Frequences;
using HermEsb.Core.Processors;
using Moq;
using NUnit.Framework;
using System;

namespace HermEsb.Core.Test.Monitoring
{
    [TestFixture]
    public class MonitorTest
    {
        private Mock<IController> _controller;
        private Mock<IOutputGateway<IMessage>> _outputGateway;
        private Mock<IFrequenceHelper> _frequenceHelper;
        private Mock<IProcessor> _monitorableProcessor;
        private Mock<IBusInfo> _businfo;
        private Mock<IEndPoint> _processorEndpoit;
        private Mock<IEndPoint> _controllerEndpoit;

        [SetUp]
        public void Setup()
        {
            _controller = new Mock<IController>();
            _monitorableProcessor = new Mock<IProcessor>();
            _outputGateway = new Mock<IOutputGateway<IMessage>>();
            _frequenceHelper = new Mock<IFrequenceHelper>();
            _businfo = new Mock<IBusInfo>();
            _processorEndpoit = new Mock<IEndPoint>();
            _controllerEndpoit = new Mock<IEndPoint>();

            _businfo.Setup(x => x.Identification).Returns(new Identification());
            _processorEndpoit.Setup(x => x.Uri).Returns(new Uri("http://someUrl"));
            _controllerEndpoit.Setup(x => x.Uri).Returns(new Uri("http://someUrl"));

            _monitorableProcessor.Setup(x => x.Identification).Returns(new Identification());
            _monitorableProcessor.Setup(x => x.JoinedBusInfo).Returns(_businfo.Object);
            _monitorableProcessor.Setup(x => x.ReceiverEndPoint).Returns(_processorEndpoit.Object);

            _controller.Setup(x => x.ReceiverEndPoint).Returns(_controllerEndpoit.Object);
            _controller.SetupProperty(x => x.Processor,_monitorableProcessor.Object);

            _frequenceHelper.Setup(f => f.GetFrequence(It.IsAny<FrequenceLevel>())).Returns(60);

            MonitorFactory.Create(_frequenceHelper.Object);
        }

        /// <summary>
        /// Creates the monitor_ valid monitor_ assert true.
        /// </summary>
        [Test]
        public void CreateMonitor_ValidMonitor_AssertTrue()
        {
            var monitor = MonitorFactory.Instance.Create(_controller.Object, 
                                                     _outputGateway.Object,
                                                     new[] { "Mrwesb.Core.Test.dll" });

            Assert.NotNull(monitor);
        }

        [Test]
        public void StartMonitor_ValidMonitor_AssertTrue()
        {
            var monitor = MonitorFactory.Instance.Create(_controller.Object,
                                                     _outputGateway.Object,
                                                     new[] { "Mrwesb.Core.Test.dll" });


            monitor.Start();



            Assert.NotNull(monitor);
        }

        [Test]
        public void StartMonitor_SendInputQueuesMessage_AssertTrue()
        {
            var monitor = MonitorFactory.Instance.Create(_controller.Object,
                                                     _outputGateway.Object,
                                                     new[] { "Mrwesb.Core.Test.dll" });


            monitor.Start();

            _outputGateway.Verify(x => x.Send(It.IsAny<InputQueueMessage>()));
        }

        [Test]
        public void StartMonitor_InvoqueOnStart_AssertTrue()
        {
            var monitor = MonitorFactory.Instance.Create(_controller.Object,
                                                     _outputGateway.Object,
                                                     new[] { "Mrwesb.Core.Test.dll" });


            bool started = false;
            monitor.OnStart += (sender, args) => started = true;
            monitor.Start();

            Assert.IsTrue(started);
        }

        [Test]
        public void StopMonitor_InvoqueOnStop_AssertTrue()
        {
            var monitor = MonitorFactory.Instance.Create(_controller.Object,
                                                     _outputGateway.Object,
                                                     new[] { "Mrwesb.Core.Test.dll" });


            bool stoped = false;
            monitor.OnStop += (sender, args) => stoped = true;
            monitor.Start();
            monitor.Stop();

            Assert.IsTrue(stoped);
        }

    }
}