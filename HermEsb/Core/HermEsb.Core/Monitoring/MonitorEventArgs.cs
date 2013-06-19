using System;

namespace HermEsb.Core.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public class MonitorEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the message created at.
        /// </summary>
        /// <value>The message created at.</value>
        public DateTime MessageCreatedAt { get; set; }
    }
}