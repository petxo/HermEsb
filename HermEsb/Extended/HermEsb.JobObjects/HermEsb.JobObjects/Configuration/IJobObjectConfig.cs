using System.Configuration;

namespace HermEsb.Extended.JobObjects.Configuration
{
    public interface IJobObjectConfig
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [ConfigurationProperty("name", IsKey = true, DefaultValue = "job", IsRequired = true)]
        string Name { get; }

        /// <summary>
        /// Gets or sets the security attributes config.
        /// </summary>
        /// <value>
        /// The security attributes config.
        /// </value>
        [ConfigurationProperty("securityAttributes")]
        SecurityAttributesConfig SecurityAttributes { get; }

        /// <summary>
        /// Gets or sets the extended limit information.
        /// </summary>
        /// <value>
        /// The extended limit information.
        /// </value>
        [ConfigurationProperty("extendedLimitInformation")]
        ExtendedLimitConfig ExtendedLimitInformation { get; }
        
        /// <summary>
        /// Gets the basic limit information.
        /// </summary>
        [ConfigurationProperty("basicLimitInformation")]
        BasicLimitConfig BasicLimitInformation { get; }
    }
}