using System.Threading;
using HermEsb.Core.Messages.Monitoring;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Monitoring.Frequences;
using HermEsb.Monitoring.Messages;

namespace HermEsb.Monitoring.Samplers
{
    [SamplerFrequenceLevel(Frequence = FrequenceLevel.Normal)]
    public class TransferVelocitySampler : Sampler
    {
        private int _inputSize;
        private int _outputSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransferVelocitySampler"/> class.
        /// </summary>
        /// <param name="monitorableProcessor">The monitorable processor.</param>
        public TransferVelocitySampler(IMonitorableProcessor monitorableProcessor)
            : base(monitorableProcessor)
        {

        }

        /// <summary>
        /// Samplers the task.
        /// </summary>
        protected override void SamplerTask()
        {
            var message = MonitoringMessageFactory.Create<TransferVelocityMessage>(MonitorableProcessor);
            message.Input.Total = (float) _inputSize / 1024;
            message.Input.Speed = message.Input.Total / Frequence;

            message.Output.Total = (float) _outputSize / 1024;
            message.Output.Speed = message.Output.Total / Frequence;

            MonitoringSender.Send(message);
            ReSet();
        }

        /// <summary>
        /// Res the set.
        /// </summary>
        private void ReSet()
        {
            _inputSize = 0;
            _outputSize = 0;
        }

        /// <summary>
        /// Configures the sampler.
        /// </summary>
        protected override void ConfigureSampler()
        {
            _inputSize = 0;
            _outputSize = 0;
            MonitorableProcessor.MonitorableInputGateway.OnMessageBusReceived +=
                (sender, args) => Interlocked.Add(ref _inputSize, args.Size);

            MonitorableProcessor.SenderGatewayChanged += (sender, args) => SetSenderEventGateway();

            SetSenderEventGateway();
        }

        /// <summary>
        /// Sets the sender event gateway.
        /// </summary>
        private void SetSenderEventGateway()
        {
            if (MonitorableProcessor.MonitorableSenderGateway != null)
                MonitorableProcessor.MonitorableSenderGateway.SentMessage += OnMonitorableSenderGatewayOnSentMessage;
        }

        /// <summary>
        /// Called when [monitorable sender gateway on sent message].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        private void OnMonitorableSenderGatewayOnSentMessage(object sender, MessageGatewayEventHandlerArgs args)
        {
            Interlocked.Add(ref _outputSize, args.Size);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                MonitorableProcessor.MonitorableSenderGateway.SentMessage -= OnMonitorableSenderGatewayOnSentMessage;
            }
        }
    }
}