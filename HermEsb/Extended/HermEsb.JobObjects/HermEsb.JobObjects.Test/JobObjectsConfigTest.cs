using System.Configuration;
using FluentAssert;
using HermEsb.Extended.JobObjects.Configuration;
using HermEsb.Extended.JobObjects.Installer;
using HermEsb.Core.Ioc;
using NUnit.Framework;

namespace HermEsb.Extended.JobObjects.Test
{
    [TestFixture]
    public class JobObjectsConfigTest
    {
        private IJobObjectsConfig _subjectUnderTest;
        private JobsConfig _jobsReturned;

        [SetUp]
        public void BeforeEachTest()
        {
            _subjectUnderTest = ConfigurationManager.GetSection("jobObjects") as JobObjectsConfig;
        }

        [Test]
        public void GivenAnInstanceOfIJobObjectsConfigWhenTheJobsCollectionIsAccessedShouldBeReturnedAListOfIJobObjectConfig()
        {
            FluentAssert.Test
                .Given(_subjectUnderTest)
                .When(TheJobsCollectionIsAccessed)
                .Should(ReturnAListOfIJobObjectConfig)
                .Verify();
        }

        private void TheJobsCollectionIsAccessed()
        {
            _jobsReturned = _subjectUnderTest.Jobs;
        }

        private void ReturnAListOfIJobObjectConfig()
        {
            _jobsReturned
                .ShouldNotBeNull()
                .Count
                    .ShouldBeEqualTo(3);
        }

        [TearDown]
        public void AfterEachTest()
        {
        }         
    }
}