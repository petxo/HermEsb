using System.Configuration;
using HermEsb.Configuration.EndPoints;
using HermEsb.Configuration.Services;

namespace HermEsb.Configuration.Publishers
{
    /// <summary>
    /// 
    /// </summary>
    [ConfigurationFragmentName("publisher")]
    public class PublisherConfig : ConfigurationSection
    {
        /// <summary>
        /// Gets the output.
        /// </summary>
        /// <value>The output.</value>
        [ConfigurationProperty("output", IsRequired = true)]
        public EndPointConfig Output
        {
            get { return (EndPointConfig)this["output"] ?? new EndPointConfig(); }
        }

        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        /// <value>The identification.</value>
        [ConfigurationProperty("identification", IsRequired = true)]
        public IdentificationConfig Identification
        {
            get { return (IdentificationConfig) this["identification"] ?? new IdentificationConfig(); }
        }
    }
}