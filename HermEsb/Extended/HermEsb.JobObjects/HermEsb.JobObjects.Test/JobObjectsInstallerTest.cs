using Castle.Windsor;
using FluentAssert;
using HermEsb.Extended.JobObjects.Configuration;
using HermEsb.Extended.JobObjects.Installer;
using NUnit.Framework;

namespace HermEsb.Extended.JobObjects.Test
{
    [TestFixture]
    public class JobObjectsInstallerTest
    {
        private IWindsorContainer _containerUnderTest;
        private IJobObjectsConfig _configUnderTest;
        private IJobObjectConfig _jobObjectConfigUnderTest;

        [SetUp]
        public void BeforeEachTest()
        {
            _containerUnderTest = new WindsorContainer();
            _containerUnderTest.Install(new JobObjectsInstaller());
        }

        [Test]
        public void GivenAWindsorContaineInitializedWithJobObjectsInstallerWhenItIsAskedForAnInstanceOfIJobObjectsConfigShouldReturnANotNullInstance()
        {
            FluentAssert.Test
                .Given(_containerUnderTest)
                .When(ItIsAskedToResolveAnInstanceOfIJobObjectsConfig)
                .Should(ReturnANotNullInstanceOfIJobObjectsConfig)
                .Verify();
        }

        private void ItIsAskedToResolveAnInstanceOfIJobObjectsConfig()
        {
            _configUnderTest = _containerUnderTest.Resolve<IJobObjectsConfig>();
        }

        private void ReturnANotNullInstanceOfIJobObjectsConfig()
        {
            _configUnderTest
                .ShouldNotBeNull();
        }

        [Test]
        public void GivenAWindsorContaineInitializedWithJobObjectsInstallerWhenItIsAskedForAnInstanceOfIJobObjectConfigShouldReturnANotNullInstance()
        {
            FluentAssert.Test
                .Given(_containerUnderTest)
                .When(ItIsAskedToResolveAnInstanceOfIJobObjectConfig)
                .Should(ReturnANotNullInstanceOfIJobObjectConfig)
                .Verify();
        }

        private void ItIsAskedToResolveAnInstanceOfIJobObjectConfig()
        {
            _jobObjectConfigUnderTest = _containerUnderTest.Resolve<IJobObjectConfig>();
        }

        private void ReturnANotNullInstanceOfIJobObjectConfig()
        {
            _jobObjectConfigUnderTest
                .ShouldNotBeNull();
        }

        [TearDown]
        public void AfterEachTest()
        {
            _containerUnderTest.Dispose();
        }
    }
}
