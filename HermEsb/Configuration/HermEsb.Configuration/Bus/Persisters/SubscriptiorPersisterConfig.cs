using System.Configuration;
using HermEsb.Configuration.Bus.Persisters.Mongo;

namespace HermEsb.Configuration.Bus.Persisters
{
    [ConfigurationFragmentName("subscriptorsPersister")]
    public class SubscriptiorPersisterConfig : ConfigurationElement
    {
        /// <summary>
        ///     Gets the output.
        /// </summary>
        /// <value>The output.</value>
        [ConfigurationProperty("mongoPersister")]
        public MongoPersiterConfig MongoPersister
        {
            get { return (MongoPersiterConfig) this["mongoPersister"] ?? new MongoPersiterConfig(); }
        }
    }
}