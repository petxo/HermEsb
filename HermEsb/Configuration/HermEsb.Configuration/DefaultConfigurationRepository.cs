using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace HermEsb.Configuration
{
    public class DefaultConfigurationRepository : IConfigurationRepository
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : class, new()
        {
            if (!typeof(ConfigurationSection).IsAssignableFrom(typeof(T)))
                throw new ArgumentException("DefaultConfiguration is based on .NET Framework's ConfigurationSections");

            var configFragmentNameAttributes = new List<IConfigurationFragmentNameAttribute>(typeof (T).GetCustomAttributes(typeof(ConfigurationFragmentNameAttribute), false).Cast<IConfigurationFragmentNameAttribute>());
            var configFragmentName = configFragmentNameAttributes.Count > 0 ? configFragmentNameAttributes.FirstOrDefault().ConfigurationFragmentName : typeof (T).Name;
            return ConfigurationManager.GetSection(configFragmentName) as T;
        }
    }
}