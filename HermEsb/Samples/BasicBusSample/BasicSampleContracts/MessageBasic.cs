using System;

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

        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>

        public string Nombre { get; set; }
    }
}
