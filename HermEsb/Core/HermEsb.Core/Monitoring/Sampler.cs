using System;
using System.Threading;
using System.Threading.Tasks;
using Bteam.SimpleStateMachine;
using HermEsb.Logging;

namespace HermEsb.Core.Monitoring
{
    /// <summary>
    /// </summary>
    public abstract class Sampler : ISampler
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly IMonitorableProcessor _monitorableProcessor;
        private ILogger _logger;
        private IStateMachine<MonitorStatus> _stateMachine;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Sampler" /> class.
        /// </summary>
        /// <param name="monitorableProcessor">The monitorable processor.</param>
        protected Sampler(IMonitorableProcessor monitorableProcessor)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _monitorableProcessor = monitorableProcessor;
            ConfigureStateMachine();
            ConfigureSampler();
        }

        /// <summary>
        ///     Gets or sets the frequence.
        /// </summary>
        /// <value>The frequence.</value>
        public int Frequence { get; private set; }

        /// <summary>
        ///     Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger
        {
            get { return _logger ?? new NullLogger(); }
            set { _logger = value; }
        }

        /// <summary>
        ///     Gets the monitorable processor.
        /// </summary>
        /// <value>The monitorable processor.</value>
        protected IMonitorableProcessor MonitorableProcessor
        {
            get { return _monitorableProcessor; }
        }

        /// <summary>
        ///     Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public MonitorStatus Status
        {
            get { return _stateMachine.CurrentState; }
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public void Start()
        {
            _stateMachine.TryChangeState(MonitorStatus.Started);
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public void Stop()
        {
            _stateMachine.TryChangeState(MonitorStatus.Stopped);
        }

        /// <summary>
        ///     Occurs when [on start].
        /// </summary>
        public event EventHandler OnStart;

        /// <summary>
        ///     Occurs when [on stop].
        /// </summary>
        public event EventHandler OnStop;

        /// <summary>
        ///     Gets the bus.
        /// </summary>
        /// <value>The bus.</value>
        public IMonitoringSender MonitoringSender { get; set; }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _cancellationTokenSource.Dispose();
            Dispose(true);
        }

        /// <summary>
        ///     Sets the frequence.
        /// </summary>
        /// <param name="frequence">The frequence in secs.</param>
        public void SetFrequence(int frequence)
        {
            Frequence = frequence;
        }

        /// <summary>
        ///     Configures the state machine.
        /// </summary>
        private void ConfigureStateMachine()
        {
            _stateMachine = StateMachineFactory.Create(MonitorStatus.Initializing);
            _stateMachine.Permit(MonitorStatus.Initializing, MonitorStatus.Started, OnStartSampler)
                         .Permit(MonitorStatus.Started, MonitorStatus.Stopped, e =>
                             {
                                 _cancellationTokenSource.Cancel();
                                 InvokeOnStop();
                             })
                         .Permit(MonitorStatus.Stopped, MonitorStatus.Started, OnStartSampler);
        }

        /// <summary>
        ///     Samplers the task.
        /// </summary>
        protected abstract void SamplerTask();

        /// <summary>
        ///     Configures the sampler.
        /// </summary>
        protected abstract void ConfigureSampler();

        /// <summary>
        ///     Invokes the on start.
        /// </summary>
        private void InvokeOnStart()
        {
            EventHandler handler = OnStart;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Invokes the on stop.
        /// </summary>
        private void InvokeOnStop()
        {
            EventHandler handler = OnStop;
            if (handler != null) handler(this, EventArgs.Empty);
        }


        /// <summary>
        ///     Called when [start sampler].
        /// </summary>
        /// <param name="e">The e.</param>
        private void OnStartSampler(TriggerArs e)
        {
            InvokeOnStart();
            Task.Factory.StartNew(() =>
                {
                    while (_stateMachine.CurrentState == MonitorStatus.Started)
                    {
                        try
                        {
                            SamplerTask();
                        }
                        catch (Exception exception)
                        {
                            Logger.Error("Error Sampler Task", exception);
                            Stop();
                        }

                        Thread.Sleep(Frequence*1000);
                    }
                }, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected abstract void Dispose(bool disposing);
    }
}