using System.Configuration;

namespace HermEsb.Configuration.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public class EndPointParameterConfig : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string) this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the name space.
        /// </summary>
        /// <value>The name space.</value>
        [ConfigurationProperty("value", DefaultValue = default(string))]
        public string Value
        {
            get { return (string) this["value"]; }
            set { this["value"] = value; }
        }
    }
}