using System.Configuration;
using System.Diagnostics;
using FluentAssert;
using HermEsb.Extended.JobObjects.Configuration;
using HermEsb.Extended.JobObjects.Installer;
using Castle;
using Castle.Core;
using Castle.Windsor;
using HermEsb.Core.Ioc;
using HermEsb.Core.Ioc.WindsorContainer;
using NUnit.Framework;

namespace HermEsb.Extended.JobObjects.Test
{
    [TestFixture]
    public class JobObjectTest
    {
        private JobObjectConfig _jobObjectConfig;
        private JobObject _subjectUnderTest;
        private Process _process;
        private bool _returnedValue;
        private long _returnedJobMemoryLimit;
        private long _returnedProcessMemoryLimit;
        private long _configuredJobMemoryLimit;
        private long _configuredProcessMemoryLimit;

        [SetUp]
        public void BeforeEachTest()
        {
			var container = new WindsorContainer ();
			container.Install (new JobObjectsInstaller ());
			ContextManager.Create (new WindsorContainerHelper (container));
			ContextManager.Instance.CreateNewContext ();
			var jobObjectsConfig = ConfigurationManager.GetSection("jobObjects") as JobObjectsConfig;
            if (jobObjectsConfig != null) _jobObjectConfig = jobObjectsConfig.Jobs[2];
            _subjectUnderTest = new JobObject(_jobObjectConfig);
        }

#if __MonoCS__
        [Ignore]
#else
		[Test]
#endif
        public void GivenAnInstanceOfIJobObjectWithAnIJobObjectConfigAndWithAProcessInitializedWhenTheProcessIsAddedCorrectlyToTheIJobObjectShouldRemainBehindTheLimitsDefinedInTheIJobObjectConfig()
        {
            FluentAssert.Test
                        .Given(_subjectUnderTest)
                        .When(TheProcessIsAddedCorrectlyToTheIJobObject)
                        .With(AProcessInitialized)
                        .With(AnJobObjectConfiguration)
                        .Should(RemainBehindTheLimitsDefinedByConfiguration)
                        .Verify();
        }

        private void TheProcessIsAddedCorrectlyToTheIJobObject()
        {
            _subjectUnderTest.AddProcess(_process);
            _returnedJobMemoryLimit = _subjectUnderTest.JobMemoryLimit;
            _returnedProcessMemoryLimit = _subjectUnderTest.ProcessMemoryLimit;
        }

        private void AProcessInitialized()
        {           
            _process = new Process
                {
                    StartInfo =
                        {
                            FileName = @"C:\MongoDB\bin\mongod.exe",
                            Arguments = @"-rest --dbpath C:\MongoDB\data --logpath C:\MongoDB\log",
                            WorkingDirectory = @"C:\MongoDB\bin\",
                            UseShellExecute = false,
                            CreateNoWindow = true                            
                        }                        
                };                       
            _returnedValue = _process.Start();
        }

        private void AnJobObjectConfiguration()
        {
            _configuredJobMemoryLimit = _jobObjectConfig.ExtendedLimitInformation.JobMemoryLimit;
            _configuredProcessMemoryLimit = _jobObjectConfig.ExtendedLimitInformation.ProcessMemoryLimit;
        }

        private void RemainBehindTheLimitsDefinedByConfiguration()
        {
            _returnedValue
                .ShouldBeTrue();
            _returnedJobMemoryLimit
                .ShouldBeLessThanOrEqualTo(_configuredJobMemoryLimit);
            _returnedProcessMemoryLimit
                .ShouldBeLessThanOrEqualTo(_configuredProcessMemoryLimit);
        }

        [TearDown]
        public void AfterEachTest()        
        {                                    
            _process.Kill();
            _process.Dispose();
            _subjectUnderTest.Dispose();
			ContextManager.Instance.CurrentContext.Dispose ();
        }
    }
}