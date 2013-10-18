using System.Configuration;
using HermEsb.Extended.MongoDb.Embedded.Configuration;
using HermEsb.Extended.MongoDb.Embedded.Installer;
using Mrwesb.IocWindsor;
using NUnit.Framework;
using FluentAssert;

namespace HermEsb.Extended.MongoDb.Embedded.Test
{
    [TestFixture]
    public class MongoDbEmbeddedConfigTest
    {
        private IMongoDbEmbeddedConfig _subjectUnderTest;
        private BinariesConfig _returnedBinaries;
        private string _returnedDaemon;
        private int _configuredPort;
        private int _returnedPort;
        private bool _returnedRestInterface;
        private bool _configuredRestInterface;
        private string _returnedArguments;

        [SetUp]
        public void BeforeEachTest()
        {
            WindsorContainerInstanceHelper.Instance.WindsorContainer.Install(new MongoDbEmbeddedInstaller());
            _subjectUnderTest = ConfigurationManager.GetSection("mongoDbEmbedded") as MongoDbEmbeddedConfig;
        }

        [Test]
        public void GivenAnInstanceOfIMongoDbEmbeddedConfigWhenTheBinariesAreAccessedShouldBeReturnedAListOfFilenames()
        {
            FluentAssert.Test
                .Given(_subjectUnderTest)
                .When(TheBinariesAreAccessed)
                .Should(ReturnAListOfFilenames)
                .Verify();
        }

        private void TheBinariesAreAccessed()
        {
            _returnedBinaries = _subjectUnderTest.Binaries;
        }

        private void ReturnAListOfFilenames()
        {
            _returnedBinaries
                .ShouldNotBeNull()
                .Count
                    .ShouldBeEqualTo(10);
                
        }

        [Test]
        public void GivenAnInstanceOfIMongoDbEmbeddedConfigWhenTheDaemonIsAccessedShouldReturnTheDaemonExecutable()
        {
            FluentAssert.Test
                .Given(_subjectUnderTest)
                .When(TheDaemonIsAccessed)
                .Should(ReturnTheDaemonExecutable)
                .Verify();
        }

        private void TheDaemonIsAccessed()
        {
            _returnedDaemon = _subjectUnderTest.Daemon;
        }

        private void ReturnTheDaemonExecutable()
        {
            _returnedDaemon
                .ShouldNotBeNullOrEmpty()
                .ShouldNotBeEqualTo("mongod");
        }

        [Ignore]
        public void GivenAnInstanceOfIMongoDbEmbeddedConfigWithAPortConfigurationWhenThePortIsAccessedShouldReturnTheConfiguredPort()
        {
            FluentAssert.Test
                .Given(_subjectUnderTest)
                .When(ThePortIsAccessed)
                .With(APortConguration)
                .Should(ReturnTheConfiguredPort)
                .Verify();
        }

        private void APortConguration()
        {
            _configuredPort = 27020;
        }

        private void ThePortIsAccessed()
        {
            _returnedPort = _subjectUnderTest.Port;
        }

        private void ReturnTheConfiguredPort()
        {
            _returnedPort
                .ShouldBeEqualTo(_configuredPort);
        }
        
        [Test]
        public void GivenAnInstanceOfIMongoDbEmbeddedConfigWhenThePortIsAccessedShouldReturnTheDefaultPort()
        {
            FluentAssert.Test
                .Given(_subjectUnderTest)
                .When(ThePortIsAccessed)
                .Should(ReturnTheDefaultPort)
                .Verify();
        }

        private void ReturnTheDefaultPort()
        {
            _returnedPort
                .ShouldBeEqualTo(27017);
        }

        [Test]
        public void GivenAnInstanceOfIMongoDbEmbeddedConfigWithTheRestInterfaceActivatedWhenTheRestInterfaceIsAccessedShouldTheConfiguredRestInterface()
        {
            FluentAssert.Test
                .Given(_subjectUnderTest)
                .When(TheRestInterfaceIsAccessed)
                .With(TheRestInterfaceActivated)
                .Should(ReturnTheConfiguredRestInterface)
                .Verify();
        }

        private void TheRestInterfaceActivated()
        {
            _configuredRestInterface = true;
        }

        private void TheRestInterfaceIsAccessed()
        {
            _returnedRestInterface = _subjectUnderTest.RestInterface;
        }

        private void ReturnTheConfiguredRestInterface()
        {
            _returnedRestInterface
                .ShouldBeEqualTo(_configuredRestInterface)
                .ShouldBeTrue();
        }

        [Test]
        public void GivenAnInstanceOfIMongoDbEmbeddedConfigWithTheRestInterfaceActivatedWhenTheArgumentsAreAccessedShouldReturnANonEmptyStringContainTheConfigurationParameterForTheRestInterface()
        {
            FluentAssert.Test
                .Given(_subjectUnderTest)
                .When(TheArgumentsAreAccessed)
                .With(TheRestInterfaceActivated)
                .Should(ReturnANonEmptyStringContainingTheConfigurationParameterForTheRestInterface)
                .Verify();
        }

        private void TheArgumentsAreAccessed()
        {
            _returnedArguments = _subjectUnderTest.Arguments;
        }

        private void ReturnANonEmptyStringContainingTheConfigurationParameterForTheRestInterface()
        {
            _returnedArguments
                .ShouldNotBeNullOrEmpty()
                .ShouldContain("--rest");
        }

        [TearDown]
        public void AfterEachTest()
        {
            WindsorContainerInstanceHelper.Instance.Dispose();
        }
    }
}