using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

namespace HermEsb.Extended.JobObjects
{
    /// <summary>
    /// Job Objects Windows API native methods.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal static class PlatformAPI
    {
        /// <summary>
        /// Used for calling the Win API
        /// </summary>
        internal enum JobObjectInfoClass
        {
            /// <summary>
            /// The lpJobObjectInfo parameter is a pointer to a JOBOBJECT_BASIC_ACCOUNTING_INFORMATION structure.
            /// </summary>
            JobObjectBasicAccountingInformation = 1,

            /// <summary>
            /// The lpJobObjectInfo parameter is a pointer to a JOBOBJECT_BASIC_AND_IO_ACCOUNTING_INFORMATION structure.
            /// </summary>
            [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Io", Justification = "Appropriate to suppress.")]
            JobObjectBasicAndIoAccountingInformation = 8,

            /// <summary>
            /// The lpJobObjectInfo parameter is a pointer to a JOBOBJECT_BASIC_LIMIT_INFORMATION structure.
            /// </summary>
            JobObjectBasicLimitInformation = 2,

            /// <summary>
            /// The lpJobObjectInfo parameter is a pointer to a JOBOBJECT_BASIC_PROCESS_ID_LIST structure.
            /// </summary>
            JobObjectBasicProcessIdList = 3,

            /// <summary>
            /// The lpJobObjectInfo parameter is a pointer to a JOBOBJECT_BASIC_UI_RESTRICTIONS structure.
            /// </summary>
            JobObjectBasicUIRestrictions = 4,

            /// <summary>
            /// The lpJobObjectInfo parameter is a pointer to a JOBOBJECT_END_OF_JOB_TIME_INFORMATION structure.
            /// </summary>
            JobObjectEndOfJobTimeInformation = 6,

            /// <summary>
            /// The lpJobObjectInfo parameter is a pointer to a JOBOBJECT_EXTENDED_LIMIT_INFORMATION structure.
            /// </summary>
            JobObjectExtendedLimitInformation = 9,

            /// <summary>
            /// The lpJobObjectInfo parameter is a pointer to a JOBOBJECT_ASSOCIATE_COMPLETION_PORT structure.
            /// </summary>
            JobObjectAssociateCompletionPortInformation = 7,
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern JobObjectHandle CreateJobObject(IntPtr lpJobAttributes, string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsProcessInJob(IntPtr process, JobObjectHandle hJob, [MarshalAs(UnmanagedType.Bool)] out bool result);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetInformationJobObject(JobObjectHandle hJob, JobObjectInfoClass jobObjectInfoClass, IntPtr lpJobObjectInfo, uint cbJobObjectInfoLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool QueryInformationJobObject(JobObjectHandle hJob, JobObjectInfoClass jobObjectInformationClass, IntPtr lpJobObjectInfo, uint cbJobObjectInfoLength, IntPtr lpReturnLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AssignProcessToJobObject(JobObjectHandle hJob, IntPtr hProcess);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TerminateJobObject(JobObjectHandle hJob, uint uExitCode);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern JobObjectHandle OpenJobObject(uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hHandle);

        /// <summary>
        /// The SECURITY_ATTRIBUTES structure contains the security descriptor for an object and specifies whether the handle retrieved by specifying this structure is inheritable.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Preserve WinAPI naming.")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct SecurityAttributes
        {
            /// <summary>
            /// Length of the structure.
            /// </summary>
            public int Length;

            /// <summary>
            /// Security Descriptor
            /// </summary>
            public IntPtr SecurityDescriptor;

            /// <summary>
            /// Child process inheritance.
            /// </summary>
            public int InheritHandle;
        }

        /// <summary>
        /// Contains basic and extended limit information for a job object.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct JobObjectBasicLimitInformation
        {
            /// <summary>
            /// JOB_OBJECT_LIMIT_ACTIVE_PROCESS Windows API constant.
            /// </summary>
            public const uint JobObjectLimitActiveProcess = 0x00000008;

            /// <summary>
            /// JOB_OBJECT_LIMIT_AFFINITY Windows API constant.
            /// </summary>
            public const uint JobObjectLimitAffinity = 0x00000010;

            /// <summary>
            /// JOB_OBJECT_LIMIT_BREAKAWAY_OK Windows API constant.
            /// </summary>
            public const uint JobObjectLimitBreakawayOk = 0x00000800;

            /// <summary>
            /// JOB_OBJECT_LIMIT_DIE_ON_UNHANDLED_EXCEPTION Windows API constant.
            /// </summary>
            public const uint JobObjectLimitDieOnUnhandledException = 0x00000400;

            /// <summary>
            /// JOB_OBJECT_LIMIT_JOB_MEMORY Windows API constant.
            /// </summary>
            public const uint JobObjectLimitJobMemory = 0x00000200;

            /// <summary>
            /// JOB_OBJECT_LIMIT_JOB_TIME Windows API constant.
            /// </summary>
            public const uint JobObjectLimitJobTime = 0x00000004;

            /// <summary>
            /// JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE Windows API constant.
            /// </summary>
            public const uint JobObjectLimitKillOnJobClose = 0x00002000;

            /// <summary>
            /// JOB_OBJECT_LIMIT_PRESERVE_JOB_TIME Windows API constant.
            /// </summary>
            public const uint JobObjectLimitPreserveJobTime = 0x00000040;

            /// <summary>
            /// JOB_OBJECT_LIMIT_PRIORITY_CLASS Windows API constant.
            /// </summary>
            public const uint JobObjectLimitPriorityClass = 0x00000020;

            /// <summary>
            /// JOB_OBJECT_LIMIT_PROCESS_MEMORY Windows API constant.
            /// </summary>
            public const uint JobObjectLimitProcessMemory = 0x00000100;

            /// <summary>
            /// JOB_OBJECT_LIMIT_PROCESS_TIME Windows API constant.
            /// </summary>
            public const uint JobObjectLimitProcessTime = 0x00000002;

            /// <summary>
            /// JOB_OBJECT_LIMIT_SCHEDULING_CLASS Windows API constant.
            /// </summary>
            public const uint JobObjectLimitSchedulingClass = 0x00000080;

            /// <summary>
            /// JOB_OBJECT_LIMIT_SILENT_BREAKAWAY_OK Windows API constant.
            /// </summary>
            public const uint JobObjectLimitSilentBreakawayOk = 0x00001000;

            /// <summary>
            /// JOB_OBJECT_LIMIT_SILENT_BREAKAWAY_OK Windows API constant.
            /// </summary>
            public const uint JobObjectLimitSubsetAffinity = 0x00004000;

            /// <summary>
            /// JOB_OBJECT_LIMIT_WORKINGSET Windows API constant.
            /// </summary>
            public const uint JobObjectLimitWorkingset = 0x00000001;

            /// <summary>
            /// Per process user time limit.
            /// </summary>
            public long PerProcessUserTimeLimit;

            /// <summary>
            /// Per job user time limit.
            /// </summary>
            public long PerJobUserTimeLimit;

            /// <summary>
            /// Limit flags.
            /// </summary>
            public uint LimitFlags;

            /// <summary>
            /// Minimum working set size.
            /// </summary>
            public IntPtr MinimumWorkingSetSize;

            /// <summary>
            /// Maximum working set size.
            /// </summary>
            public IntPtr MaximumWorkingSetSize;

            /// <summary>
            /// Active process limit.
            /// </summary>
            public uint ActiveProcessLimit;

            /// <summary>
            /// Processor affinity.
            /// </summary>
            public IntPtr Affinity;

            /// <summary>
            /// Priority class.
            /// </summary>
            public uint PriorityClass;

            /// <summary>
            /// Scheduling class.
            /// </summary>
            public uint SchedulingClass;
        }

        /// <summary>
        /// JOBOBJECT_BASIC_UI_RESTRICTIONS Windows API structure.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct JobobjectBasicUIRestrictions
        {
            /// <summary>
            /// UI Restrictions class.
            /// </summary>
            public uint UIRestrictionsClass;
        }

        /// <summary>
        /// JOBOBJECT_CPU_RATE_CONTROL_INFORMATION Windows API structure.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct JobobjectCpuRateControlInformation
        {
            /// <summary>
            /// Control Flags.
            /// </summary>
            public uint ControlFlags;

            /// <summary>
            /// CPU rate weight.
            /// </summary>
            public uint CpuRate_Weight;
        }

        /// <summary>
        /// JOBOBJECT_EXTENDED_LIMIT_INFORMATION Windows API structure.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct JobobjectExtendedLimitInformation
        {
            /// <summary>
            /// BasicLimitInformation Windows API structure member.
            /// </summary>
            public JobObjectBasicLimitInformation BasicLimitInformation;

            /// <summary>
            /// IoInfo Windows API structure member.
            /// </summary>
            public IOCounters IoInfo;

            /// <summary>
            /// ProcessMemoryLimit Windows API structure member.
            /// </summary>
            public IntPtr ProcessMemoryLimit;

            /// <summary>
            /// JobMemoryLimit Windows API structure member.
            /// </summary>
            public IntPtr JobMemoryLimit;

            /// <summary>
            /// PeakProcessMemoryUsed Windows API structure member.
            /// </summary>
            public IntPtr PeakProcessMemoryUsed;

            /// <summary>
            /// PeakJobMemoryUsed Windows API structure member.
            /// </summary>
            public IntPtr PeakJobMemoryUsed;
        }

        /// <summary>
        /// IO_COUNTERS Windows API structure.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct IOCounters
        {
            /// <summary>
            /// ReadOperationCount Windows API structure member.
            /// </summary>
            public ulong ReadOperationCount;

            /// <summary>
            /// WriteOperationCount Windows API structure member.
            /// </summary>
            public ulong WriteOperationCount;

            /// <summary>
            /// OtherOperationCount Windows API structure member.
            /// </summary>
            public ulong OtherOperationCount;

            /// <summary>
            /// ReadTransferCount Windows API structure member.
            /// </summary>
            public ulong ReadTransferCount;

            /// <summary>
            /// WriteTransferCount Windows API structure member.
            /// </summary>
            public ulong WriteTransferCount;

            /// <summary>
            /// OtherTransferCount Windows API structure member.
            /// </summary>
            public ulong OtherTransferCount;
        }

        /// <summary>
        /// JOBOBJECT_BASIC_ACCOUNTING_INFORMATION Windows API structure.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct JobObjectBasicAccountingInformation
        {
            /// <summary>
            /// TotalUserTime Windows API structure member.
            /// </summary>
            public ulong TotalUserTime;

            /// <summary>
            /// TotalKernelTime Windows API structure member.
            /// </summary>
            public ulong TotalKernelTime;

            /// <summary>
            /// ThisPeriodTotalUserTime Windows API structure member.
            /// </summary>
            public ulong ThisPeriodTotalUserTime;

            /// <summary>
            /// ThisPeriodTotalKernelTime Windows API structure member.
            /// </summary>
            public ulong ThisPeriodTotalKernelTime;

            /// <summary>
            /// TotalPageFaultCount Windows API structure member.
            /// </summary>
            public uint TotalPageFaultCount;

            /// <summary>
            /// TotalProcesses Windows API structure member.
            /// </summary>
            public uint TotalProcesses;

            /// <summary>
            /// ActiveProcesses Windows API structure member.
            /// </summary>
            public uint ActiveProcesses;

            /// <summary>
            /// TotalTerminatedProcesses Windows API structure member.
            /// </summary>
            public uint TotalTerminatedProcesses;
        }

        /// <summary>
        /// JOBOBJECT_BASIC_AND_IO_ACCOUNTING_INFORMATION Windows API structure.
        /// </summary>
        internal struct JobObjectBasicAndIOAccountingInformation
        {
            /// <summary>
            /// BasicInfo Windows API structure member.
            /// </summary>
            public JobObjectBasicAccountingInformation BasicInfo;

            /// <summary>
            /// IoInfo Windows API structure member.
            /// </summary>
            public IOCounters IoInfo;
        }

        /// <summary>
        /// JOBOBJECT_BASIC_PROCESS_ID_LIST Windows API structure.
        /// </summary>
        internal struct JobObjectBasicProcessIdList
        {
            /// <summary>
            /// The maximum number of processes that are allocated when querying Windows API. 
            /// </summary>
            public const uint MaxProcessListLength = 200;

            /// <summary>
            /// NumberOfAssignedProcesses Windows API structure member.
            /// </summary>
            public uint NumberOfAssignedProcesses;

            /// <summary>
            /// NumberOfProcessIdsInList Windows API structure member.
            /// </summary>
            public uint NumberOfProcessIdsInList;

            /// <summary>
            /// ProcessIdList Windows API structure member.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)MaxProcessListLength)]
            public IntPtr[] ProcessIdList;
        }
    }
}