using System.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using HermEsb.Extended.JobObjects.Configuration;

namespace HermEsb.Extended.JobObjects.Installer
{
    public class JobObjectsInstaller : IWindsorInstaller
    {
        #region Implementation of IWindsorInstaller

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                                   .For<IBasicLimitConfig>()
                                   .ImplementedBy<BasicLimitConfig>()
                                   .LifeStyle.Transient
                );
            container.Register(Component
                                   .For<IExtendedLimitConfig>()
                                   .ImplementedBy<ExtendedLimitConfig>()
                                   .LifeStyle.Transient
                );
            container.Register(Component
                                   .For<ISecurityAttributesConfig>()
                                   .ImplementedBy<SecurityAttributesConfig>()
                                   .LifeStyle.Transient
                );
            container.Register(Component
                                   .For<IJobObjectConfig>()
                                   .ImplementedBy<JobObjectConfig>()
                                   .LifeStyle.Transient
                );
            container.Register(Component
                                   .For<JobsConfig>()
                                   .ImplementedBy<JobsConfig>()
                                   .LifeStyle.Singleton
                );
            container.Register(Component
                                   .For<IJobObjectsConfig>()
                                   .ImplementedBy<JobObjectsConfig>()
                                   .LifeStyle.Singleton
                                   .UsingFactoryMethod(() => ConfigurationManager.GetSection("jobObjects") as IJobObjectsConfig)
                );
            container.Register(Component
                                   .For<IJobObject>()
                                   .ImplementedBy<JobObject>()
                                   .LifeStyle.Transient
                );
        }

        #endregion
    }
}