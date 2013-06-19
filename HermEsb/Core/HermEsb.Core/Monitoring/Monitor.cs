using System;
using System.Collections.Generic;
using Bteam.SimpleStateMachine;
using System.Linq;
using System.Threading.Tasks;
using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Monitoring;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Agent;

namespace HermEsb.Core.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public class Monitor : IMonitor
    {
        private IStateMachine<MonitorStatus> _stateMachine;
        private readonly IController _controller;
        private readonly IOutputGateway<IMessage> _outputGateway;
        private readonly IList<ISampler> _samplers;

        /// <summary>
        /// Initializes a new instance of the <see cref="Monitor"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="outputGateway">The output gateway.</param>
        public Monitor(IController controller, IOutputGateway<IMessage> outputGateway)
        {
            _samplers = new List<ISampler>();
            _controller = controller;
            _outputGateway = outputGateway;
            ConfigureStateMachine();
        }

        /// <summary>
        /// Configures the state machine.
        /// </summary>
        private void ConfigureStateMachine()
        {
            _stateMachine = StateMachineFactory.Create(MonitorStatus.Initializing);
            _stateMachine.Permit(MonitorStatus.Initializing, MonitorStatus.Started, StartMonitoring);
            _stateMachine.Permit(MonitorStatus.Started, MonitorStatus.Stopped, StopMonitoring);
            _stateMachine.Permit(MonitorStatus.Stopped, MonitorStatus.Started, StartMonitoring);
        }

        /// <summary>
        /// Stops the monitoring.
        /// </summary>
        /// <param name="triggerars">The triggerars.</param>
        private void StopMonitoring(TriggerArs triggerars)
        {
            foreach (var sampler in _samplers)
            {
                sampler.Stop();
            }
            InvokeOnStop();
        }

        /// <summary>
        /// Starts the monitoring.
        /// </summary>
        /// <param name="triggerars">The triggerars.</param>
        private void StartMonitoring(TriggerArs triggerars)
        {
            SendQueuesInfo();
            Parallel.ForEach(_samplers, sampler => sampler.Start());
            InvokeOnStart();
        }

        /// <summary>
        /// Sends the queues info.
        /// </summary>
        private void SendQueuesInfo()
        {
            var message = MonitoringMessageFactory.Create<InputQueueMessage>(_controller.Processor);
            message.InputControlQueue = _controller.ReceiverEndPoint.Uri.OriginalString;
            message.InputControlQueueTransport = _controller.ReceiverEndPoint.Transport;
            message.InputProcessorQueue = _controller.Processor.ReceiverEndPoint.Uri.OriginalString;
            message.InputProcessorQueueTransport = _controller.Processor.ReceiverEndPoint.Transport;

            Send(message);

            if (_controller.Processor is ServiceProcessor)
            {
                var messageInputQueues = MonitoringMessageFactory.Create<MessageTypesMessage>(_controller.Processor);
                var messageTypes = (_controller.Processor as ServiceProcessor).GetMessageTypes();
                messageInputQueues.MessageTypes = messageTypes.AsParallel().Select(
                    x => new MessageType { FullName = x.FullName }).ToList();

                Send(messageInputQueues);
            }
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
        private void InvokeOnStop()
        {
            EventHandler handler = OnStop;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public MonitorStatus Status
        {
            get { return _stateMachine.CurrentState; }
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <value>The controller.</value>
        public IController Controller { get { return _controller; } }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            _stateMachine.TryChangeState(MonitorStatus.Started);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            _stateMachine.TryChangeState(MonitorStatus.Stopped);
        }

        /// <summary>
        /// Adds the sampler.
        /// </summary>
        /// <param name="sampler">The sampler.</param>
        public void AddSampler(ISampler sampler)
        {
            _samplers.Add(sampler);
            sampler.MonitoringSender = this;
            if (_stateMachine.CurrentState  == MonitorStatus.Started)
            {
                sampler.Start();
            }
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(IMonitoringMessage message)
        {
            _outputGateway.Send(message);
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
                foreach (var sampler in _samplers)
                {
                    sampler.Dispose();
                }
            }
        }
    }
}