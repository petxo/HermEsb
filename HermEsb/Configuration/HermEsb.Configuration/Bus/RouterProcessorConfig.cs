using System.Configuration;
using HermEsb.Configuration.EndPoints;

namespace HermEsb.Configuration.Bus
{
    /// <summary>
    /// 
    /// </summary>
    [ConfigurationFragmentName("routerProcessor")]
    public class RouterProcessorConfig : ConfigurationElement
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
        /// Gets the control input.
        /// </summary>
        /// <value>The control input.</value>
        [ConfigurationProperty("input", IsRequired = true)]
        public EndPointConfig Input
        {
            get { return (EndPointConfig)this["input"] ?? new EndPointConfig(); }
        }
    }
}