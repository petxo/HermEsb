using System;
using System.Threading;
using HermEsb.Core.Messages.Monitoring;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Monitoring.Frequences;
using HermEsb.Monitoring.Messages;

namespace HermEsb.Monitoring.Samplers
{
    [SamplerFrequenceLevel(Frequence = FrequenceLevel.Normal)]
    public class ProcessingVelocitySampler : Sampler
    {
        private int _messageProcessedCounter;
        private float _lastLatency = 0;
        private float _maxLatency = 0;
        private float _minLatency = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessingVelocitySampler"/> class.
        /// </summary>
        /// <param name="monitorableProcessor">The monitorable processor.</param>
        public ProcessingVelocitySampler(IMonitorableProcessor monitorableProcessor)
            : base(monitorableProcessor)
        {
            MonitorableProcessor.MonitorableInputGateway.OnMessageBusReceived += OnMessageBusReceived;
        }

        /// <summary>
        /// Samplers the task.
        /// </summary>
        protected override void SamplerTask()
        {
            Thread.MemoryBarrier();

            var numMessages = _messageProcessedCounter;
            var snapshotLatency = _lastLatency;
            var peakMax = _maxLatency;
            var peakMin = _minLatency;

            Thread.MemoryBarrier();

            var message = MonitoringMessageFactory.Create<ProcessingVelocityMessage>(MonitorableProcessor);
            message.NumberMessagesProcessed = numMessages;
            message.Velocity = (float)numMessages / Frequence;
            message.Latency = (float)Math.Round(snapshotLatency / 1000,5);
            message.PeakMaxLatency = (float)Math.Round(peakMax / 1000, 5);
            message.PeakMinLatency = (float)Math.Round(peakMin / 1000, 5);

            Logger.Info(String.Format("NumberMessagesProcessed {0}", numMessages));
            Logger.Info(String.Format("Latency {0}", snapshotLatency));
            Logger.Info(String.Format("PeakMaxLatency {0}", peakMax));
            Logger.Info(String.Format("PeakMinLatency {0}", peakMin));
            Logger.Info(String.Format("Velocity {0}", message.Velocity));

            ReSet();

            MonitoringSender.Send(message);
        }

        /// <summary>
        /// Called when [message received].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Mrwesb.Core.Monitoring.MonitorEventArgs"/> instance containing the event data.</param>
        private void OnMessageBusReceived(object sender, MessageGatewayEventHandlerArgs args)
        {
            var time = DateTime.UtcNow.Subtract(args.CreatedAt);
            var messages = Interlocked.Increment(ref _messageProcessedCounter);

            CalculateAverage(messages, (float)time.TotalMilliseconds);
            
            Logger.Debug(String.Format("OnMessageBusReceived => CreatedAt {0}", args.CreatedAt));
            Logger.Debug(String.Format("                     => NumberMessagesProcessed  {0}", messages));
            Logger.Debug(String.Format("                     => Sustract  {0}", time.TotalMilliseconds));
            Logger.Debug(String.Format("                     => Latency  {0}", _lastLatency));
        }

        /// <summary>
        /// Calculates the average. [[(n-1)*Xn-1]+Xn]/n
        /// </summary>
        /// <param name="n">The n.</param>
        /// <param name="lastSampler">The lastSampler.</param>
        private void CalculateAverage(int n, float lastSampler)
        {
            if (n == 0) return;

            var spinWait = new SpinWait();

            if (n == 1)
            {
                Interlocked.Exchange(ref _maxLatency, lastSampler);
                Interlocked.Exchange(ref _minLatency, lastSampler);
            }

            _lastLatency = ((_lastLatency * (n - 1)) + lastSampler) / n;

            if (lastSampler > _maxLatency) Interlocked.Exchange(ref _maxLatency, lastSampler);
            if (lastSampler < _minLatency) Interlocked.Exchange(ref _minLatency, lastSampler);

            spinWait.SpinOnce();
        }

        /// <summary>
        /// Locks the free update.
        /// http://www.albahari.com/threading/part5.aspx
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="updater">The updater.</param>
        void LockFreeFloatUpdate(ref float field, Func<float, float> updater)
        {
            var spinWait = new SpinWait();
            while (true)
            {
                var snapshot1 = field;
                var average = updater(snapshot1);
                var snapshot2 = Interlocked.CompareExchange(ref field, average, snapshot1);
                if (snapshot1 == snapshot2) return;   // No one preempted us.
                spinWait.SpinOnce();
            }
        }

        /// <summary>
        /// Res the set.
        /// </summary>
        private void ReSet()
        {
            Interlocked.Exchange(ref _lastLatency,0);
            Interlocked.Exchange(ref _messageProcessedCounter, 0);
            Interlocked.Exchange(ref _maxLatency, 0);
            Interlocked.Exchange(ref _minLatency, 0);
        }

        /// <summary>
        /// Configures the sampler.
        /// </summary>
        protected override void ConfigureSampler() {}


        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                MonitorableProcessor.MonitorableInputGateway.OnMessageBusReceived -= OnMessageBusReceived;
            }
        }

    }


}
