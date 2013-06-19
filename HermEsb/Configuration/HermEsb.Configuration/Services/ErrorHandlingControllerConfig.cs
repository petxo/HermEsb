using System.Configuration;
using HermEsb.Configuration.EndPoints;

namespace HermEsb.Configuration.Services
{
    [ConfigurationFragmentName("errorHandling")]
    public class ErrorHandlingControllerConfig : ConfigurationElement
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
    }
}