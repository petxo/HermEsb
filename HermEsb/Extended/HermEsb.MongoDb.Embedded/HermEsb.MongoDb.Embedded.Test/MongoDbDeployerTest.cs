using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using FluentAssert;
using HermEsb.Extended.JobObjects.Installer;
using HermEsb.Extended.MongoDb.Embedded.Installer;
using Mrwesb.IocWindsor;
using NUnit.Framework;

namespace HermEsb.Extended.MongoDb.Embedded.Test
{
    [TestFixture]
    public class MongoDbDeployerTest
    {
        private IMongoDeployer _subjectUnderTest;
        private bool _returnedIsRunning;

        [SetUp]
        public void BeforeEachTest()
        {
            WindsorContainerInstanceHelper.Instance.WindsorContainer.Install(new MongoDbEmbeddedInstaller());
            WindsorContainerInstanceHelper.Instance.WindsorContainer.Install(new JobObjectsInstaller());
            _subjectUnderTest = WindsorContainerInstanceHelper.Instance.Resolve<IMongoDeployer>();
        }

        [Test]
        public void GivenAnIMongoDeployerWhenItIsAskedToDeployMongoDbShouldExistTheBinFolderWithTheMongoDbExecutablesInside()
        {
            FluentAssert.Test
                        .Given(_subjectUnderTest)
                        .When(ItIsAskedToDeployMongoDb)
                        .Should(ExistTheBinFolderWithTheMongoDbExecutablesInside)
                        .Verify();
        }

        private void ItIsAskedToDeployMongoDb()
        {
            _subjectUnderTest.Deploy();
        }

        private void ExistTheBinFolderWithTheMongoDbExecutablesInside()
        {
            Directory.Exists(@"MongoDb\x64")
                .ShouldBeTrue();
        }

        [Test]
        public void GivenAnIMongoDeployerWithTheMongoDbDeployedWhenItIsAskedToRunMongoDbShouldExistAProcessRunningContainingTheStringMongoInItsName()
        {
            FluentAssert.Test
                        .Given(_subjectUnderTest)
                        .When(ItIsAskedToRunMongoDb)
                        .With(TheMongoDbDeployed)
                        .Should(ExistAProcessRunningContainingTheStringMongoInItsName)
                        .Verify();
        }

        private void TheMongoDbDeployed()
        {
            _subjectUnderTest.Deploy();
        }

        private void ItIsAskedToRunMongoDb()
        {
            _subjectUnderTest.Run();
            Thread.Sleep(1000);
        }

        private void ExistAProcessRunningContainingTheStringMongoInItsName()
        {
            Process.GetProcesses().Any(p => p.ProcessName.Contains("mongod"))
                .ShouldBeTrue();
        }

        [Test]
        public void
        GivenAnIMongoDeployerWithTheMongoDbDeployedAndRunningWhenItIsAskedToKillMongoDbShouldNotExistAProcessRunningContainingTheStringMongoInItsName()
        {
            FluentAssert.Test
                        .Given(_subjectUnderTest)
                        .When(ItIsAskedToKillMongoDb)
                        .With(TheMongoDbDeployed)
                        .With(TheMongoDbRunning)
                        .Should(NotExistAProcessRunningContainingTheStringMongoInItsName)
                        .Verify();
        }

        private void ItIsAskedToKillMongoDb()
        {
            _subjectUnderTest.Kill();
            Thread.Sleep(1000);
        }

        private void TheMongoDbRunning()
        {
            _subjectUnderTest.Run();
            Thread.Sleep(1000);
        }

        private void NotExistAProcessRunningContainingTheStringMongoInItsName()
        {
            Process.GetProcessesByName("mongod").Any()
                .ShouldBeFalse();            
        }

        [Test]
        public void GivenAnIMongoDeployerWithTheMongoDbDeployedAndRunningWhenItIsAskedIfMongoDbIsRunningShouldReturnTrue()
        {
            FluentAssert.Test
                        .Given(_subjectUnderTest)
                        .When(ItIsAskedIfMongoDbIsRunning)
                        .With(TheMongoDbDeployed)
                        .With(TheMongoDbRunning)
                        .Should(ReturnTrue)
                        .Verify();
        }

        private void ItIsAskedIfMongoDbIsRunning()
        {
            _returnedIsRunning = _subjectUnderTest.IsRunning();
        }

        private void ReturnTrue()
        {
            _returnedIsRunning
                .ShouldBeTrue();
            ExistAProcessRunningContainingTheStringMongoInItsName();
        }

        [Test]
        public void GivenAnIMongoDeployerWithTheMongoDbDeployedWhenItIsAskedIfMongoDbIsRunningShouldReturnFalse()
        {
            FluentAssert.Test
                        .Given(_subjectUnderTest)
                        .When(ItIsAskedIfMongoDbIsRunning)
                        .With(TheMongoDbDeployed)
                        .Should(ReturnFalse)
                        .Verify();
        }

        private void ReturnFalse()
        {
            _returnedIsRunning
                .ShouldBeFalse();
            NotExistAProcessRunningContainingTheStringMongoInItsName();
        }

        [TearDown]
        public void AfterEachTest()
        {
            if (_subjectUnderTest.IsDeployed())
            {
                //_subjectUnderTest.Remove();
                _subjectUnderTest.Dispose();
            }
            WindsorContainerInstanceHelper.Instance.Dispose();
        }
    }
}