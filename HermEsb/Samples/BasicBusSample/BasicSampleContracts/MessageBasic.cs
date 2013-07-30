using System;
using System.Collections.Generic;

namespace BasicSampleContracts
{
    public class MessageBasic : IMessageBasic
    {
        /// <summary>
        /// Gets or sets the fecha.
        /// </summary>
        /// <value>
        /// The fecha.
        /// </value>
        public DateTime Fecha { get; set; }

        public SubMessage Data { get; set; }

        public IList<int> ListaEnteros { get; set; }
    }

    public class SubMessage
    {
        public DateTime OtraFecha { get; set; }

        public IList<string> Data { get; set; }
    }
}
