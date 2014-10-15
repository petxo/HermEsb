using HermEsb.Core.Messages.Monitoring;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Monitoring.Frequences;
using HermEsb.Monitoring.Messages;

namespace HermEsb.Monitoring.Samplers
{
    /// <summary>
    /// 
    /// </summary>
    [SamplerFrequenceLevel(Frequence = FrequenceLevel.High)]
    public class HeartBeatSampler : Sampler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeartBeatSampler"/> class.
        /// </summary>
        /// <param name="monitorableProcessor">The monitorable processor.</param>
        public HeartBeatSampler(IMonitorableProcessor monitorableProcessor) : base(monitorableProcessor)
        {
            
        }

        /// <summary>
        /// Samplers the task.
        /// </summary>
        protected override void SamplerTask()
        {
            var message = MonitoringMessageFactory.Create<HeartBeatMessage>(MonitorableProcessor);
            message.Status = MonitorableProcessor.ProcessorStatus;

            MonitoringSender.Send(message);
        }

        /// <summary>
        /// Configures the sampler.
        /// </summary>
        protected override void ConfigureSampler() { }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)  
            {
            }
        }
    }
}
