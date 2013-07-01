using HermEsb.Core.Monitoring;
using HermEsb.Core.Monitoring.Frequences;

namespace HermEsb.Core.Test.Monitoring
{
    [SamplerFrequenceLevel(Frequence = FrequenceLevel.High)]
    public class SamplerFake : Sampler
    {
        private int CountMessages { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplerFake"/> class.
        /// </summary>
        /// <param name="monitorableProcessor">The monitorable processor.</param>
        public SamplerFake(IMonitorableProcessor monitorableProcessor)
            : base(monitorableProcessor)
        {
            CountMessages = 0;
        }

        protected override void SamplerTask() {}

        /// <summary>
        /// Configures the sampler.
        /// </summary>
        protected override void ConfigureSampler()
        {
            MonitorableProcessor.OnMessageReceived += MessageReceived;
        }

        /// <summary>
        /// Messages the received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Mrwesb.Core.Monitoring.MonitorEventArgs"/> instance containing the event data.</param>
        private void MessageReceived(object sender, MonitorEventArgs args)
        {
            if (Status != MonitorStatus.Started)
            {
                return;
            }

            CountMessages++;
            if (CountMessages % 10 == 0)
            {
                MonitoringSender.Send(new MonitoringMessageFake { Count = CountMessages });
            }
        }


        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                MonitorableProcessor.OnMessageReceived -= MessageReceived;
            }
        }
    }
}