using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using HermEsb.Logging;

namespace HermEsb.Configuration
{
    /// <summary>
    /// </summary>
    public class FileConfigurationRepository : IConfigurationRepository
    {
        private readonly System.Configuration.Configuration _configuration;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileConfigurationRepository" /> class.
        /// </summary>
        /// <param name="fileConfigPath">The file config path.</param>
        public FileConfigurationRepository(string fileConfigPath)
        {
            LoggerManager.Instance.Debug(string.Format("Obteniendo el fichero de configuracion: {0}", fileConfigPath));
            var configFile = new ExeConfigurationFileMap {ExeConfigFilename = fileConfigPath};
            _configuration = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
        }

        /// <summary>
        ///     Gets the configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : class, new()
        {
            if (!typeof (ConfigurationSection).IsAssignableFrom(typeof (T)))
                throw new ArgumentException("DefaultConfiguration is based on .NET Framework's ConfigurationSections");

            var configFragmentNameAttributes =
                new List<IConfigurationFragmentNameAttribute>(
                    typeof (T).GetCustomAttributes(typeof (ConfigurationFragmentNameAttribute), false)
                              .Cast<IConfigurationFragmentNameAttribute>());
            string configFragmentName = configFragmentNameAttributes.Count > 0
                                            ? configFragmentNameAttributes.FirstOrDefault().ConfigurationFragmentName
                                            : typeof (T).Name;

            return _configuration.GetSection(configFragmentName) as T;
        }
    }
}