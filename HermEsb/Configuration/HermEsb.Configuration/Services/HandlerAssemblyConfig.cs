using System.Configuration;

namespace HermEsb.Configuration.Services
{
    public class HandlerAssemblyConfig : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string) this["name"]; }
        }

        /// <summary>
        /// Gets or sets the assembly.
        /// </summary>
        /// <value>The assembly.</value>
        [ConfigurationProperty("assembly", DefaultValue = default(string))]
        public string Assembly
        {
            get { return (string) this["assembly"]; }
        }
    }
}