using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using HermEsb.Core.Messages;

namespace BasicSampleContracts
{
    [DataContract]
    public class MessageBasic : IMessageBasic
    {
        /// <summary>
        /// Gets or sets the fecha.
        /// </summary>
        /// <value>
        /// The fecha.
        /// </value>
        [DataMember]
        public DateTime Fecha { get; set; }
    }
}
