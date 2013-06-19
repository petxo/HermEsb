using HermEsb.Configuration.Publishers;
using HermEsb.Core.Publishers;
using HermEsb.Logging;
using HermEsb.Logging.L4N;

namespace HermEsb.Configuration
{
    /// <summary>
    /// </summary>
    public static class ConfigurationPublisherExtensions
    {
        /// <summary>
        ///     Configures the publisher.
        /// </summary>
        /// <param name="configurationPublisher">The configuration publisher.</param>
        /// <returns></returns>
        public static ConfigurationPublisher ConfigurePublisher(this ConfigurationPublisher configurationPublisher)
        {
            configurationPublisher.PublisherConfigurator =
                new PublisherConfigurator(configurationPublisher.ConfigurationRepository.Get<PublisherConfig>());
            return configurationPublisher;
        }

        /// <summary>
        ///     Creates the specified configuration publisher.
        /// </summary>
        /// <param name="configurationPublisher">The configuration publisher.</param>
        /// <returns></returns>
        public static IBusPublisher Create(this ConfigurationPublisher configurationPublisher)
        {
            return configurationPublisher.PublisherConfigurator.CreateBusPublisher();
        }

        /// <summary>
        ///     Log4s the net builder.
        /// </summary>
        /// <param name="configurationHelper">The configuration helper.</param>
        /// <returns></returns>
        public static ConfigurationPublisher Log4NetBuilder(this ConfigurationPublisher configurationHelper)
        {
            LoggerManager.Create(Log4NetFactory.DefaultLogger());
            return configurationHelper;
        }


        /// <summary>
        ///     Log4s the net builder.
        /// </summary>
        /// <param name="configurationHelper">The configuration helper.</param>
        /// <param name="pathConfig">The path config.</param>
        /// <returns></returns>
        public static ConfigurationPublisher Log4NetBuilder(this ConfigurationPublisher configurationHelper,
                                                            string pathConfig)
        {
            LoggerManager.Create(Log4NetFactory.CreateFrom(pathConfig));
            return configurationHelper;
        }
    }
}