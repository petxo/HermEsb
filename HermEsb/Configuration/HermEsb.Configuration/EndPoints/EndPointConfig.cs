using System.Configuration;
using HermEsb.Core.Communication;

namespace HermEsb.Configuration.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public class EndPointConfig : ConfigurationElementCollection, IEndPointConfig
    {
        /// <summary>
        /// Gets or sets the transport.
        /// </summary>
        /// <value>The transport.</value>
        [ConfigurationProperty("transport", IsRequired = true)]
        public TransportType Transport
        {
            get { return (TransportType) this["transport"]; }
        }

        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <value>The URI.</value>
        public string Uri
        {
            get { return ((EndPointParameterConfig) BaseGet("uri")).Value; }
        }

        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            switch (Transport)
            {
                default:
                    return new EndPointParameterConfig();
            }
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement"/> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object"/> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            switch (Transport)
            {
                default:
                    return ((EndPointParameterConfig) element).Name;
            }
        }
    }
}