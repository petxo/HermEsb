using System;
using System.Configuration;

namespace HermEsb.Extended.JobObjects.Configuration
{
    public interface ISecurityAttributesConfig
    {
        /// <summary>
        /// Gets or sets the B inherit handle.
        /// </summary>
        /// <value>
        /// The B inherit handle.
        /// </value>
        [ConfigurationProperty("bInheriHandle", DefaultValue = 0)]
        int BInheritHandle { get; }

        /// <summary>
        /// Gets or sets the lp security descriptor.
        /// </summary>
        /// <value>
        /// The lp security descriptor.
        /// </value>
        [ConfigurationProperty("lpSecurityDescriptor", DefaultValue = null)]
        IntPtr LpSecurityDescriptor { get; }

        /// <summary>
        /// Gets or sets the length of the N.
        /// </summary>
        /// <value>
        /// The length of the N.
        /// </value>
        [ConfigurationProperty("nLength", DefaultValue = (uint)0)]
        uint NLength { get; }
    }
}