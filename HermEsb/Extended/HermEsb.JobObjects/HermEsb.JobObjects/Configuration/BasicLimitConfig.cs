using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using HermEsb.Extended.JobObjects.Configuration.Converters;

namespace HermEsb.Extended.JobObjects.Configuration
{
    public class BasicLimitConfig : ConfigurationElement, IBasicLimitConfig
    {
        #region Implementation of IBasicLimitConfig

        /// <summary>
        /// Gets or sets the active process limit.
        /// </summary>
        /// <value>
        /// The active process limit.
        /// </value>
        [ConfigurationProperty("activeProcessLimit", DefaultValue = 0)]
        public int ActiveProcessesLimit { get { return (int) this["activeProcessLimit"]; } }

        /// <summary>
        /// Gets or sets the affinity.
        /// </summary>
        /// <value>
        /// The affinity.
        /// </value>
        [ConfigurationProperty("affinity", DefaultValue = null)]
        [TypeConverter(typeof(IntPtrConverter))]
        public IntPtr Affinity { get { return (IntPtr) this["affinity"]; } }

        /// <summary>
        /// Gets or sets the limit flags.
        /// </summary>
        /// <value>
        /// The limit flags.
        /// </value>
        [ConfigurationProperty("limitFlags", DefaultValue = (uint)0)]
        public uint LimitFlags { get { return (uint)this["limitFlags"]; } }

        /// <summary>
        /// Gets or sets the maximum size of the working set.
        /// </summary>
        /// <value>
        /// The maximum size of the working set.
        /// </value>
        [ConfigurationProperty("maximumWorkingSetSize", DefaultValue = null)]
        [TypeConverter(typeof(UIntPtrConverter))]
        public UIntPtr MaximumWorkingSetSize { get { return (UIntPtr)this["maximumWorkingSetSize"]; } }

        /// <summary>
        /// Gets or sets the minimum size of the working set.
        /// </summary>
        /// <value>
        /// The minimum size of the working set.
        /// </value>
        [ConfigurationProperty("minimumWorkingSetSize", DefaultValue = null)]
        [TypeConverter(typeof(UIntPtrConverter))]
        public UIntPtr MinimumWorkingSetSize { get { return (UIntPtr) this["minimumWorkingSetSize"]; } }

        /// <summary>
        /// Gets or sets the per job user time limit.
        /// </summary>
        /// <value>
        /// The per job user time limit.
        /// </value>
        [ConfigurationProperty("perJobUserTimeLimit")]
        public TimeSpan PerJobUserTimeLimit { get { return (TimeSpan) this["perJobUserTimeLimit"]; } }

        /// <summary>
        /// Gets or sets the per process user time limit.
        /// </summary>
        /// <value>
        /// The per process user time limit.
        /// </value>
        [ConfigurationProperty("perProcessUserTimeLimit")]
        public TimeSpan PerProcessUserTimeLimit { get { return (TimeSpan) this["perProcessUserTimeLimit"]; } }

        /// <summary>
        /// Gets or sets the priority class.
        /// </summary>
        /// <value>
        /// The priority class.
        /// </value>
        [ConfigurationProperty("priorityClass", DefaultValue = ProcessPriorityClass.Normal)]
        public ProcessPriorityClass PriorityClass { get { return (ProcessPriorityClass) this["priorityClass"]; } }

        /// <summary>
        /// Gets or sets the scheduling class.
        /// </summary>
        /// <value>
        /// The scheduling class.
        /// </value>
        [ConfigurationProperty("schedulingClass", DefaultValue = 0)]
        public int SchedulingClass { get { return (int) this["schedulingClass"]; } }

        #endregion
    }
}