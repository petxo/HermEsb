using System.Configuration;
using BteamMongoDB.Config;

namespace HermEsb.Configuration.Bus.Persisters.Mongo
{
    public class MongoPersiterConfig : ConfigurationElement
    {
        /// <summary>
        ///     Gets the output.
        /// </summary>
        /// <value>The output.</value>
        [ConfigurationProperty("connection", IsRequired = true)]
        public ConnectionExtendedConfig Connection
        {
            get { return (ConnectionExtendedConfig) this["connection"] ?? new ConnectionExtendedConfig(); }
        }


        /// <summary>
        ///     Gets the collection.
        /// </summary>
        /// <value>The collection.</value>
        [ConfigurationProperty("collection", IsRequired = true)]
        public string Collection
        {
            get { return (string) this["collection"]; }
        }
    }
}