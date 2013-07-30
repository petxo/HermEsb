using System;
using System.Collections.Generic;
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

        SubMessage Data { get; set; }
        IList<int> ListaEnteros { get; set; }
    }
}