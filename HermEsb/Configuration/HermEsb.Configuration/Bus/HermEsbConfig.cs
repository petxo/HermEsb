using System.Configuration;
using HermEsb.Configuration.Services;

namespace HermEsb.Configuration.Bus
{
    [ConfigurationFragmentName("HermEsb")]
    public class HermEsbConfig : ConfigurationSection
    {
        /// <summary>
        /// Gets the processor.
        /// </summary>
        /// <value>The processor.</value>
        [ConfigurationProperty("routerProcessor", IsRequired = true)]
        public RouterProcessorConfig RouterProcessor
        {
            get { return (RouterProcessorConfig) this["routerProcessor"] ?? new RouterProcessorConfig(); }
        }

        /// <summary>
        /// Gets the control.
        /// </summary>
        /// <value>The control.</value>
        [ConfigurationProperty("routerControlProcessor", IsRequired = true)]
        public RouterControlProcessorConfig RouterControlProcessor
        {
            get
            {
                return (RouterControlProcessorConfig) this["routerControlProcessor"] ??
                       new RouterControlProcessorConfig();
            }
        }

        /// <summary>
        /// Gets the identification.
        /// </summary>
        /// <value>The identification.</value>
        [ConfigurationProperty("identification", IsRequired = true)]
        public IdentificationConfig Identification
        {
            get { return (IdentificationConfig)this["identification"]; }
        }

        /// <summary>
        /// Gets the error handling controller.
        /// </summary>
        /// <value>The error handling controller.</value>
        [ConfigurationProperty("errorHandling", IsRequired = false)]
        public ErrorHandlingControllerConfig ErrorHandlingController
        {
            get { return (ErrorHandlingControllerConfig)this["errorHandling"]; }
        }
    }
}
