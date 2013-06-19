using System;
using Bteam.SimpleStateMachine;
using HermEsb.Core.ErrorHandling;
using HermEsb.Core.Logging;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Router;
using HermEsb.Logging;

namespace HermEsb.Core.Service
{
    /// <summary>
    /// </summary>
    public class Service : IService, ILoggable
    {
        private readonly IController _controller;
        private readonly IErrorHandlingController _errorHandlingController;
        private readonly IProcessor _processor;
        private ILogger _logger;
        private IStateMachine<ServiceStatus> _stateMachine;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Service" /> class.
        /// </summary>
        /// <param name="processor">The processor.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="errorHandlingController">The error handling controller.</param>
        public Service(IProcessor processor, IController controller, IErrorHandlingController errorHandlingController)
        {
            _processor = processor;
            _controller = controller;
            _errorHandlingController = errorHandlingController;
            ConfigureErrorHandling();
            _controller.Processor = _processor;
            ConfigureStateMachine();
        }


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
        ///     Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public ServiceStatus Status
        {
            get { return _stateMachine.CurrentState; }
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public void Start()
        {
            if (_stateMachine.CanChangeState(ServiceStatus.StandBy))
            {
                _stateMachine.ChangeState(ServiceStatus.StandBy);
            }
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public void Stop()
        {
            if (_stateMachine.CanChangeState(ServiceStatus.Stopped))
            {
                _stateMachine.ChangeState(ServiceStatus.Stopped);
            }
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
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        ///     Occurs when [on stand by].
        /// </summary>
        public event EventHandler OnStandBy;

        private void ConfigureErrorHandling()
        {
            if (_processor is ISubscriber)
            {
                _errorHandlingController.AddRouterErrorHandling(_processor as IRouterErrorHandling);
            }
            else
            {
                _errorHandlingController.AddAgentErrorHandling(_processor as IAgentErrorHandling);
            }
            _errorHandlingController.AddAgentErrorHandling(_controller as IAgentErrorHandling);
        }

        /// <summary>
        ///     Configures the state machine.
        /// </summary>
        private void ConfigureStateMachine()
        {
            _stateMachine = StateMachineFactory.Create(ServiceStatus.Initializing)
                                               .Permit(ServiceStatus.Initializing, ServiceStatus.StandBy, StartControl)
                                               .Permit(ServiceStatus.StandBy, ServiceStatus.Started, StartProcessor)
                                               .Permit(ServiceStatus.StandBy, ServiceStatus.Stopped, StopControl)
                                               .Permit(ServiceStatus.Started, ServiceStatus.StandBy, StopProcessor)
                                               .Permit(ServiceStatus.Started, ServiceStatus.Stopped, StopAll)
                                               .Permit(ServiceStatus.Stopped, ServiceStatus.StandBy, StartControl);

            _controller.OnStart += (sender, args) =>
                {
                    //El processor es un bus no un servicio, los servicios arrancan cuando el controller acaba de configurarlos
                    if (_processor is ISubscriber)
                    {
                        if (_stateMachine.CanChangeState(ServiceStatus.Started))
                        {
                            _stateMachine.ChangeState(ServiceStatus.Started);
                        }
                    }
                };

            _processor.OnStart += (sender, args) =>
                {
                    if (_stateMachine.CanChangeState(ServiceStatus.Started))
                    {
                        _stateMachine.ChangeState(ServiceStatus.Started);
                    }
                };

            _processor.OnStop += (sender, args) =>
                {
                    if (_stateMachine.CanChangeState(ServiceStatus.StandBy))
                    {
                        _stateMachine.ChangeState(ServiceStatus.StandBy);
                    }
                };
        }

        /// <summary>
        ///     Called when [stop processor].
        /// </summary>
        /// <param name="triggerars">The triggerars.</param>
        private void StopProcessor(TriggerArs triggerars)
        {
            Logger.Debug("Stop Router Processor");
            _processor.Stop();
            InvokeOnStandBy();
        }

        /// <summary>
        ///     Called when [start processor].
        /// </summary>
        /// <param name="triggerars">The triggerars.</param>
        private void StartProcessor(TriggerArs triggerars)
        {
            Logger.Debug("Starting Router Processor");
            _processor.Start();
            InvokeOnStart();
        }

        /// <summary>
        ///     Called when [stop control].
        /// </summary>
        /// <param name="triggerars">The triggerars.</param>
        private void StopControl(TriggerArs triggerars)
        {
            Logger.Debug("Stopping Control Processor");
            _controller.Stop();
            InvokeOnStop();
        }

        /// <summary>
        ///     Called when [start].
        /// </summary>
        /// <param name="triggerars">The triggerars.</param>
        private void StartControl(TriggerArs triggerars)
        {
            Logger.Debug("Starting Control Processor");
            _controller.Start();
        }

        /// <summary>
        ///     Called when [stop].
        /// </summary>
        /// <param name="triggerars">The triggerars.</param>
        private void StopAll(TriggerArs triggerars)
        {
            Logger.Debug("Stoping Router Processor");
            _processor.Stop();

            Logger.Debug("Stoping Control Processor");
            _controller.Stop();
            InvokeOnStop();
        }


        private void InvokeOnStart()
        {
            EventHandler handler = OnStart;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void InvokeOnStop()
        {
            EventHandler handler = OnStop;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _errorHandlingController.Dispose();
                _processor.Dispose();
                _controller.Dispose();
            }
        }

        /// <summary>
        ///     Invokes the on stand by.
        /// </summary>
        private void InvokeOnStandBy()
        {
            EventHandler handler = OnStandBy;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}