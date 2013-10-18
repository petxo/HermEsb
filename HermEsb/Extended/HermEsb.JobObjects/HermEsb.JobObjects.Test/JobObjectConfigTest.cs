using System;
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
    public class JobObjectConfigTest
    {
        private IJobObjectsConfig _fullConfigUnderTest;
        private IJobObjectConfig _subjectUnderTest;
        
        private string _returnedName;
        private string _expectedName;
        private int _expectedActiveProcessLimit;
        private IntPtr _expectedAffinity;
        private uint _expectedLimitFlags;
        private UIntPtr _expectedMaximumWorkingSetSize;
        private UIntPtr _expectedMinimumWorkingSetSize;
        private TimeSpan _expectedPerJobUserTimeLimit;
        private TimeSpan _expectedPerProcessUserTimeLimit;
        private ProcessPriorityClass _expectedPriorityClass;
        private int _expectedSchedulingClass;
        private BasicLimitConfig _returnedBasicLimitConfig;

        private int _expectedBInheritHandle;
        private IntPtr _expectedLpSecurityDescriptor;
        private uint _expectedNLength;
        private SecurityAttributesConfig _returnedSecurityAttributesConfig;
        
        private long _expectedJobMemoryLimit;
        private UIntPtr _expectedPeakJobMemoryUsed;
        private UIntPtr _expectedPeakProcessMemoryUsed;
        private long _expectedProcessMemoryLimit;        
        private ExtendedLimitConfig _returnedExtendedLimitInformation;
		        
        [SetUp]
        public void BeforeEachTest()
        {
			var container = new WindsorContainer ();
			container.Install (new JobObjectsInstaller ());
			ContextManager.Create (new WindsorContainerHelper (container));
			ContextManager.Instance.CreateNewContext ();
            _fullConfigUnderTest = ConfigurationManager.GetSection("jobObjects") as JobObjectsConfig;
            if (_fullConfigUnderTest != null) _subjectUnderTest = _fullConfigUnderTest.Jobs[0];
        }

        [Test]
        public void GivenAnInstanceOfIJobObjectConfigWithAJobNameWhenTheNamePropertyIsAccessedShouldBeReturnedANonEmptyStringWithTheNameOfTheJob()
        {
            FluentAssert.Test
                .Given(_subjectUnderTest)
                .When(TheNamePropertyIsAccessed)
                .With(AJobName)
                .Should(ReturnANonEmptyStringWithTheNameOfTheJob)
                .Verify();
        }

        private void AJobName()
        {
            _expectedName = "job1";
        }

        private void TheNamePropertyIsAccessed()
        {
            _returnedName = _subjectUnderTest.Name;
        }

        private void ReturnANonEmptyStringWithTheNameOfTheJob()
        {
            _returnedName
                .ShouldNotBeNullOrEmpty()
                .ShouldBeEqualTo(_expectedName);
        }

        [Test]
        public void GivenAnInstanceOfIJobObjectConfigWithBasicLimitConfigWhenTheBasicLimitConfigPropertyIsAccessedShouldBeReturnedANotNullBasicLimitConfigOfTheJobAndReturnedBasicLimitConfigShouldMatchExpected()
        {
            FluentAssert.Test
                .Given(_subjectUnderTest)
                .When(TheBasicLimitConfigPropertyIsAccessed)
                .With(BasicLimitConfig)
                .Should(ReturnANotNullBasicLimitConfigOfTheJob)
                .Should(ReturnedBasicLimitConfigMatchesExpected)
                .Verify();
        }

        private void BasicLimitConfig()
        {
            _expectedActiveProcessLimit = 65535;
            _expectedAffinity = IntPtr.Zero;
            _expectedLimitFlags = 512;
            _expectedMaximumWorkingSetSize = new UIntPtr(512);
            _expectedMinimumWorkingSetSize = new UIntPtr(128);
            _expectedPerJobUserTimeLimit = TimeSpan.Parse("100");
            _expectedPerProcessUserTimeLimit = TimeSpan.Parse("200");
            _expectedPriorityClass = ProcessPriorityClass.Normal;
            _expectedSchedulingClass = 0;
        }

        private void TheBasicLimitConfigPropertyIsAccessed()
        {
            _returnedBasicLimitConfig = _subjectUnderTest.BasicLimitInformation;
        }

        private void ReturnANotNullBasicLimitConfigOfTheJob()
        {
            _returnedBasicLimitConfig
                .ShouldNotBeNull();
        }

        private void ReturnedBasicLimitConfigMatchesExpected()
        {
            _returnedBasicLimitConfig
                .ActiveProcessesLimit
                    .ShouldBeEqualTo(_expectedActiveProcessLimit);
            _returnedBasicLimitConfig
                .Affinity
                    .ShouldBeEqualTo(_expectedAffinity);
            _returnedBasicLimitConfig
                .LimitFlags
                    .ShouldBeEqualTo(_expectedLimitFlags);
            _returnedBasicLimitConfig
                .MaximumWorkingSetSize
                    .ShouldBeEqualTo(_expectedMaximumWorkingSetSize);
            _returnedBasicLimitConfig
                .MinimumWorkingSetSize
                    .ShouldBeEqualTo(_expectedMinimumWorkingSetSize);
            _returnedBasicLimitConfig
                .PerJobUserTimeLimit
                    .ShouldBeEqualTo(_expectedPerJobUserTimeLimit);
            _returnedBasicLimitConfig
                .PerProcessUserTimeLimit
                    .ShouldBeEqualTo(_expectedPerProcessUserTimeLimit);
            _returnedBasicLimitConfig
                .PriorityClass
                    .ShouldBeEqualTo(_expectedPriorityClass);
            _returnedBasicLimitConfig
                .SchedulingClass
                    .ShouldBeEqualTo(_expectedSchedulingClass);
        }
        
        [Test]
        public void GivenAnInstanceOfIJobObjectConfigWithExtendedLimitConfigWhenTheExtendedLimitConfigPropertyIsAccessedShouldBeReturnedANotNullExtendedLimitConfigOfTheJobAndReturnedExtendedLimitConfigShouldMatchExpected()
        {
            FluentAssert.Test
                .Given(_subjectUnderTest)
                .When(TheExtendedLimitConfigPropertyIsAccessed)
                .With(ExtendedLimitConfig)
                .Should(ReturnANotNullExtendedLimitConfigOfTheJob)
                .Should(ReturnedExtendedLimitConfigMatchesExpected)
                .Verify();
        }

        private void ExtendedLimitConfig()
        {
            _expectedJobMemoryLimit = 512;
            _expectedPeakJobMemoryUsed = new UIntPtr(1024);
            _expectedPeakProcessMemoryUsed = new UIntPtr(648);
            _expectedProcessMemoryLimit = 648;
        }

        private void TheExtendedLimitConfigPropertyIsAccessed()
        {
            _returnedExtendedLimitInformation = _subjectUnderTest.ExtendedLimitInformation;
        }

        private void ReturnANotNullExtendedLimitConfigOfTheJob()
        {
            _returnedExtendedLimitInformation
                .ShouldNotBeNull();
        }

        private void ReturnedExtendedLimitConfigMatchesExpected()
        {
            _returnedExtendedLimitInformation
                .BasicLimitInfo
                    .ShouldNotBeNull();
            _returnedExtendedLimitInformation
                .JobMemoryLimit
                    .ShouldBeEqualTo(_expectedJobMemoryLimit);
            _returnedExtendedLimitInformation
                .PeakJobMemoryUsed
                    .ShouldBeEqualTo(_expectedPeakJobMemoryUsed);
            _returnedExtendedLimitInformation
                .PeakProcessMemoryUsed
                    .ShouldBeEqualTo(_expectedPeakProcessMemoryUsed);
            _returnedExtendedLimitInformation
                .ProcessMemoryLimit
                    .ShouldBeEqualTo(_expectedProcessMemoryLimit);
        }

        [Ignore]
        public void GivenAnInstanceOfIJobObjectConfigWithSecurityAttributesConfigWhenTheSecurityAttributesConfigPropertyIsAccessedShouldBeReturnedANotNullSecurityAttributesConfigOfTheJobAndReturnedSecurityAttributesConfigShouldMatchExpected()
        {
            FluentAssert.Test
                .Given(_subjectUnderTest)
                .When(TheSecurityAttribytesConfigPropertyIsAccessed)
                .With(SecurityAttribytesConfig)
                .Should(ReturnANotNullSecurityAttribytesConfigOfTheJob)
                .Should(ReturnedSecurityAttribytesConfigMatchesExpected)
                .Verify();
        }

        private void SecurityAttribytesConfig()
        {
            _expectedBInheritHandle = 0;
            _expectedLpSecurityDescriptor = IntPtr.Zero;
            _expectedNLength = 0;
        }

        private void TheSecurityAttribytesConfigPropertyIsAccessed()
        {
            _returnedSecurityAttributesConfig = _subjectUnderTest.SecurityAttributes;
        }

        private void ReturnANotNullSecurityAttribytesConfigOfTheJob()
        {
            _returnedSecurityAttributesConfig
                .ShouldNotBeNull();
        }

        private void ReturnedSecurityAttribytesConfigMatchesExpected()
        {
            _returnedSecurityAttributesConfig
                .BInheritHandle
                    .ShouldBeEqualTo(_expectedBInheritHandle);
            _returnedSecurityAttributesConfig
                .LpSecurityDescriptor
                    .ShouldBeEqualTo(_expectedLpSecurityDescriptor);
            _returnedSecurityAttributesConfig
                .NLength
                    .ShouldBeEqualTo(_expectedNLength);
        }

        [TearDown]
        public void AfterEachTest()
        {
			ContextManager.Instance.CurrentContext.Dispose();
        }         
    }
}