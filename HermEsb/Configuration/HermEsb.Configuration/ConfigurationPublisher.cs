using HermEsb.Configuration.Publishers;
using HermEsb.Core.Serialization;

namespace HermEsb.Configuration
{
    public class ConfigurationPublisher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationPublisher"/> class.
        /// </summary>
        /// <param name="configurationRepository">The configuration repository.</param>
        private ConfigurationPublisher(IConfigurationRepository configurationRepository)
        {
            ConfigurationRepository = configurationRepository;
        }

        /// <summary>
        /// Gets or sets the publisher configurator.
        /// </summary>
        /// <value>The publisher configurator.</value>
        internal PublisherConfigurator PublisherConfigurator { get; set; }

        /// <summary>
        /// Gets or sets the configuration repository.
        /// </summary>
        /// <value>The configuration repository.</value>
        internal IConfigurationRepository ConfigurationRepository { get; private set; }

        /// <summary>
        /// Withes this instance.
        /// </summary>
        /// <returns></returns>
        public static ConfigurationPublisher With()
        {
            InstallSerializers.Install();
            return new ConfigurationPublisher(new DefaultConfigurationRepository());
        }

        /// <summary>
        /// Withes the specified file config path.
        /// </summary>
        /// <param name="fileConfigPath">The file config path.</param>
        /// <returns></returns>
        public static ConfigurationPublisher With(string fileConfigPath)
        {
            InstallSerializers.Install();
            return new ConfigurationPublisher(new FileConfigurationRepository(fileConfigPath));
        }

    }
}