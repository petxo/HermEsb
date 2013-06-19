using System;

namespace HermEsb.Core.Monitoring.Frequences
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SamplerFrequenceLevelAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the frequence.
        /// </summary>
        /// <value>The frequence.</value>
        public FrequenceLevel Frequence { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplerFrequenceLevelAttribute"/> class.
        /// </summary>
        public SamplerFrequenceLevelAttribute()
        {
            Frequence = FrequenceLevel.Normal;
        }
    }
}