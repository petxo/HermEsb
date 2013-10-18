using System.Configuration;

namespace HermEsb.Extended.JobObjects.Configuration
{
    public interface IJobObjectsConfig
    {
        /// <summary>
        /// Gets the jobs.
        /// </summary>
        [ConfigurationProperty("jobs", IsRequired = true, IsKey = false, IsDefaultCollection = false)]
        JobsConfig Jobs { get; }
    }
}