using System;

namespace HermEsb.Extended.JobObjects.Configuration
{
    public interface IExtendedLimitConfig
    {
        /// <summary>
        /// Gets or sets the basic limit info.
        /// </summary>
        /// <value>
        /// The basic limit info.
        /// </value>
        BasicLimitConfig BasicLimitInfo { get; }

        /// <summary>
        /// Gets or sets the job memory limit.
        /// </summary>
        /// <value>
        /// The job memory limit.
        /// </value>
        long JobMemoryLimit { get; }

        /// <summary>
        /// Gets or sets the peak job memory used.
        /// </summary>
        /// <value>
        /// The peak job memory used.
        /// </value>
        UIntPtr PeakJobMemoryUsed { get; }

        /// <summary>
        /// Gets or sets the peak process memory used.
        /// </summary>
        /// <value>
        /// The peak process memory used.
        /// </value>
        UIntPtr PeakProcessMemoryUsed { get; }

        /// <summary>
        /// Gets or sets the process memory limit.
        /// </summary>
        /// <value>
        /// The process memory limit.
        /// </value>
        long ProcessMemoryLimit { get; }
    }
}