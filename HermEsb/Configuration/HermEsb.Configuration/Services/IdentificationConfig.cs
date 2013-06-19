using System.Configuration;

namespace HermEsb.Configuration.Services
{
    public class IdentificationConfig : ConfigurationElement
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        [ConfigurationProperty("id", IsRequired = true)]
        public string Id
        {
            get { return (string) this["id"]; }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string) this["type"]; }
        }
    }
}