using System;

namespace HermEsb.Configuration
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationFragmentNameAttribute : Attribute, IConfigurationFragmentNameAttribute
    {
        /// <summary>
        /// Gets or sets the name of the configuration section.
        /// </summary>
        /// <value>The name of the configuration section.</value>
        public string ConfigurationFragmentName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationFragmentNameAttribute"/> class.
        /// </summary>
        /// <param name="configurationSectionName">Name of the configuration section.</param>
        public ConfigurationFragmentNameAttribute(string configurationSectionName)
        {
            ConfigurationFragmentName = configurationSectionName;
        }
    }
}