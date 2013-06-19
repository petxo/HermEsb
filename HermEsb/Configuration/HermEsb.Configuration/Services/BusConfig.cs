using System.Configuration;
using HermEsb.Configuration.EndPoints;

namespace HermEsb.Configuration.Services
{
    [ConfigurationFragmentName("bus")]
    public class BusConfig : ConfigurationElement
    {
        /// <summary>
        /// Gets the input control.
        /// </summary>
        /// <value>The input control.</value>
        [ConfigurationProperty("controlInput", IsRequired = true)]
        public EndPointConfig ControlInput
        {
            get { return (EndPointConfig)this["controlInput"] ?? new EndPointConfig(); }
        }
    }
}