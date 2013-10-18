using System.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using HermEsb.Extended.MongoDb.Embedded.Configuration;

namespace HermEsb.Extended.MongoDb.Embedded.Installer
{
    public class MongoDbEmbeddedInstaller : IWindsorInstaller
    {
        #region Implementation of IWindsorInstaller

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                                   .For<IBinaryConfig>()
                                   .ImplementedBy<BinaryConfig>()
                                   .LifeStyle.Transient
                );
            container.Register(Component
                                   .For<BinariesConfig>()
                                   .ImplementedBy<BinariesConfig>()
                                   .LifeStyle.Transient
                );
            container.Register(Component
                                   .For<IMongoDbEmbeddedConfig>()
                                   .ImplementedBy<MongoDbEmbeddedConfig>()
                                   .LifeStyle.Transient
                                   .UsingFactoryMethod(() => ConfigurationManager.GetSection("mongoDbEmbedded") as IMongoDbEmbeddedConfig)
                );
            container.Register(Component
                                   .For<IEmbeddedResourceHelper>()
                                   .ImplementedBy<EmbeddedResourceHelper>()
                                   .LifeStyle.Singleton
                );
            container.Register(Component
                                   .For<IMongoDeployer>()
                                   .ImplementedBy<MongoDeployer>()
                                   .LifeStyle.PerThread
                );
            container.Register(Component
                                   .For<IMongoBootstrapper>()
                                   .ImplementedBy<MongoBootstrapper>()
                                   .LifeStyle.PerThread
                );
        }

        #endregion
    }
}