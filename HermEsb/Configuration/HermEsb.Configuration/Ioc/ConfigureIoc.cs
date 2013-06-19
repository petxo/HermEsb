using Castle.Windsor;
using HermEsb.Core.Ioc;
using HermEsb.Core.Ioc.WindsorContainer;

namespace HermEsb.Configuration.Ioc
{
    public static class ConfigureIoc
    {

        /// <summary>
        /// Defaults the builder.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns></returns>
        public static ConfigurationHelper DefaultBuilder(this ConfigurationHelper config)
        {
            var windsorContainer = new WindsorContainer();
            return config.WindsorContainerBuilder(windsorContainer);
        }

        /// <summary>
        /// Windsors the container builder.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="windsorContainer">The windsor container.</param>
        /// <returns></returns>
        public static ConfigurationHelper WindsorContainerBuilder(this ConfigurationHelper config, IWindsorContainer windsorContainer)
        {
            var windsorContainerHelper = new WindsorContainerHelper(windsorContainer);
            ContextManager.Create(windsorContainerHelper);

            return config;
        }

        /// <summary>
        /// Windsors the container builder.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="fileConfigPath">The file config path.</param>
        /// <returns></returns>
        public static ConfigurationHelper WindsorContainerBuilder(this ConfigurationHelper config, string fileConfigPath)
        {
            var windsorContainer = new WindsorContainer(fileConfigPath);
            var windsorContainerHelper = new WindsorContainerHelper(windsorContainer);
            ContextManager.Create(windsorContainerHelper);

            return config;
        }
        
    }
}