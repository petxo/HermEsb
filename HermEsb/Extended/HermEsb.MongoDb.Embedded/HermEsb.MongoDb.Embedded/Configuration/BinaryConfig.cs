using System.Configuration;

namespace HermEsb.Extended.MongoDb.Embedded.Configuration
{
    public class BinaryConfig : ConfigurationElement, IBinaryConfig
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [ConfigurationProperty("name", DefaultValue = "binary_name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return this["name"] as string; }
            set { this["name"] = value; }
        }
    }
}