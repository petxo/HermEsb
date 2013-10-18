using System.Configuration;

namespace HermEsb.Extended.MongoDb.Embedded.Configuration
{
    public interface IBinaryConfig
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [ConfigurationProperty("name", DefaultValue = "binary_name", IsRequired = true, IsKey = true)]
        string Name { get; set; }
    }
}