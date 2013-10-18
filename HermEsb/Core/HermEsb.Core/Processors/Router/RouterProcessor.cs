using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bteam.SimpleStateMachine;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.ErrorHandling;
using HermEsb.Core.Gateways;
using HermEsb.Core.Gateways.Router;
using HermEsb.Core.Logging;
using HermEsb.Core.Messages;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Processors.Router.Outputs;
using HermEsb.Core.Processors.Router.Subscriptors;
using HermEsb.Core.Serialization;
using HermEsb.Logging;

namespace HermEsb.Core.Processors.Router
{
    /// <summary>
    /// </summary>
    public sealed class RouterProcessor : IProcessor, ISubscriber, ILoggable, IMonitorableRouter, IRouterErrorHandling
    {
        private readonly Identification _identification;
        private readonly IInputGateway<byte[], RouterHeader> _inputGateway;
        private readonly IRouterOutputHelper _routerOutputHelper;
        private readonly IDataContractSerializer _serializer;

        private ILogger _logger;
        private IStateMachine<ProcessorStatus> _stateMachine;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RouterProcessor" /> class.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="inputGateway">The message receiver.</param>
        /// <param name="routerOutputHelper">The bus sender helper.</param>
        internal RouterProcessor(Identification identification, IInputGateway<byte[], RouterHeader> inputGateway,
                                 IRouterOutputHelper routerOutputHelper)
        {
            JoinedBusInfo = BusInfo.Create();

            ConfigureStateMachine();
            _inputGateway = inputGateway;
            _identification = identification;
            _routerOutputHelper = routerOutputHelper;
            _inputGateway.OnMessage += MessageReceived;
            _stateMachine.ChangeState(ProcessorStatus.Configured);
            _serializer = new JsonDataContractSerializer();
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
        ///     Gets the message types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubscriptionKey> GetMessageTypes()
        {
            return _routerOutputHelper.GetMessageTypes();
        }

        /// <summary>
        ///     Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public ProcessorStatus Status
        {
            get { return _stateMachine.CurrentState; }
        }


        /// <summary>
        ///     Gets or sets the joined bus info.
        /// </summary>
        /// <value>The joined bus info.</value>
        public IBusInfo JoinedBusInfo { get; set; }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public void Start()
        {
            Logger.Debug("Starting.. Router Processor");
            if (_stateMachine.CanChangeState(ProcessorStatus.Started))
            {
                _stateMachine.ChangeState(ProcessorStatus.Started);
                Logger.Debug("Router Processor Started");
            }
            else
            {
                Logger.Debug("Don't Change state to Start");
            }
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public void Stop()
        {
            if (_stateMachine.CanChangeState(ProcessorStatus.Stopped))
            {
                _stateMachine.ChangeState(ProcessorStatus.Stopped);
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
        ///     Gets the identification.
        /// </summary>
        /// <value>The identification.</value>
        public Identification Identification
        {
            get { return _identification; }
        }

        /// <summary>
        ///     Gets the end point.
        /// </summary>
        /// <value>The end point.</value>
        public IEndPoint ReceiverEndPoint
        {
            get { return _inputGateway.ReceiverEndPoint; }
        }


        /// <summary>
        ///     Gets the input gateway.
        /// </summary>
        /// <value>The input gateway.</value>
        public IMonitorableReceiverGateway MonitorableInputGateway
        {
            get { return _inputGateway; }
        }

        /// <summary>
        ///     Gets the monitorable sender gateway.
        /// </summary>
        /// <value>The monitorable sender gateway.</value>
        public IMonitorableSenderGateway MonitorableSenderGateway
        {
            get { return _routerOutputHelper; }
        }

        /// <summary>
        ///     Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public ProcessorStatus ProcessorStatus
        {
            get { return _stateMachine.CurrentState; }
        }

        /// <summary>
        ///     Occurs when [on message received].
        /// </summary>
        public event MonitorEventHandler OnMessageReceived;

        /// <summary>
        ///     Occurs when [on message sended].
        /// </summary>
        public event MonitorEventHandler OnMessageSent;

        /// <summary>
        ///     Occurs when [on created handler].
        /// </summary>
        public event HandlerMonitorEventHandler OnCreatedHandler;

        /// <summary>
        ///     Occurs when [on destoyed handler].
        /// </summary>
        public event HandlerMonitorEventHandler OnDestoyedHandler;

        /// <summary>
        ///     Occurs when [sender gateway changed].
        /// </summary>
        public event EventHandler SenderGatewayChanged;

        /// <summary>
        ///     Occurs when [on router error].
        /// </summary>
        public event ErrorOnRouterEventHandler OnRouterError;

        /// <summary>
        ///     Subscribes the specified message type.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="service"></param>
        /// <param name="outputGateway">The message sender.</param>
        public void Subscribe(SubscriptionKey messageType, Identification service, IOutputGateway<byte[]> outputGateway)
        {
            _routerOutputHelper.Subscribe(messageType, service, outputGateway);
        }

        /// <summary>
        ///     Unsubscribes the specified message type.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="service"></param>
        /// <param name="outputGateway">The message sender.</param>
        public void Unsubscribe(SubscriptionKey messageType, Identification service,
                                IOutputGateway<byte[]> outputGateway)
        {
            _routerOutputHelper.Unsubscribe(messageType, service, outputGateway);
        }

        /// <summary>
        ///     Configures the state machine.
        /// </summary>
        private void ConfigureStateMachine()
        {
            _stateMachine = StateMachineFactory.Create(ProcessorStatus.Initializing)
                                               .Permit(ProcessorStatus.Initializing, ProcessorStatus.Configured)
                                               .Permit(ProcessorStatus.Configured, ProcessorStatus.Started,
                                                       t => _inputGateway.Start())
                                               .Permit(ProcessorStatus.Started, ProcessorStatus.Stopped,
                                                       t => _inputGateway.Stop())
                                               .Permit(ProcessorStatus.Stopped, ProcessorStatus.Started,
                                                       t => _inputGateway.Start())
                                               .Permit(ProcessorStatus.Stopped, ProcessorStatus.Configured);
        }

        /// <summary>
        ///     Messages the received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        private void MessageReceived(object sender, OutputGatewayEventHandlerArgs<byte[], RouterHeader> args)
        {
            InvokeOnMessageReceived();
            try
            {
                if (!string.IsNullOrEmpty(args.Header.BodyType))
                {
                    if (args.Header.Type != MessageBusType.Reply)
                        _routerOutputHelper.Publish(args.Header.BodyType, args.Header.Priority, args.SerializedMessage);
                    else
                    {
                        _routerOutputHelper.Reply(args.Header.Identification, args.Header.Priority,
                                                  args.SerializedMessage);
                    }
                }
                else
                {
                    Logger.Error(string.Format("El mensaje no tiene bodytype: {0}", args.SerializedMessage));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Error publish bus: {0}", args.SerializedMessage), ex);
                var messageByte = new byte[args.Header.MessageLength];
                args.Message.CopyTo(messageByte, 15);
                var message = _serializer.Deserialize<MessageBus>(messageByte);
                OnOnRouterError(message, ex);
            }
            InvokeOnMessageSent(args.Message != null
                                    ? new MonitorEventArgs {MessageCreatedAt = args.Header.CreatedAt}
                                    : new MonitorEventArgs());
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
                _inputGateway.Dispose();
                _routerOutputHelper.Dispose();
            }
        }

        /// <summary>
        ///     Invokes the on message received.
        /// </summary>
        private void InvokeOnMessageReceived()
        {
            MonitorEventHandler handler = OnMessageReceived;
            if (handler != null) Task.Factory.StartNew(() => handler(this, new MonitorEventArgs()));
        }

        /// <summary>
        ///     Invokes the on message sended.
        /// </summary>
        /// <param name="monitorEventArgs"></param>
        private void InvokeOnMessageSent(MonitorEventArgs monitorEventArgs)
        {
            MonitorEventHandler handler = OnMessageSent;
            if (handler != null) Task.Factory.StartNew(() => handler(this, monitorEventArgs));
        }

        /// <summary>
        ///     Called when [on router error].
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        private void OnOnRouterError(MessageBus message, Exception exception)
        {
            ErrorOnRouterEventHandler handler = OnRouterError;
            if (handler != null)
                handler(this, new ErrorOnRouterEventHandlerArgs {Message = message, Exception = exception});
        }
    }
}