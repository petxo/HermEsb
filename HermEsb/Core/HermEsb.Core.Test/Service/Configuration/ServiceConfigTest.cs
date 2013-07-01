using NUnit.Framework;

namespace HermEsb.Core.Test.Service.Configuration
{
    [TestFixture]
    public class ServiceConfigTest
    {
        [Test]
        public void ServiceConfigLoadFromFileOkTest()
        {
            //var serviceConfig = ConfigurationHelper.CurrentInstance.ConfigurationRepository.Get<MrwesbServiceConfig>();
            //Assert.IsNotNull(serviceConfig.Bus);
            //Assert.IsNotNull(serviceConfig.Bus.ControlInput);
            //Assert.AreEqual(@"\\localhost\private$\subscriptionQueue", serviceConfig.Bus.ControlInput.Uri);
            //Assert.AreEqual(TransportType.Msmq, serviceConfig.Bus.ControlInput.Transport);

            //Assert.IsNotNull(serviceConfig.ServiceProcessor);
            //Assert.IsNotNull(serviceConfig.ServiceProcessor.Input);
            //Assert.AreEqual(@"\\localhost\private$\serviceInputQueue", serviceConfig.ServiceProcessor.Input.Uri);
            //Assert.AreEqual(TransportType.Msmq, serviceConfig.ServiceProcessor.Input.Transport);
            //Assert.IsNotNull(serviceConfig.ServiceProcessor.HandlersAssemblies);
            //Assert.IsNotEmpty(serviceConfig.ServiceProcessor.HandlersAssemblies);
            //Assert.IsTrue(serviceConfig.ServiceProcessor.HandlersAssemblies.Count == 3);

            //Assert.IsNotNull(serviceConfig.ControlProcessor);
            //Assert.IsNotNull(serviceConfig.ControlProcessor.Input);
            //Assert.AreEqual(@"\\localhost\private$\serviceControlQueue", serviceConfig.ControlProcessor.Input.Uri);
            //Assert.AreEqual(TransportType.Msmq, serviceConfig.ControlProcessor.Input.Transport);
        }
    }
}