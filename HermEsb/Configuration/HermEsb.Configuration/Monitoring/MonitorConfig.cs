using System.Configuration;
using HermEsb.Configuration.EndPoints;
using HermEsb.Configuration.Services;

namespace HermEsb.Configuration.Monitoring
{
    [ConfigurationFragmentName("monitor")]
    public class MonitorConfig : ConfigurationElement
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
        /// Gets the samplers assemblies.
        /// </summary>
        /// <value>The samplers assemblies.</value>
        [ConfigurationProperty("samplersAssemblies", IsRequired = true)]
        public HandlerAssembliesListConfig SamplersAssemblies
        {
            get { return (HandlerAssembliesListConfig)this["samplersAssemblies"] ?? new HandlerAssembliesListConfig(); }
        }

        /// <summary>
        /// Gets the frequence level.
        /// </summary>
        /// <value>The frequence level.</value>
        [ConfigurationProperty("frequenceLevelConfig")]
        public FrequenceLevelConfig FrequenceLevel
        {
            get { return (FrequenceLevelConfig)this["frequenceLevelConfig"] ?? new FrequenceLevelConfig(); }
        }
        
    }
}