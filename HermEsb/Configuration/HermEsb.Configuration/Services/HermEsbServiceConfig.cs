using System.Configuration;

namespace HermEsb.Configuration.Services
{
    [ConfigurationFragmentName("HermEsbService")]
    public class HermEsbServiceConfig : ConfigurationSection
    {
        /// <summary>
        /// Gets the name of the section.
        /// </summary>
        /// <value>The name of the section.</value>
        public static string SectionName
        {
            get { return "HermEsbService"; }
        }
        /// <summary>
        /// Gets the bus.
        /// </summary>
        /// <value>The bus.</value>
        [ConfigurationProperty("bus", IsRequired = true)]
        public BusConfig Bus
        {
            get { return (BusConfig) this["bus"] ?? new BusConfig(); }
        }

        /// <summary>
        /// Gets the processor.
        /// </summary>
        /// <value>The processor.</value>
        [ConfigurationProperty("serviceProcessor", IsRequired = true)]
        public ServiceProcessorConfig ServiceProcessor
        {
            get { return (ServiceProcessorConfig) this["serviceProcessor"] ?? new ServiceProcessorConfig(); }
        }

        /// <summary>
        /// Gets the control.
        /// </summary>
        /// <value>The control.</value>
        [ConfigurationProperty("controlProcessor", IsRequired = true)]
        public ControlProcessorConfig ControlProcessor
        {
            get { return (ControlProcessorConfig) this["controlProcessor"] ?? new ControlProcessorConfig(); }
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