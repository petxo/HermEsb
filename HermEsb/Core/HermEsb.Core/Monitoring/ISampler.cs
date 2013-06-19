using System;
using HermEsb.Core.Communication;

namespace HermEsb.Core.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISampler : IStartable<MonitorStatus>, IDisposable
    {
        /// <summary>
        /// Gets the bus.
        /// </summary>
        /// <value>The bus.</value>
        IMonitoringSender MonitoringSender { get; set; }
    }
}