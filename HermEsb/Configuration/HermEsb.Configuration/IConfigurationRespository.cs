namespace HermEsb.Configuration
{
    public interface IConfigurationRepository
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>() where T : class, new();
    }
}