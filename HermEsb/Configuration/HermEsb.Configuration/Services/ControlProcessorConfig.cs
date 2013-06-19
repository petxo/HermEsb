using System.Configuration;
using HermEsb.Configuration.EndPoints;
using HermEsb.Configuration.Monitoring;

namespace HermEsb.Configuration.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ConfigurationFragmentName("controlProcessor")]
    public class ControlProcessorConfig : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the number of threads.
        /// </summary>
        /// <value>The number of threads.</value>
        [ConfigurationProperty("numberOfParallelTasks", DefaultValue = 100)]
        public int NumberOfParallelTasks
        {
            get { return (int)this["numberOfParallelTasks"]; }
        }

        /// <summary>
        /// Gets or sets the number of threads.
        /// </summary>
        /// <value>The number of threads.</value>
        [ConfigurationProperty("maxReijections", DefaultValue = 5)]
        public int MaxReijections
        {
            get { return (int)this["maxReijections"]; }
        }

        /// <summary>
        /// Gets the input.
        /// </summary>
        /// <value>The input.</value>
        [ConfigurationProperty("input", IsRequired = true)]
        public EndPointConfig Input
        {
            get { return (EndPointConfig)this["input"] ?? new EndPointConfig(); }
        }

        /// <summary>
        /// Gets the handlers assemblies.
        /// </summary>
        /// <value>The handlers assemblies.</value>
        [ConfigurationProperty("handlersAssemblies", IsRequired = true)]
        public HandlerAssembliesListConfig HandlersAssemblies
        {
            get { return (HandlerAssembliesListConfig)this["handlersAssemblies"] ?? new HandlerAssembliesListConfig(); }
        }

        /// <summary>
        /// Gets the monitor.
        /// </summary>
        /// <value>The monitor.</value>
        [ConfigurationProperty("monitor")]
        public MonitorConfig Monitor
        {
            get { return (MonitorConfig) (this["monitor"] ?? new MonitorConfig()); }
        }
    }
}