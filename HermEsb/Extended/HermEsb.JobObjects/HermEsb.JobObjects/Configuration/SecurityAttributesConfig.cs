using System;
using System.ComponentModel;
using System.Configuration;
using HermEsb.Extended.JobObjects.Configuration.Converters;

namespace HermEsb.Extended.JobObjects.Configuration
{
    public class SecurityAttributesConfig : ConfigurationElement, ISecurityAttributesConfig
    {
        #region Implementation of ISecurityAttributesConfig

        /// <summary>
        /// Gets or sets the B inherit handle.
        /// </summary>
        /// <value>
        /// The B inherit handle.
        /// </value>
        [ConfigurationProperty("bInheritHandle", DefaultValue = 0)]
        public int BInheritHandle { get { return (int) this["bInheritHandle"]; } }

        /// <summary>
        /// Gets or sets the lp security descriptor.
        /// </summary>
        /// <value>
        /// The lp security descriptor.
        /// </value>
        [ConfigurationProperty("lpSecurityDescriptor", DefaultValue = null)]
        [TypeConverter(typeof(IntPtrConverter))]
        public IntPtr LpSecurityDescriptor { get { return (IntPtr) this["lpSecurityDescriptor"]; } }

        /// <summary>
        /// Gets or sets the length of the N.
        /// </summary>
        /// <value>
        /// The length of the N.
        /// </value>
        [ConfigurationProperty("nLength", DefaultValue = (uint)0)]
        public uint NLength { get { return (uint) this["nLength"]; } }

        #endregion
    }
}