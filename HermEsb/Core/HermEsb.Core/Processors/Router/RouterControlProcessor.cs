using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Bteam.SimpleStateMachine;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.ErrorHandling;
using HermEsb.Core.Gateways;
using HermEsb.Core.Handlers;
using HermEsb.Core.Ioc;
using HermEsb.Core.Logging;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Builders;
using HermEsb.Core.Messages.Control;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Processors.Router.Subscriptors;
using HermEsb.Logging;
using ServiceStack;

namespace HermEsb.Core.Processors.Router
{
    /// <summary>
    /// </summary>
    public sealed class RouterControlProcessor : IIdentificable, IRouterController, ILoggable, IAgentErrorHandling
    {
        private readonly IHandlerRepository _handlerRepository;
        private readonly Identification _identification;
        private readonly IInputGateway<IControlMessage, MessageHeader> _inputGateway;
        private readonly IMessageBuilder _messageBuilder;
        private readonly ISubscriptorsHelper _subscriptonsHelper;
        private ILogger _logger;

        private IProcessor _processor;
        private IStateMachine<ProcessorStatus> _stateMachine;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RouterControlProcessor" /> class.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="inputGateway">The input gateway.</param>
        /// <param name="handlerRepository">The handler repository.</param>
        /// <param name="subscriptonsHelper">The subscriptors helper.</param>
        /// <param name="messageBuilder">The message builder.</param>
        internal RouterControlProcessor(Identification identification,
                                        IInputGateway<IControlMessage, MessageHeader> inputGateway,
                                        IHandlerRepository handlerRepository,
                                        ISubscriptorsHelper subscriptonsHelper,
                                        IMessageBuilder messageBuilder)
        {
            ConfigureStateMachine();
            _identification = identification;
            _inputGateway = inputGateway;
            _inputGateway.OnMessage += MessageReceived;
            _handlerRepository = handlerRepository;
            _subscriptonsHelper = subscriptonsHelper;
            _subscriptonsHelper.Controller = this;
            _messageBuilder = messageBuilder;
        }

        /// <summary>
        ///     Occurs when [on error handler].
        /// </summary>
        public event ErrorOnHandlersEventHandler OnErrorHandler;

        /// <summary>
        ///     Gets the identification.
        /// </summary>
        /// <value>The identification.</value>
        public Identification Identification
        {
            get { return _identification; }
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
        ///     Gets or sets the proccesor.
        /// </summary>
        /// <value>The proccesor.</value>
        public IProcessor Processor
        {
            get { return _processor; }
            set
            {
                _processor = value;
                _stateMachine.ChangeState(ProcessorStatus.Configured);
            }
        }

        /// <summary>
        ///     Gets or sets the monitor.
        /// </summary>
        /// <value>The monitor.</value>
        public IMonitor Monitor { get; set; }

        /// <summary>
        ///     Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public ProcessorStatus Status
        {
            get { return _stateMachine.CurrentState; }
        }

        /// <summary>
        ///     Gets the subscriptors.
        /// </summary>
        /// <value>The subscriptors.</value>
        public ISubscriptons Subscriptons
        {
            get { return _subscriptonsHelper; }
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public void Start()
        {
            if (_stateMachine.CanChangeState(ProcessorStatus.Started))
            {
                _stateMachine.ChangeState(ProcessorStatus.Started);
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
        ///     Gets the message builder.
        /// </summary>
        /// <value>The message builder.</value>
        public IMessageBuilder MessageBuilder
        {
            get { return _messageBuilder; }
        }


        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Publishes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Publish(IMessage message)
        {
            _subscriptonsHelper.Send((IControlMessage) message);
        }

        /// <summary>
        ///     Publishes the specified identification.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="message">The message.</param>
        public void Publish(Identification identification, IControlMessage message)
        {
            _subscriptonsHelper.Send(identification, message);
        }

        /// <summary>
        ///     Envia el mensaje a la cola para procesarlo mas tarde.
        /// </summary>
        public void ProcessLater()
        {
        }

        /// <summary>
        ///     Envia el mensaje a la cola para procesarlo mas tarde.
        /// </summary>
        /// <param name="miliseconds">The miliseconds.</param>
        public void ProcessLater(int miliseconds)
        {
            Thread.Sleep(miliseconds);
            ProcessLater();
        }

        /// <summary>
        ///     Envia el mensaje a la cola para procesarlo mas tarde.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        public void ProcessLater(TimeSpan timeSpan)
        {
            Thread.Sleep(timeSpan);
            ProcessLater();
        }

        /// <summary>
        ///     Replies the specified request.
        /// </summary>
        /// <param name="replyMessage">The reply message.</param>
        public void Reply(IMessage replyMessage)
        {
            Publish(replyMessage);
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
        ///     Configures the state machine.
        /// </summary>
        private void ConfigureStateMachine()
        {
            _stateMachine = StateMachineFactory.Create(ProcessorStatus.Initializing)
                                               .Permit(ProcessorStatus.Initializing, ProcessorStatus.Configured,
                                                       OnConfiguredControl)
                                               .Permit(ProcessorStatus.Configured, ProcessorStatus.Started,
                                                       OnStartControl)
                                               .Permit(ProcessorStatus.Started, ProcessorStatus.Stopped, OnStopControl)
                                               .Permit(ProcessorStatus.Stopped, ProcessorStatus.Started, OnStartControl)
                                               .Permit(ProcessorStatus.Stopped, ProcessorStatus.Configured);
        }

        /// <summary>
        ///     Called when [configured control].
        /// </summary>
        /// <param name="triggerars">The triggerars.</param>
        private void OnConfiguredControl(TriggerArs triggerars)
        {
            _subscriptonsHelper.LoadStoredSubscriptors(_identification);
        }

        /// <summary>
        ///     Called when [stop control].
        /// </summary>
        /// <param name="t">The t.</param>
        private void OnStopControl(TriggerArs t)
        {
            _inputGateway.Stop();
            if (Monitor != null)
            {
                Monitor.Stop();
            }
            InvokeOnStop();
        }

        /// <summary>
        ///     Called when [start control].
        /// </summary>
        /// <param name="t">The t.</param>
        private void OnStartControl(TriggerArs t)
        {
            _inputGateway.Purge();

            _inputGateway.Start();
            if (Monitor != null)
            {
                Monitor.Start();
            }

            InvokeOnStart();
        }

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
        ///     Messages the received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        private void MessageReceived(object sender, OutputGatewayEventHandlerArgs<IControlMessage, MessageHeader> args)
        {
            var listTask = new List<Task>();

            Logger.Debug("Control Message Received");

            //Buscar en los handlers para procesar el mensaje
            foreach (Type type in _handlerRepository.GetHandlersByMessage(args.Message.GetType()))
            {
                Type typeClosure = type;
                listTask.Add(Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            using (IMessageContext messageContext = ContextManager.Instance.CreateNewContext())
                            {
                                object messageHandler =
                                    messageContext.Resolve(typeClosure);

                                SetPropertyToHandler(messageHandler, "Controller",
                                                     this);
                                SetPropertyToHandler(messageHandler, "Processor",
                                                     Processor);

                                InvokeMethodHandle(messageHandler, args.Message);
                            }
                        }
                        catch (Exception ex)
                        {
                            var message = args.Message.ToJson();
                            Logger.Error(string.Format("Error Mensaje de Control: {0}", message), ex);
                            InvokeOnErrorHandler(message, args.Header, typeClosure, ex);
                        }
                    }));
            }

            Task.WaitAll(listTask.ToArray());
        }

        /// <summary>
        ///     Sets the property to handler.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        private static void SetPropertyToHandler(object obj, string propertyName, object value)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property != null)
            {
                property.SetValue(obj, value, null);
            }
        }

        /// <summary>
        ///     Invokes the method handle.
        /// </summary>
        /// <param name="messageHandler">The message handler.</param>
        /// <param name="message">The message.</param>
        private static void InvokeMethodHandle(object messageHandler, object message)
        {
            MethodInfo method = messageHandler.GetType().GetMethod("HandleMessage", new[] {message.GetType()});
            if (method != null)
            {
                method.Invoke(messageHandler, new[] {message});
            }
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
                _subscriptonsHelper.Dispose();
            }
        }

        /// <summary>
        ///     Invokes the on error handler.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="header">The header.</param>
        /// <param name="handlerType">Type of the handler.</param>
        /// <param name="exception">The exception.</param>
        private void InvokeOnErrorHandler(string message, MessageHeader header, Type handlerType, Exception exception)
        {
            ErrorOnHandlersEventHandler handler = OnErrorHandler;
            if (handler != null)
                handler(this, new ErrorOnHandlersEventHandlerArgs<string>
                    {
                        Message = message,
                        Header = header,
                        HandlerType = handlerType,
                        Exception = exception
                    });
        }
    }
}