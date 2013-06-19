using System.Configuration;

namespace HermEsb.Configuration.Monitoring
{
    [ConfigurationFragmentName("frequenceLevel")]
    public class FrequenceLevelConfig : ConfigurationElement
    {
        /// <summary>
        /// Gets the highest.
        /// </summary>
        /// <value>The highest.</value>
        [ConfigurationProperty("highest")]
        public int? Highest
        {
            get { return (int?) this["highest"]; }
        }

        /// <summary>
        /// Gets the highest.
        /// </summary>
        /// <value>The highest.</value>
        [ConfigurationProperty("high")]
        public int? High
        {
            get { return (int?)this["high"]; }
        }

        /// <summary>
        /// Gets the highest.
        /// </summary>
        /// <value>The highest.</value>
        [ConfigurationProperty("normal")]
        public int? Normal
        {
            get { return (int?)this["normal"]; }
        }

        /// <summary>
        /// Gets the highest.
        /// </summary>
        /// <value>The highest.</value>
        [ConfigurationProperty("low")]
        public int? Low
        {
            get { return (int?)this["low"]; }
        }


        /// <summary>
        /// Gets the highest.
        /// </summary>
        /// <value>The highest.</value>
        [ConfigurationProperty("lowest")]
        public int? Lowest
        {
            get { return (int?)this["lowest"]; }
        }
    }
}