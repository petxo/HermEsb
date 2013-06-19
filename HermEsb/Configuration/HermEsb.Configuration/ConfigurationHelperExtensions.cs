using HermEsb.Configuration.Bus;
using HermEsb.Configuration.Listeners;
using HermEsb.Configuration.Services;
using HermEsb.Core.Service;

namespace HermEsb.Configuration
{
    public static class ConfigurationHelperExtensions
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <returns></returns>
        public static ConfigurationHelper ConfigureService(this ConfigurationHelper configurationHelper)
        {
            configurationHelper.Configurator = new ServiceConfigurator(configurationHelper.ConfigurationRepository.Get<HermEsbServiceConfig>());
            configurationHelper.Configurator.Configure();
            return configurationHelper;
        }

        /// <summary>
        /// Configures the bus.
        /// </summary>
        /// <returns></returns>
        public static ConfigurationHelper ConfigureBus(this ConfigurationHelper configurationHelper)
        {
            configurationHelper.Configurator = new BusConfigurator(configurationHelper.ConfigurationRepository.Get<HermEsbConfig>());
            configurationHelper.Configurator.Configure();
            return configurationHelper;
        }

        public static ConfigurationHelper ConfigureListener(this ConfigurationHelper configurationHelper)
        {
            configurationHelper.Configurator = new MonitorListenerConfigurator(configurationHelper.ConfigurationRepository.Get<ListenerConfig>());
            configurationHelper.Configurator.Configure();
            return configurationHelper;
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static IService Create(this ConfigurationHelper configurationHelper)
        {
            return configurationHelper.Configurator.Create();
        }
    }
}