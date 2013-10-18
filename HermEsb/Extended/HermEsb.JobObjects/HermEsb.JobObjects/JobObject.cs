using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using HermEsb.Extended.JobObjects.Configuration;

namespace HermEsb.Extended.JobObjects
{
    /// <summary>
    ///     Is a class that manages a Windows Job Object. Job Objects allows to group a number of processes and
    ///     perform some aggregate operations. It is a good tool for enforcing resource sandboxing for processes.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation",Justification = "Preserve WinAPI naming.")]
    [SecurityPermission(SecurityAction.LinkDemand), SuppressMessage("Microsoft.Security", "CA2126:TypeLinkDemandsRequireInheritanceDemands", Justification = "Appropriate to suppress."), SuppressMessage("Microsoft.Security", "CA2135:SecurityRuleSetLevel2MethodsShouldNotBeProtectedWithLinkDemandsFxCopRule", Justification = "Appropriate to suppress.")]
    public class JobObject : IDisposable, IJobObject
    {
        /// <summary>
        ///     The Windows Handle
        /// </summary>
        private readonly JobObjectHandle _jobHandle;

        /// <summary>
        ///     Maximum number of active processes.
        /// </summary>
        private uint _activeProcessesLimit;

        /// <summary>
        ///     The processor affinity.
        /// </summary>
        private IntPtr _affinity = IntPtr.Zero;

        /// <summary>
        ///     Allow child process breakaway.
        /// </summary>
        private bool _allowChildProcessesBreakaway;

        /// <summary>
        ///     Breakaway child processes by default.
        /// </summary>
        private bool _alwaysBreakawayChildProcesses;

        /// <summary>
        ///     Die on unhandled exception.
        /// </summary>
        private bool _dieOnUnhandledException;

        /// <summary>
        ///     Total IO bytes used in other operations.
        /// </summary>
        private long _ioOtherBytes;

        /// <summary>
        ///     Other IO operations count for the while job.
        /// </summary>
        private long _ioOtherOperationsCount;

        /// <summary>
        ///     Total IO bytes read by the job.
        /// </summary>
        private long _ioReadBytes;

        /// <summary>
        ///     The read IO operations count for the whole job.
        /// </summary>
        private long _ioReadOperationsCount;

        /// <summary>
        ///     Total IO bytes written by the job.
        /// </summary>
        private long _ioWriteBytes;

        /// <summary>
        ///     The write IO operations count for the whole job.
        /// </summary>
        private long _ioWriteOperationsCount;

        /// <summary>
        ///     The memory limit of the Job Object.
        /// </summary>
        private long _jobMemoryLimit;

        /// <summary>
        ///     The processes in the job.
        /// </summary>
        private Process[] _jobProcesses;

        /// <summary>
        ///     The processor user time limit for the job.
        /// </summary>
        private long _jobUserTimeLimit;

        /// <summary>
        ///     Flag if the job processor user time limit has changed.
        /// </summary>
        private bool _jobUserTimeLimitChanged;

        /// <summary>
        ///     Total kernel processor time used by the job.
        /// </summary>
        private ulong _kernelProcessorTime;

        /// <summary>
        ///     Flag to kill processes on job close.
        /// </summary>
        private bool _killProcessesOnJobClose = true;

        /// <summary>
        ///     Peak memory usage by the job.
        /// </summary>
        private ulong _peakJobMemory;

        /// <summary>
        ///     Peak memory usage by a process.
        /// </summary>
        private ulong _peakProcessMemory;

        /// <summary>
        ///     The priority class of the Job Object.
        /// </summary>
        private uint _priorityClass;

        /// <summary>
        ///     The memory limit of each process in the Job Object.
        /// </summary>
        private long _processMemoryLimit;

        /// <summary>
        ///     The user time limit per each process in the job.
        /// </summary>
        private long _processUserTimeLimit;

        /// <summary>
        ///     The scheduling class.
        /// </summary>
        private uint _schedulingClass;

        /// <summary>
        ///     Total user processor time used by the job.
        /// </summary>
        private ulong _userProcessorTime;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Mrw.PoC.JobObjects.JobObject" /> class. The Windows Job Object is unnamed.
        /// </summary>
        protected JobObject() : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mrw.PoC.JobObjects.JobObject"/> class. If a Job Object with the specified name exists,
        /// then the Job Object is opened; if not, the Job Object with the specified named is opened.
        /// </summary>
        /// <param name="jobObjectName">Name of the job object.</param>
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CreateJobObject", Justification = "Appropriate to suppress."),
         SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OpenJobObject", Justification = "Appropriate to suppress.")]
        protected JobObject(string jobObjectName)
        {
            if (string.IsNullOrEmpty(jobObjectName))
            {
                jobObjectName = null;
            }

            _jobHandle = PlatformAPI.CreateJobObject(IntPtr.Zero, jobObjectName);
            if (_jobHandle.IsInvalid)
            {
                if (jobObjectName == null)
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error, "CreateJobObject failed.");
                }
                // JOB_OBJECT_ALL_ACCESS = 0x1F001F
                _jobHandle = PlatformAPI.OpenJobObject(0x1F001F, false, jobObjectName);
                if (_jobHandle.IsInvalid)
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error, "OpenJobObject failed.");
                }
            }

            UpdateExtendedLimit();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobObject"/> class.
        /// </summary>
        /// <param name="config">The config.</param>
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CreateJobObject", Justification = "Appropriate to suppress."),
         SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OpenJobObject", Justification = "Appropriate to suppress.")]
        public JobObject(IJobObjectConfig config) : this (config.Name)
        {
            SetBasicLimitConfig(config.BasicLimitInformation);
            SetExtendedLimitConfig(config.ExtendedLimitInformation);
        }

        /// <summary>
        /// Applies the basic limit config.
        /// </summary>
        /// <param name="basicLimitInformation">The basic limit information.</param>
        private void SetBasicLimitConfig(IBasicLimitConfig basicLimitInformation)
        {
            ActiveProcessesLimit = basicLimitInformation.ActiveProcessesLimit;
            Affinity = basicLimitInformation.Affinity;           
            //LimitFlags = basicLimitInformation.LimitFlags;
            //MaximumWorkingSetSize = basicLimitInformation.MaximumWorkingSetSize;
            //MinimumWorkingSetSize = basicLimitInformation.MinimumWorkingSetSize;
            JobUserTimeLimit = basicLimitInformation.PerJobUserTimeLimit;
            ProcessUserTimeLimit = basicLimitInformation.PerProcessUserTimeLimit;
            PriorityClass = basicLimitInformation.PriorityClass;
            SchedulingClass = basicLimitInformation.SchedulingClass;
        }

        /// <summary>
        /// Applies the extended limit config.
        /// </summary>
        /// <param name="extendedLimitConfig">The extended limit config.</param>
        private void SetExtendedLimitConfig(IExtendedLimitConfig extendedLimitConfig)
        {
            SetBasicLimitConfig(extendedLimitConfig.BasicLimitInfo);
            JobMemoryLimit = extendedLimitConfig.JobMemoryLimit;
            ProcessMemoryLimit = extendedLimitConfig.ProcessMemoryLimit;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to kill the processes when the Job Object is closed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [kill processes on job close]; otherwise, <c>false</c>.
        /// </value>
        public bool KillProcessesOnJobClose
        {
            get { return _killProcessesOnJobClose; }
            set
            {
                _killProcessesOnJobClose = value;
                UpdateExtendedLimit();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a process to die on an unhandled exception.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [die on unhandled exception]; otherwise, <c>false</c>.
        /// </value>
        public bool DieOnUnhandledException
        {
            get { return _dieOnUnhandledException; }
            set
            {
                _dieOnUnhandledException = value;
                UpdateExtendedLimit();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether processes are allowed to create processes outside the Job Object.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [allow child processes breakaway]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowChildProcessesBreakaway
        {
            get { return _allowChildProcessesBreakaway; }
            set
            {
                _allowChildProcessesBreakaway = value;
                UpdateExtendedLimit();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether child processes are not added to the Job Object.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [always breakaway child processes]; otherwise, <c>false</c>.
        /// </value>
        public bool AlwaysBreakawayChildProcesses
        {
            get { return _alwaysBreakawayChildProcesses; }
            set
            {
                _alwaysBreakawayChildProcesses = value;
                UpdateExtendedLimit();
            }
        }

        /// <summary>
        /// Gets or sets the active processes in the Job Object. Set to 0 (zero) to disable the limit.
        /// </summary>
        /// <value>
        /// The active processes.
        /// </value>
        public int ActiveProcessesLimit
        {
            get { return (int) _activeProcessesLimit; }
            set
            {
                _activeProcessesLimit = (uint) value;
                UpdateExtendedLimit();
            }
        }

        /// <summary>
        /// Gets or sets the memory in bytes limit enforced per process. Set to 0 (zero) to disable the limit.
        /// </summary>
        /// <value>
        /// The process memory limit.
        /// </value>
        public long ProcessMemoryLimit
        {
            get { return _processMemoryLimit; }
            set
            {
                _processMemoryLimit = value;
                UpdateExtendedLimit();
            }
        }

        /// <summary>
        /// Gets or sets the memory limit in bytes of the entire Job Object. Set to 0 (zero) to disable the limit.
        /// </summary>
        /// <value>
        /// The job memory limit.
        /// </value>
        public long JobMemoryLimit
        {
            get { return _jobMemoryLimit; }
            set
            {
                _jobMemoryLimit = value;
                UpdateExtendedLimit();
            }
        }

        /// <summary>
        /// Gets or sets the process user time limit. It is enforced per process. Set to 0 (zero) to disable the limit.
        /// </summary>
        /// <value>
        /// The process user time limit.
        /// </value>
        public TimeSpan ProcessUserTimeLimit
        {
            get { return new TimeSpan(_processUserTimeLimit); }
            set
            {
                _processUserTimeLimit = value.Ticks;
                UpdateExtendedLimit();
            }
        }

        /// <summary>
        /// Gets or sets the Job Object user time limit. Every process user time is accounted. Set to 0 (zero) to disable the limit.
        /// </summary>
        /// <value>
        /// The job user time limit.
        /// </value>
        public TimeSpan JobUserTimeLimit
        {
            get { return new TimeSpan(_jobUserTimeLimit); }
            set
            {
                _jobUserTimeLimit = value.Ticks;
                _jobUserTimeLimitChanged = true;
                UpdateExtendedLimit();
            }
        }

        /// <summary>
        /// Gets or sets the priority class of the Job Object.
        /// </summary>
        /// <value>
        /// The priority class.
        /// </value>
        public ProcessPriorityClass PriorityClass
        {
            get { return (ProcessPriorityClass) _priorityClass; }
            set
            {
                _priorityClass = (uint) value;
                UpdateExtendedLimit();
            }
        }

        /// <summary>
        /// Gets or sets the scheduling class of the JobObject.
        /// </summary>
        /// <value>
        /// The scheduling class.
        /// </value>
        public int SchedulingClass
        {
            get { return (int) _schedulingClass; }
            set
            {
                _schedulingClass = (uint) value;
                UpdateExtendedLimit();
            }
        }

        /// <summary>
        /// Gets or sets the processor affinity, enforced for every process.
        /// </summary>
        /// <value>
        /// The affinity.
        /// </value>
        public IntPtr Affinity
        {
            get { return _affinity; }
            set
            {
                _affinity = value;
                UpdateExtendedLimit();
            }
        }

        /// <summary>
        /// Gets the total IO bytes read by every process in the Job Object.
        /// </summary>
        public long IOReadBytes
        {
            get
            {
                QueryBasicAndIoAccounting();
                return _ioReadBytes;
            }
        }

        /// <summary>
        ///     Gets the total IO bytes written by every process in the Job Object.
        /// </summary>
        public long IOWriteBytes
        {
            get
            {
                QueryBasicAndIoAccounting();
                return _ioWriteBytes;
            }
        }

        /// <summary>
        /// Gets the total IO bytes used in other operations by every process in the Job Object.
        /// </summary>
        public long IOOtherBytes
        {
            get
            {
                QueryBasicAndIoAccounting();
                return _ioOtherBytes;
            }
        }

        /// <summary>
        /// Gets the IO read operations count preformed by every process in the Job Object.
        /// </summary>
        public long IOReadOperationsCount
        {
            get
            {
                QueryBasicAndIoAccounting();
                return _ioReadOperationsCount;
            }
        }

        /// <summary>
        /// Gets the IO write operations count preformed by every process in the Job Object.
        /// </summary>
        public long IOWriteOperationsCount
        {
            get
            {
                QueryBasicAndIoAccounting();
                return _ioWriteOperationsCount;
            }
        }

        /// <summary>
        /// Gets the other IO operations count preformed by every process in the Job Object.
        /// </summary>
        public long IOOtherOperationsCount
        {
            get
            {
                QueryBasicAndIoAccounting();
                return _ioOtherOperationsCount;
            }
        }

        /// <summary>
        /// Gets the peak memory in bytes used by the Job Object at any given time.
        /// </summary>
        public long PeakJobMemory
        {
            get
            {
                QueryExtendedLimitInformation();
                return (long) _peakJobMemory;
            }
        }

        /// <summary>
        /// Gets the peak memory in bytes used by a process.
        /// </summary>
        public long PeakProcessMemory
        {
            get
            {
                QueryExtendedLimitInformation();
                return (long) _peakProcessMemory;
            }
        }

        /// <summary>
        /// Gets the total processor time used by each process.
        /// </summary>
        public TimeSpan TotalProcessorTime
        {
            get
            {
                QueryBasicAndIoAccounting();
                return new TimeSpan((long) _userProcessorTime + (long) _kernelProcessorTime);
            }
        }

        /// <summary>
        /// Gets the user processor time used by each process.
        /// </summary>
        public TimeSpan UserProcessorTime
        {
            get
            {
                QueryBasicAndIoAccounting();
                return new TimeSpan((long) _userProcessorTime);
            }
        }

        /// <summary>
        /// Gets the kernel processor time used by each process.
        /// </summary>
        public TimeSpan KernelProcessorTime
        {
            get
            {
                QueryBasicAndIoAccounting();
                return new TimeSpan((long) _kernelProcessorTime);
            }
        }

        /// <summary>
        /// Gets the working set memory in bytes of the Job Object.
        /// </summary>
        public long WorkingSetMemory
        {
            get
            {
                QueryProcessIds();
                return _jobProcesses.Sum(p => p.WorkingSet64);
            }
        }

        /// <summary>
        /// Gets the virtual memory in bytes.
        /// </summary>
        public long VirtualMemory
        {
            get
            {
                QueryProcessIds();
                return _jobProcesses.Sum(p => p.VirtualMemorySize64);
            }
        }

        /// <summary>
        /// Gets the private memory in bytes.
        /// </summary>
        public long PrivateMemory
        {
            get
            {
                QueryProcessIds();
                return _jobProcesses.Sum(p => p.PrivateMemorySize64);
            }
        }

        /// <summary>
        /// Gets the paged memory in bytes.
        /// </summary>
        public long PagedMemory
        {
            get
            {
                QueryProcessIds();
                return _jobProcesses.Sum(p => p.PagedMemorySize64);
            }
        }

        /// <summary>
        /// Gets the paged system memory in bytes.
        /// </summary>
        public long PagedSystemMemory
        {
            get
            {
                QueryProcessIds();
                return _jobProcesses.Sum(p => p.PagedSystemMemorySize64);
            }
        }

        /// <summary>
        /// Gets the non paged system memory in bytes.
        /// </summary>
        public long NonPagedSystemMemory
        {
            get
            {
                QueryProcessIds();
                return _jobProcesses.Sum(p => p.NonpagedSystemMemorySize64);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the JobObject class. Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Mrw.PoC.JobObjects.JobObject"/> is reclaimed by garbage collection.
        /// </summary>
        ~JobObject()
        {
            Dispose(false);
        }

        /// <summary>
        /// Adds a process to the current Job Object, for which the Job Object limits apply.
        /// </summary>
        /// <param name="process">The process to be added.</param>
        public void AddProcess(Process process)
        {
            if (process == null)
            {
                throw new ArgumentNullException("process");
            }

            AddProcess(process.Handle);
        }

        /// <summary>
        /// Determines whether the specified process is in the Job Object
        /// </summary>
        /// <param name="process">The process to be checked for.</param>
        /// <returns>
        ///   <c>true</c> if the specified process has the process; otherwise, <c>false</c>.
        /// </returns>
        public bool HasProcess(Process process)
        {
            if (process == null)
            {
                throw new ArgumentNullException("process");
            }

            return HasProcess(process.Handle);
        }

        /// <summary>
        /// Terminates all the processes in the Job Object.
        /// </summary>
        /// <param name="exitCode">The exit code.</param>
        public void TerminateProcesses(int exitCode)
        {
            PlatformAPI.TerminateJobObject(_jobHandle, (uint) exitCode);
        }

        /// <summary>
        /// Gets all the processes included in the Job Object.
        /// </summary>
        /// <returns>
        /// The list of processes.
        /// </returns>
        public Process[] GetJobProcesses()
        {
            QueryProcessIds();
            return _jobProcesses;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _jobHandle.Close();
            }
        }

        /// <summary>
        /// Determines whether the specified process handle is included in the Job Object.
        /// </summary>
        /// <param name="processHandle">The process handle.</param>
        /// <returns>
        ///   <c>true</c> if the specified process handle has process; otherwise, <c>false</c>.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IsProcessInJob", Justification = "Appropriate to suppress.")]
        private bool HasProcess(IntPtr processHandle)
        {
            bool result;
            var success = PlatformAPI.IsProcessInJob(processHandle, _jobHandle, out result);
            if (!success)
            {
                var error = Marshal.GetLastWin32Error();
                throw new Win32Exception(error, "IsProcessInJob failed.");
            }

            return result;
        }

        /// <summary>
        /// Adds the process handle to the Job object.
        /// </summary>
        /// <param name="processHandle">The process handle.</param>
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AssignProcessToJobObject", Justification = "Appropriate to suppress.")]
        private void AddProcess(IntPtr processHandle)
        {
            var success = PlatformAPI.AssignProcessToJobObject(_jobHandle, processHandle);
            if (success) return;
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// Queries the process ids.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "QueryInformationJobObject", Justification = "Appropriate to suppress.")]
        private void QueryProcessIds()
        {
            var processList = new PlatformAPI.JobObjectBasicProcessIdList
                {
                    NumberOfAssignedProcesses = PlatformAPI.JobObjectBasicProcessIdList.MaxProcessListLength,
                    NumberOfProcessIdsInList = 0,
                    ProcessIdList = null
                };

            var processListLength = Marshal.SizeOf(typeof (PlatformAPI.JobObjectBasicProcessIdList));
            var processListPtr = Marshal.AllocHGlobal(processListLength);

            try
            {
                Marshal.StructureToPtr(processList, processListPtr, false);

                var success = PlatformAPI.QueryInformationJobObject(_jobHandle, PlatformAPI.JobObjectInfoClass.JobObjectBasicProcessIdList,
                                                                       processListPtr, (uint) processListLength,
                                                                       IntPtr.Zero);
                if (success == false)
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error, "QueryInformationJobObject failed.");
                }
                processList = (PlatformAPI.JobObjectBasicProcessIdList) Marshal.PtrToStructure(processListPtr, typeof (PlatformAPI.JobObjectBasicProcessIdList));
                var pss = new List<Process>();
                for (var i = 0; i < processList.NumberOfProcessIdsInList; i++)
                {
                    try
                    {
                        pss.Add(Process.GetProcessById((int) processList.ProcessIdList[i]));
                    }
                    catch (ArgumentException)
                    {
                    }
                }
                _jobProcesses = pss.ToArray();
            }
            finally
            {
                Marshal.FreeHGlobal(processListPtr);
            }
        }

        /// <summary>
        ///     Updates the extended limit.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SetInformationJobObject", Justification = "Appropriate to suppress.")]
        private void UpdateExtendedLimit()
        {
            var extendedLimit = new PlatformAPI.JobobjectExtendedLimitInformation();
            var basicLimit = new PlatformAPI.JobObjectBasicLimitInformation {LimitFlags = 0x0};
            if (_killProcessesOnJobClose)
            {
                basicLimit.LimitFlags |= PlatformAPI.JobObjectBasicLimitInformation.JobObjectLimitKillOnJobClose;
            }
            if (_dieOnUnhandledException)
            {
                basicLimit.LimitFlags |= PlatformAPI.JobObjectBasicLimitInformation.JobObjectLimitDieOnUnhandledException;
            }
            if (_allowChildProcessesBreakaway)
            {
                basicLimit.LimitFlags |= PlatformAPI.JobObjectBasicLimitInformation.JobObjectLimitBreakawayOk;
            }
            if (_alwaysBreakawayChildProcesses)
            {
                basicLimit.LimitFlags |= PlatformAPI.JobObjectBasicLimitInformation.JobObjectLimitSilentBreakawayOk;
            }
            if (_activeProcessesLimit != 0)
            {
                basicLimit.LimitFlags |= PlatformAPI.JobObjectBasicLimitInformation.JobObjectLimitActiveProcess;
                basicLimit.ActiveProcessLimit = _activeProcessesLimit;
            }
            if (_processMemoryLimit != 0)
            {
                basicLimit.LimitFlags |= PlatformAPI.JobObjectBasicLimitInformation.JobObjectLimitProcessMemory;
                extendedLimit.ProcessMemoryLimit = (IntPtr) _processMemoryLimit;
            }
            if (_jobMemoryLimit != 0)
            {
                basicLimit.LimitFlags |= PlatformAPI.JobObjectBasicLimitInformation.JobObjectLimitJobMemory;
                extendedLimit.JobMemoryLimit = (IntPtr) _jobMemoryLimit;
            }
            if (_processUserTimeLimit != 0)
            {
                basicLimit.LimitFlags |= PlatformAPI.JobObjectBasicLimitInformation.JobObjectLimitProcessTime;
                basicLimit.PerProcessUserTimeLimit = _processUserTimeLimit;
            }
            if (_jobUserTimeLimit != 0)
            {
                if (_jobUserTimeLimitChanged)
                {
                    basicLimit.LimitFlags |= PlatformAPI.JobObjectBasicLimitInformation.JobObjectLimitJobTime;
                    basicLimit.PerJobUserTimeLimit = _jobUserTimeLimit;
                    _jobUserTimeLimitChanged = false;
                }
                else
                {
                    basicLimit.LimitFlags |=
                        PlatformAPI.JobObjectBasicLimitInformation.JobObjectLimitPreserveJobTime;
                }
            }
            if (_priorityClass != 0)
            {
                basicLimit.LimitFlags |= PlatformAPI.JobObjectBasicLimitInformation.JobObjectLimitPriorityClass;
                basicLimit.PriorityClass = _priorityClass;
            }
            if (_schedulingClass != 0)
            {
                basicLimit.LimitFlags |=
                    PlatformAPI.JobObjectBasicLimitInformation.JobObjectLimitSchedulingClass;
                basicLimit.SchedulingClass = _schedulingClass;
            }
            if (_affinity != IntPtr.Zero)
            {
                basicLimit.LimitFlags |= PlatformAPI.JobObjectBasicLimitInformation.JobObjectLimitAffinity;
                basicLimit.Affinity = _affinity;
            }
            extendedLimit.BasicLimitInformation = basicLimit;
            var extendedLimitLength = Marshal.SizeOf(typeof (PlatformAPI.JobobjectExtendedLimitInformation));
            var extendedLimitPtr = Marshal.AllocHGlobal(extendedLimitLength);
            try
            {
                Marshal.StructureToPtr(extendedLimit, extendedLimitPtr, false);
                var success = PlatformAPI.SetInformationJobObject(_jobHandle,
                                                                     PlatformAPI.JobObjectInfoClass.JobObjectExtendedLimitInformation,
                                                                     extendedLimitPtr, (uint) extendedLimitLength);
                if (success) return;
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            finally
            {
                Marshal.FreeHGlobal(extendedLimitPtr);
            }
        }

        /// <summary>
        /// Queries the basic and IO accounting.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "QueryInformationJobObject", Justification = "Appropriate to suppress.")]
        private void QueryBasicAndIoAccounting()
        {
            var accountingLength = Marshal.SizeOf(typeof (PlatformAPI.JobObjectBasicAndIOAccountingInformation));
            var accountingPtr = Marshal.AllocHGlobal(accountingLength);
            try
            {
                var success = PlatformAPI.QueryInformationJobObject(_jobHandle,
                                                                       PlatformAPI.JobObjectInfoClass.JobObjectBasicAndIoAccountingInformation,
                                                                       accountingPtr, (uint) accountingLength,
                                                                       IntPtr.Zero);
                if (!success)
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error, "QueryInformationJobObject failed.");
                }
                var accounting = (PlatformAPI.JobObjectBasicAndIOAccountingInformation) Marshal.PtrToStructure(accountingPtr, typeof (PlatformAPI.JobObjectBasicAndIOAccountingInformation));
                _userProcessorTime = accounting.BasicInfo.TotalUserTime;
                _kernelProcessorTime = accounting.BasicInfo.TotalKernelTime;

                _ioReadBytes = (long) accounting.IoInfo.ReadTransferCount;
                _ioWriteBytes = (long) accounting.IoInfo.WriteTransferCount;
                _ioOtherBytes = (long) accounting.IoInfo.OtherTransferCount;
                _ioReadOperationsCount = (long) accounting.IoInfo.ReadOperationCount;
                _ioWriteOperationsCount = (long) accounting.IoInfo.WriteOperationCount;
                _ioOtherOperationsCount = (long) accounting.IoInfo.OtherOperationCount;
            }
            finally
            {
                Marshal.FreeHGlobal(accountingPtr);
            }
        }

        /// <summary>
        ///     Queries the extended limit information.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "QueryInformationJobObject", Justification = "Spelling is correct.")]
        private void QueryExtendedLimitInformation()
        {
            var extenedLimitLength = Marshal.SizeOf(typeof (PlatformAPI.JobobjectExtendedLimitInformation));
            var extendedLimitPtr = Marshal.AllocHGlobal(extenedLimitLength);
            try
            {
                var success = PlatformAPI.QueryInformationJobObject(_jobHandle,
                                                                       PlatformAPI.JobObjectInfoClass.JobObjectExtendedLimitInformation,
                                                                       extendedLimitPtr, (uint) extenedLimitLength,
                                                                       IntPtr.Zero);
                if (!success)
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error, "QueryInformationJobObject failed.");
                }
                var extendedLimit = (PlatformAPI.JobobjectExtendedLimitInformation) Marshal.PtrToStructure(extendedLimitPtr, typeof (PlatformAPI.JobobjectExtendedLimitInformation));
                _peakJobMemory = (ulong) extendedLimit.PeakJobMemoryUsed;
                _peakProcessMemory = (ulong) extendedLimit.PeakProcessMemoryUsed;
            }
            finally
            {
                Marshal.FreeHGlobal(extendedLimitPtr);
            }
        }
    }
}