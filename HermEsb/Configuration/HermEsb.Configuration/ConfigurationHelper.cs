using HermEsb.Configuration.Ioc;
using HermEsb.Core.Serialization;
using HermEsb.Logging;

namespace HermEsb.Configuration
{
    /// <summary>
    /// </summary>
    public class ConfigurationHelper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigurationHelper" /> class.
        /// </summary>
        /// <param name="configurationRepository">The configuration repository.</param>
        private ConfigurationHelper(IConfigurationRepository configurationRepository)
        {
            ConfigurationRepository = configurationRepository;
        }

        /// <summary>
        ///     Gets or sets the configurator.
        /// </summary>
        /// <value>The configurator.</value>
        internal IConfigurator Configurator { get; set; }

        /// <summary>
        ///     Gets or sets the configuration repository.
        /// </summary>
        /// <value>The configuration repository.</value>
        internal IConfigurationRepository ConfigurationRepository { get; private set; }

        /// <summary>
        ///     Withes this instance.
        /// </summary>
        /// <returns></returns>
        public static ConfigurationHelper With()
        {
            InstallSerializers.Install();
            var instance = new ConfigurationHelper(new DefaultConfigurationRepository());
            instance.DefaultBuilder();

            return instance;
        }

        /// <summary>
        ///     Withes the specified file config path.
        /// </summary>
        /// <param name="fileConfigPath">The file config path.</param>
        /// <returns></returns>
        public static ConfigurationHelper With(string fileConfigPath)
        {
            InstallSerializers.Install();
            LoggerManager.Instance.Debug(string.Format("Configuracion con el fichero: {0}", fileConfigPath));
            var instance = new ConfigurationHelper(new FileConfigurationRepository(fileConfigPath));
            instance.DefaultBuilder();

            return instance;
        }
    }
}