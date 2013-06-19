namespace HermEsb.Configuration
{
    public interface IConfigurationFragmentNameAttribute
    {
        /// <summary>
        /// Gets or sets the name of the configuration section.
        /// </summary>
        /// <value>The name of the configuration section.</value>
        string ConfigurationFragmentName { get; }
    }
}