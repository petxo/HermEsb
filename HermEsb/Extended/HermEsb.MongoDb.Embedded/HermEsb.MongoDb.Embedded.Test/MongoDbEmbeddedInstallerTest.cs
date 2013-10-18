using Castle.Windsor;
using FluentAssert;
using HermEsb.Extended.JobObjects.Installer;
using HermEsb.Extended.MongoDb.Embedded.Configuration;
using HermEsb.Extended.MongoDb.Embedded.Installer;
using NUnit.Framework;

namespace HermEsb.Extended.MongoDb.Embedded.Test
{
    [TestFixture]
    public class MongoDbEmbeddedInstallerTest
    {
        private IWindsorContainer _containerUnderTest;
        private IMongoBootstrapper _bootstrapperUnderTest;
        private IMongoDeployer _deployerUnderTest;
        private IMongoDbEmbeddedConfig _configUnderTest;
        private IEmbeddedResourceHelper _resourceUnderTest;
        private IBinaryConfig _binaryConfigUnderTest;

        [SetUp]
        public void BeforeEachTest()
        {            
            _containerUnderTest = new WindsorContainer();
            _containerUnderTest.Install(new JobObjectsInstaller());
            _containerUnderTest.Install(new MongoDbEmbeddedInstaller());
        }

        [Test]
        public void GivenAWindsorContainerInitializedWithMongoDbEmbeddedInstallerWhenItIsAskedForAnInstanceOfIMongoBootstrapperShouldReturnANotNullInstance()
        {
            FluentAssert.Test
                .Given(_containerUnderTest)
                .When(ItIsAskedToResolveAnInstanceOfIMongoBootstrapper)
                .Should(ReturnANotNullInstanceOfIMongoBootstrapper)
                .Verify();
        }

        private void ItIsAskedToResolveAnInstanceOfIMongoBootstrapper()
        {
            _bootstrapperUnderTest = _containerUnderTest.Resolve<IMongoBootstrapper>();
        }

        private void ReturnANotNullInstanceOfIMongoBootstrapper()
        {
            _bootstrapperUnderTest
                .ShouldNotBeNull();
        }

        [Test]
        public void GivenAWindsorContainerInitializedWithMongoDbEmbeddedInstallerWhenItIsAskedForAnInstanceOfIMongoDeployerShouldReturnANotNullInstance()
        {
            FluentAssert.Test
                .Given(_containerUnderTest)
                .When(ItIsAskedToResolveAnInstanceOfIMongoDeployer)
                .Should(ReturnANotNullInstanceOfIMongoDeployer)
                .Verify();
        }

        private void ItIsAskedToResolveAnInstanceOfIMongoDeployer()
        {
            _deployerUnderTest = _containerUnderTest.Resolve<IMongoDeployer>();
        }

        private void ReturnANotNullInstanceOfIMongoDeployer()
        {
            _deployerUnderTest
                .ShouldNotBeNull();
        }


        [Test]
        public void GivenAWindsorContainerInitializedWithMongoDbEmbeddedInstallerWhenItIsAskedForAnInstanceOfIBinaryConfigShouldReturnANotNullInstance()
        {
            FluentAssert.Test
                .Given(_containerUnderTest)
                .When(ItIsAskedToResolveAnInstanceOfIBinaryConfig)
                .Should(ReturnANotNullInstanceOfIBinaryConfig)
                .Verify();
        }

        private void ItIsAskedToResolveAnInstanceOfIBinaryConfig()
        {
            _binaryConfigUnderTest = _containerUnderTest.Resolve<IBinaryConfig>();
        }

        private void ReturnANotNullInstanceOfIBinaryConfig()
        {
            _binaryConfigUnderTest
                .ShouldNotBeNull();
        }

        [Test]
        public void GivenAWindsorContainerInitializedWithMongoDbEmbeddedInstallerWhenItIsAskedForAnInstanceOfIMongoDbEmbeddedConfigShouldReturnANotNullInstance()
        {
            FluentAssert.Test
                .Given(_containerUnderTest)
                .When(ItIsAskedToResolveAnInstanceOfIMongoDbEmbeddedConfig)
                .Should(ReturnANotNullInstanceOfIMongoDbEmbeddedConfig)
                .Verify();
        }

        private void ItIsAskedToResolveAnInstanceOfIMongoDbEmbeddedConfig()
        {
            _configUnderTest = _containerUnderTest.Resolve<IMongoDbEmbeddedConfig>();
        }

        private void ReturnANotNullInstanceOfIMongoDbEmbeddedConfig()
        {
            _configUnderTest
                .ShouldNotBeNull();
        }

        [Test]
        public void GivenAWindsorContainerInitializedWithMongoDbEmbeddedInstallerWhenItIsAskedForAnInstanceOfIEmbeddedResourceShouldReturnANotNullInstance()
        {
            FluentAssert.Test
                .Given(_containerUnderTest)
                .When(ItIsAskedToResolveAnInstanceOfIEmbeddedResult)
                .Should(ReturnANotNullInstanceOfIEmbeddedResult)
                .Verify();
        }

        private void ItIsAskedToResolveAnInstanceOfIEmbeddedResult()
        {
            _resourceUnderTest = _containerUnderTest.Resolve<IEmbeddedResourceHelper>();
        }

        private void ReturnANotNullInstanceOfIEmbeddedResult()
        {
            _resourceUnderTest
                .ShouldNotBeNull();
        }

        [TearDown]
        public void AfterEachTest()
        {            
            _containerUnderTest.Dispose();
        }
    }
}
