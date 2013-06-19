using System;
using HermEsb.Core.Messages;

namespace BasicSampleContracts
{
    public interface IMessageBasic : IMessage
    {
        /// <summary>
        /// Gets or sets the fecha.
        /// </summary>
        /// <value>
        /// The fecha.
        /// </value>
        DateTime Fecha { get; set; }
    }
}