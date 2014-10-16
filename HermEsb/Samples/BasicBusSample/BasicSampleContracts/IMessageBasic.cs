using System;
using System.Runtime.Serialization;
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

        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>

        string Nombre { get; set; }
    }
}