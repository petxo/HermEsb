using System;
using System.Diagnostics;

namespace HermEsb.Extended.JobObjects.Configuration
{
    public interface IBasicLimitConfig
    {
        /// <summary>
        /// Gets or sets the active process limit.
        /// </summary>
        /// <value>
        /// The active process limit.
        /// </value>
        int ActiveProcessesLimit { get; }
        
        /// <summary>
        /// Gets or sets the affinity.
        /// </summary>
        /// <value>
        /// The affinity.
        /// </value>
        IntPtr Affinity { get; }
        
        /// <summary>
        /// Gets or sets the limit flags.
        /// </summary>
        /// <value>
        /// The limit flags.
        /// </value>
        UInt32 LimitFlags { get; }
       
        /// <summary>
        /// Gets or sets the maximum size of the working set.
        /// </summary>
        /// <value>
        /// The maximum size of the working set.
        /// </value>
        UIntPtr MaximumWorkingSetSize { get; }
        
        /// <summary>
        /// Gets or sets the minimum size of the working set.
        /// </summary>
        /// <value>
        /// The minimum size of the working set.
        /// </value>
        UIntPtr MinimumWorkingSetSize { get; }
        
        /// <summary>
        /// Gets or sets the per job user time limit.
        /// </summary>
        /// <value>
        /// The per job user time limit.
        /// </value>
        TimeSpan PerJobUserTimeLimit { get; }
        
        /// <summary>
        /// Gets or sets the per process user time limit.
        /// </summary>
        /// <value>
        /// The per process user time limit.
        /// </value>
        TimeSpan PerProcessUserTimeLimit { get; }
        
        /// <summary>
        /// Gets or sets the priority class.
        /// </summary>
        /// <value>
        /// The priority class.
        /// </value>
        ProcessPriorityClass PriorityClass { get; }
        
        /// <summary>
        /// Gets or sets the scheduling class.
        /// </summary>
        /// <value>
        /// The scheduling class.
        /// </value>
        int SchedulingClass { get; }
    }
}