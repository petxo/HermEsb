using System;
using System.ComponentModel;
using System.Configuration;
using HermEsb.Extended.JobObjects.Configuration.Converters;

namespace HermEsb.Extended.JobObjects.Configuration
{
    public class ExtendedLimitConfig : ConfigurationElement, IExtendedLimitConfig
    {
        #region Implementation of IExtendedLimitConfig

        /// <summary>
        /// Gets or sets the basic limit info.
        /// </summary>
        /// <value>
        /// The basic limit info.
        /// </value>
        public BasicLimitConfig BasicLimitInfo { get; internal set; }

        /// <summary>
        /// Gets or sets the job memory limit.
        /// </summary>
        /// <value>
        /// The job memory limit.
        /// </value>
        [ConfigurationProperty("jobMemoryLimit", DefaultValue = null)]
        public long JobMemoryLimit { get { return (long) this["jobMemoryLimit"]; } }

        /// <summary>
        /// Gets or sets the peak job memory used.
        /// </summary>
        /// <value>
        /// The peak job memory used.
        /// </value>
        [ConfigurationProperty("peakJobMemoryUsed", DefaultValue = null)]
        [TypeConverter(typeof(UIntPtrConverter))]
        public UIntPtr PeakJobMemoryUsed { get { return (UIntPtr) this["peakJobMemoryUsed"]; } }

        /// <summary>
        /// Gets or sets the peak process memory used.
        /// </summary>
        /// <value>
        /// The peak process memory used.
        /// </value>
        [ConfigurationProperty("peakProcessMemoryUsed", DefaultValue = null)]
        [TypeConverter(typeof(UIntPtrConverter))]
        public UIntPtr PeakProcessMemoryUsed { get { return (UIntPtr) this["peakProcessMemoryUsed"]; } }

        /// <summary>
        /// Gets or sets the process memory limit.
        /// </summary>
        /// <value>
        /// The process memory limit.
        /// </value>
        [ConfigurationProperty("processMemoryLimit", DefaultValue = null)]
        public long ProcessMemoryLimit { get { return (long) this["processMemoryLimit"]; } }

        #endregion
    }
}