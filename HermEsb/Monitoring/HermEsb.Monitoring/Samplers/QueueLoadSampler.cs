using HermEsb.Core.Messages.Monitoring;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Monitoring.Frequences;
using HermEsb.Monitoring.Messages;

namespace HermEsb.Monitoring.Samplers
{
    /// <summary>
    /// 
    /// </summary>
    [SamplerFrequenceLevel(Frequence = FrequenceLevel.Normal)]
    public class QueueLoadSampler : Sampler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueueLoadSampler"/> class.
        /// </summary>
        /// <param name="monitorableProcessor">The monitorable processor.</param>
        public QueueLoadSampler(IMonitorableProcessor monitorableProcessor) : base(monitorableProcessor){}

        /// <summary>
        /// Samplers the task.
        /// </summary>
        protected override void SamplerTask()
        {
            var message = MonitoringMessageFactory.Create<QueueLoadMessage>(MonitorableProcessor);
            message.TotalMessages = MonitorableProcessor.MonitorableInputGateway.Count();

            MonitoringSender.Send(message);
        }

        /// <summary>
        /// Configures the sampler.
        /// </summary>
        protected override void ConfigureSampler()
        {}

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {}
    }
}
