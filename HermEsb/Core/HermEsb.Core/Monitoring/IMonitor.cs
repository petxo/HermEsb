using System;
using HermEsb.Core.Communication;
using HermEsb.Core.Processors;

namespace HermEsb.Core.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMonitor : IStartable<MonitorStatus>, IMonitoringSender, IDisposable
    {
        /// <summary>
        /// Adds the sampler.
        /// </summary>
        /// <param name="sampler">The sampler.</param>
        void AddSampler(ISampler sampler);

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <value>The controller.</value>
        IController Controller { get; }
    }
}