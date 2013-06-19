using HermEsb.Logging;
using HermEsb.Logging.L4N;

namespace HermEsb.Configuration.Logging
{
    /// <summary>
    /// </summary>
    public static class ConfiguratorLogger
    {
        /// <summary>
        ///     Log4s the net builder.
        /// </summary>
        /// <param name="configurationHelper">The configuration helper.</param>
        /// <returns></returns>
        public static ConfigurationHelper Log4NetBuilder(this ConfigurationHelper configurationHelper)
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
        public static ConfigurationHelper Log4NetBuilder(this ConfigurationHelper configurationHelper, string pathConfig)
        {
            LoggerManager.Create(Log4NetFactory.CreateFrom(pathConfig));
            return configurationHelper;
        }
    }
}