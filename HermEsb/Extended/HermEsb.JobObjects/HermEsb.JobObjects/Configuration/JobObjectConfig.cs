using System.Configuration;

namespace HermEsb.Extended.JobObjects.Configuration
{
    public class JobObjectConfig : ConfigurationElement, IJobObjectConfig
    {
        #region Implementation of IJobObjectConfig

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [ConfigurationProperty("name", IsKey = true, DefaultValue = "job", IsRequired = true)]
        public string Name { get { return base["name"] as string; } }

        /// <summary>
        /// Gets or sets the security attributes config.
        /// </summary>
        /// <value>
        /// The security attributes config.
        /// </value>
        [ConfigurationProperty("securityAttributes")]
        public SecurityAttributesConfig SecurityAttributes { get { return base["securityAttributes"] as SecurityAttributesConfig; } }

        /// <summary>
        /// Gets or sets the extended limit information.
        /// </summary>
        /// <value>
        /// The extended limit information.
        /// </value>
        [ConfigurationProperty("extendedLimitInformation")]
        public ExtendedLimitConfig ExtendedLimitInformation
        {
            get
            {
                var extendedLimitInfoCfg = base["extendedLimitInformation"] as ExtendedLimitConfig;
                if (extendedLimitInfoCfg != null)
                {
                    extendedLimitInfoCfg.BasicLimitInfo = BasicLimitInformation;
                }
                return extendedLimitInfoCfg;
            }
        }

        /// <summary>
        /// Gets the basic limit information.
        /// </summary>
        [ConfigurationProperty("basicLimitInformation")]
        public BasicLimitConfig BasicLimitInformation { get { return base["basicLimitInformation"] as BasicLimitConfig; } }

        #endregion
    }
}