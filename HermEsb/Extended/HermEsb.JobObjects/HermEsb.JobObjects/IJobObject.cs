using System;
using System.Diagnostics;

namespace HermEsb.Extended.JobObjects
{
    public interface IJobObject
    {
        /// <summary>
        /// Gets or sets a value indicating whether to kill the processes when the Job Object is closed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [kill processes on job close]; otherwise, <c>false</c>.
        /// </value>
        bool KillProcessesOnJobClose { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a process to die on an unhandled exception.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [die on unhandled exception]; otherwise, <c>false</c>.
        /// </value>
        bool DieOnUnhandledException { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether processes are allowed to create processes outside the Job Object.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [allow child processes breakaway]; otherwise, <c>false</c>.
        /// </value>
        bool AllowChildProcessesBreakaway { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether child processes are not added to the Job Object.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [always breakaway child processes]; otherwise, <c>false</c>.
        /// </value>
        bool AlwaysBreakawayChildProcesses { get; set; }

        /// <summary>
        /// Gets or sets the active processes in the Job Object. Set to 0 (zero) to disable the limit.
        /// </summary>
        /// <value>
        /// The active processes.
        /// </value>
        int ActiveProcessesLimit { get; set; }

        /// <summary>
        /// Gets or sets the memory in bytes limit enforced per process. Set to 0 (zero) to disable the limit.
        /// </summary>
        /// <value>
        /// The process memory limit.
        /// </value>
        long ProcessMemoryLimit { get; set; }

        /// <summary>
        /// Gets or sets the memory limit in bytes of the entire Job Object. Set to 0 (zero) to disable the limit.
        /// </summary>
        /// <value>
        /// The job memory limit.
        /// </value>
        long JobMemoryLimit { get; set; }

        /// <summary>
        /// Gets or sets the process user time limit. It is enforced per process. Set to 0 (zero) to disable the limit.
        /// </summary>
        /// <value>
        /// The process user time limit.
        /// </value>
        TimeSpan ProcessUserTimeLimit { get; set; }

        /// <summary>
        /// Gets or sets the Job Object user time limit. Every process user time is accounted. Set to 0 (zero) to disable the limit.
        /// </summary>
        /// <value>
        /// The job user time limit.
        /// </value>
        TimeSpan JobUserTimeLimit { get; set; }

        /// <summary>
        /// Gets or sets the priority class of the Job Object.
        /// </summary>
        /// <value>
        /// The priority class.
        /// </value>
        ProcessPriorityClass PriorityClass { get; set; }

        /// <summary>
        /// Gets or sets the scheduling class of the JobObject.
        /// </summary>
        /// <value>
        /// The scheduling class.
        /// </value>
        int SchedulingClass { get; set; }

        /// <summary>
        /// Gets or sets the processor affinity, enforced for every process.
        /// </summary>
        /// <value>
        /// The affinity.
        /// </value>
        IntPtr Affinity { get; set; }

        /// <summary>
        /// Gets the total IO bytes read by every process in the Job Object.
        /// </summary>
        long IOReadBytes { get; }

        /// <summary>
        ///     Gets the total IO bytes written by every process in the Job Object.
        /// </summary>
        long IOWriteBytes { get; }

        /// <summary>
        /// Gets the total IO bytes used in other operations by every process in the Job Object.
        /// </summary>
        long IOOtherBytes { get; }

        /// <summary>
        /// Gets the IO read operations count preformed by every process in the Job Object.
        /// </summary>
        long IOReadOperationsCount { get; }

        /// <summary>
        /// Gets the IO write operations count preformed by every process in the Job Object.
        /// </summary>
        long IOWriteOperationsCount { get; }

        /// <summary>
        /// Gets the other IO operations count preformed by every process in the Job Object.
        /// </summary>
        long IOOtherOperationsCount { get; }

        /// <summary>
        /// Gets the peak memory in bytes used by the Job Object at any given time.
        /// </summary>
        long PeakJobMemory { get; }

        /// <summary>
        /// Gets the peak memory in bytes used by a process.
        /// </summary>
        long PeakProcessMemory { get; }

        /// <summary>
        /// Gets the total processor time used by each process.
        /// </summary>
        TimeSpan TotalProcessorTime { get; }

        /// <summary>
        /// Gets the user processor time used by each process.
        /// </summary>
        TimeSpan UserProcessorTime { get; }

        /// <summary>
        /// Gets the kernel processor time used by each process.
        /// </summary>
        TimeSpan KernelProcessorTime { get; }

        /// <summary>
        /// Gets the working set memory in bytes of the Job Object.
        /// </summary>
        long WorkingSetMemory { get; }

        /// <summary>
        /// Gets the virtual memory in bytes.
        /// </summary>
        long VirtualMemory { get; }

        /// <summary>
        /// Gets the private memory in bytes.
        /// </summary>
        long PrivateMemory { get; }

        /// <summary>
        /// Gets the paged memory in bytes.
        /// </summary>
        long PagedMemory { get; }

        /// <summary>
        /// Gets the paged system memory in bytes.
        /// </summary>
        long PagedSystemMemory { get; }

        /// <summary>
        /// Gets the non paged system memory in bytes.
        /// </summary>
        long NonPagedSystemMemory { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Adds a process to the current Job Object, for which the Job Object limits apply.
        /// </summary>
        /// <param name="process">The process to be added.</param>
        void AddProcess(Process process);

        /// <summary>
        /// Determines whether the specified process is in the Job Object
        /// </summary>
        /// <param name="process">The process to be checked for.</param>
        /// <returns>
        ///   <c>true</c> if the specified process has the process; otherwise, <c>false</c>.
        /// </returns>
        bool HasProcess(Process process);

        /// <summary>
        /// Terminates all the processes in the Job Object.
        /// </summary>
        /// <param name="exitCode">The exit code.</param>
        void TerminateProcesses(int exitCode);

        /// <summary>
        /// Gets all the processes included in the Job Object.
        /// </summary>
        /// <returns>
        /// The list of processes.
        /// </returns>
        Process[] GetJobProcesses();
    }
}