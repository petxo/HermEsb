using System.Configuration;

namespace HermEsb.Extended.JobObjects.Configuration
{
    public class JobObjectsConfig : ConfigurationSection, IJobObjectsConfig
    {
        /// <summary>
        /// Gets the jobs.
        /// </summary>
        [ConfigurationProperty("jobs", IsRequired = true, IsKey = false, IsDefaultCollection = false)]
        public JobsConfig Jobs
        {
            get { return (JobsConfig)base["jobs"]; }
        }
    }
}