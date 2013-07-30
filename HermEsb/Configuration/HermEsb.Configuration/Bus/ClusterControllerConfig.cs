using System.Configuration;
using HermEsb.Configuration.EndPoints;

namespace HermEsb.Configuration.Bus
{
    [ConfigurationFragmentName("clusterController")]
    public class ClusterControllerConfig : ConfigurationElement
    {
        /// <summary>
        /// Gets the output.
        /// </summary>
        /// <value>The output.</value>
        [ConfigurationProperty("clusterControlOutput", IsRequired = true)]
        public EndPointConfig ClusterControlOutput
        {
            get { return (EndPointConfig)this["clusterControlOutput"] ?? new EndPointConfig(); }
        }

        [ConfigurationProperty("clusterInput", IsRequired = true)]
        public EndPointConfig ClusterInput
        {
            get { return (EndPointConfig)this["clusterInput"] ?? new EndPointConfig(); }
        }


    }
}