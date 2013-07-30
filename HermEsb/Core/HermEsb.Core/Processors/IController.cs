using System;
using HermEsb.Core.Clustering;
using HermEsb.Core.Communication;
using HermEsb.Core.Monitoring;

namespace HermEsb.Core.Processors
{
    /// <summary>
    /// 
    /// </summary>
    public interface IController : IStartable<ProcessorStatus>, IDisposable, IBus, IReceiver
    {
        /// <summary>
        /// Gets or sets the proccesor.
        /// </summary>
        /// <value>The proccesor.</value>
        IProcessor Processor { get; set; }

        /// <summary>
        /// Gets or sets the monitor.
        /// </summary>
        /// <value>The monitor.</value>
        IMonitor Monitor { get; set; }

        /// <summary>
        /// Gets or sets the cluster controller.
        /// </summary>
        /// <value>
        /// The cluster controller.
        /// </value>
        IClusterController ClusterController { get; set; }
    }
}