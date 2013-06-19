using System;
using Bteam.SimpleStateMachine;
using HermEsb.Core.Listeners;
using HermEsb.Core.Messages;
using HermEsb.Core.Service;

namespace HermEsb.Core.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public class MonitorListener : IService
    {
        private readonly Listener<IMessage> _listener;
        private IStateMachine<ServiceStatus> _stateMachine;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorListener"/> class.
        /// </summary>
        /// <param name="listener">The listener.</param>
        public MonitorListener(Listener<IMessage> listener)
        {
            _listener = listener;
            ConfigureStateMachine();
        }

        private void ConfigureStateMachine()
        {
            _stateMachine = StateMachineFactory.Create(ServiceStatus.Initializing)
                .Permit(ServiceStatus.Initializing, ServiceStatus.Started, ListenerStart)
                .Permit(ServiceStatus.Started, ServiceStatus.Stopped, ListenerStop)
                .Permit(ServiceStatus.Stopped, ServiceStatus.Started, ListenerStart);
        }

        /// <summary>
        /// Listeners the stop.
        /// </summary>
        /// <param name="triggerars">The triggerars.</param>
        private void ListenerStop(TriggerArs triggerars)
        {
            _listener.Stop();
            InvokeOnStop();
        }

        /// <summary>
        /// Listeners the start.
        /// </summary>
        /// <param name="triggerars">The triggerars.</param>
        private void ListenerStart(TriggerArs triggerars)
        {
            _listener.Start();
            InvokeOnStart();
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public ServiceStatus Status
        {
            get { return _stateMachine.CurrentState; }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            _stateMachine.TryChangeState(ServiceStatus.Started);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            _stateMachine.TryChangeState(ServiceStatus.Stopped);
        }

        /// <summary>
        /// Occurs when [on start].
        /// </summary>
        public event EventHandler OnStart;

        /// <summary>
        /// Invokes the on start.
        /// </summary>
        private void InvokeOnStart()
        {
            EventHandler handler = OnStart;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when [on stop].
        /// </summary>
        public event EventHandler OnStop;

        /// <summary>
        /// Invokes the on stop.
        /// </summary>
        public void InvokeOnStop()
        {
            EventHandler handler = OnStop;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _listener.Dispose();
            }
        }

        /// <summary>
        /// Occurs when [on stand by].
        /// </summary>
        public event EventHandler OnStandBy;
    }
}