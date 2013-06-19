using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Bteam.SimpleStateMachine;
using HermEsb.Core.Communication;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.ErrorHandling;
using HermEsb.Core.Gateways;
using HermEsb.Core.Handlers;
using HermEsb.Core.Handlers.Context;
using HermEsb.Core.Ioc;
using HermEsb.Core.Logging;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Builders;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Processors.Agent.Reinjection;
using HermEsb.Logging;

namespace HermEsb.Core.Processors.Agent
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public abstract class Agent<TMessage> : IStartable<ProcessorStatus>, IBus, ILoggable, IMonitorableProcessor,
                                            IAgentErrorHandling
        where TMessage : IMessage
    {
        private readonly IHandlerRepository _handlerRepository;
        private readonly Identification _identification;
        private readonly IInputGateway<TMessage> _inputGateway;
        private readonly IMessageBuilder _messageBuilder;
        private readonly IReinjectionEngine _reinjectionEngine;
        private ILogger _logger;
        private IOutputGateway<TMessage> _outputGateway;
        private IStateMachine<ProcessorStatus> _stateMachine;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Agent{TMessage}" /> class.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="inputGateway">The input gateway.</param>
        /// <param name="handlerRepository">The handler repository.</param>
        /// <param name="messageBuilder">The message builder.</param>
        /// <param name="reinjectionEngine">The reinjection engine.</param>
        protected Agent(Identification identification,
                        IInputGateway<TMessage> inputGateway,
                        IHandlerRepository handlerRepository, IMessageBuilder messageBuilder,
                        IReinjectionEngine reinjectionEngine)
        {
            JoinedBusInfo = BusInfo.Create();

            ConfigureStateMachine();

            _identification = identification;
            _messageBuilder = messageBuilder;
            _reinjectionEngine = reinjectionEngine;
            _inputGateway = inputGateway;
            _inputGateway.OnMessage += MessageReceived;
            _handlerRepository = handlerRepository;
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
        ///     Gets the input gateway.
        /// </summary>
        /// <value>The input gateway.</value>
        internal IInputGateway<TMessage> InputGateway
        {
            get { return _inputGateway; }
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
        ///     Gets the output gateway.
        /// </summary>
        /// <value>The output gateway.</value>
        protected IOutputGateway<TMessage> OutputGateway
        {
            get { return _outputGateway; }
            set
            {
                _outputGateway = value;
                _stateMachine.ChangeState(ProcessorStatus.Configured);
            }
        }

        /// <summary>
        ///     Gets the handler repository.
        /// </summary>
        /// <value>The handler repository.</value>
        protected IHandlerRepository HandlerRepository
        {
            get { return _handlerRepository; }
        }

        protected IStateMachine<ProcessorStatus> StateMachine
        {
            get { return _stateMachine; }
            set { _stateMachine = value; }
        }

        /// <summary>
        ///     Occurs when [on error handler].
        /// </summary>
        public event ErrorOnHandlersEventHandler OnErrorHandler;

        /// <summary>
        ///     Gets the message builder.
        /// </summary>
        /// <value>The message builder.</value>
        public IMessageBuilder MessageBuilder
        {
            get { return _messageBuilder; }
        }

        /// <summary>
        ///     Publishes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public virtual void Publish(IMessage message)
        {
            if (OutputGateway != null)
            {
                if (ContextManager.Instance.HasCurrentContext())
                {
                    Logger.Warn(string.Format("Enviando Mensaje: {0}", message.GetType().FullName));
                    OutputGateway.Send((TMessage) message, ContextManager.Instance.CurrentContext.MessageInfo);
                }
                else
                {
                    OutputGateway.Send((TMessage) message);
                }

                InvokeOnMessageSent();
            }
        }

        /// <summary>
        ///     Envia el mensaje a la cola para procesarlo mas tarde.
        /// </summary>
        public void ProcessLater()
        {
            ProcessLater(TimeSpan.FromMilliseconds(0));
        }

        /// <summary>
        ///     Envia el mensaje a la cola para procesarlo mas tarde.
        /// </summary>
        /// <param name="miliseconds">The miliseconds.</param>
        public void ProcessLater(int miliseconds)
        {
            ProcessLater(TimeSpan.FromMilliseconds(miliseconds));
        }

        /// <summary>
        ///     Envia el mensaje a la cola para procesarlo mas tarde.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        public void ProcessLater(TimeSpan timeSpan)
        {
            lock (ContextManager.Instance.CurrentContext.MessageInfo)
            {
                if (!ContextManager.Instance.CurrentContext.MessageInfo.IsReinjected)
                {
                    _reinjectionEngine.Reinject(ContextManager.Instance.CurrentContext.MessageInfo,
                                                TimeSpan.FromSeconds(0));
                    ContextManager.Instance.CurrentContext.MessageInfo.IsReinjected = true;
                }
            }
        }

        /// <summary>
        ///     Replies the specified request.
        /// </summary>
        /// <param name="replyMessage">The reply message.</param>
        public void Reply(IMessage replyMessage)
        {
            ContextManager.Instance.CurrentContext.MessageInfo.IsReply = true;
            Publish(replyMessage);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        ///     Gets or sets the joined bus info.
        /// </summary>
        /// <value>The joined bus info.</value>
        public IBusInfo JoinedBusInfo { get; set; }

        /// <summary>
        ///     Gets the identification.
        /// </summary>
        /// <value>The identification.</value>
        public Identification Identification
        {
            get { return _identification; }
        }

        /// <summary>
        ///     Occurs when [sender gateway changed].
        /// </summary>
        public event EventHandler SenderGatewayChanged;

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
            get { return OutputGateway; }
        }

        /// <summary>
        ///     Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public ProcessorStatus ProcessorStatus
        {
            get { return Status; }
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
        ///     Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public ProcessorStatus Status
        {
            get { return StateMachine.CurrentState; }
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public virtual void Start()
        {
            if (StateMachine.CanChangeState(ProcessorStatus.Started))
            {
                StateMachine.ChangeState(ProcessorStatus.Started);
            }
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public virtual void Stop()
        {
            if (StateMachine.CanChangeState(ProcessorStatus.Stopped))
            {
                StateMachine.ChangeState(ProcessorStatus.Stopped);
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
        ///     Configures the state machine.
        /// </summary>
        private void ConfigureStateMachine()
        {
            StateMachine = StateMachineFactory.Create(ProcessorStatus.Initializing)
                                              .Permit(ProcessorStatus.Initializing, ProcessorStatus.Configured,
                                                      OnConfigured)
                                              .Permit(ProcessorStatus.Configured, ProcessorStatus.Started,
                                                      InputGatewayStart)
                                              .Permit(ProcessorStatus.Started, ProcessorStatus.Stopped, InputGatewayStop)
                                              .Permit(ProcessorStatus.Stopped, ProcessorStatus.Started,
                                                      InputGatewayStart)
                                              .Permit(ProcessorStatus.Stopped, ProcessorStatus.Configured);
        }

        /// <summary>
        ///     Inputs the gateway stop.
        /// </summary>
        /// <param name="t">The t.</param>
        private void InputGatewayStop(TriggerArs t)
        {
            _inputGateway.Stop();
            InvokeOnStop();
        }

        /// <summary>
        ///     Inputs the gateway start.
        /// </summary>
        /// <param name="t">The t.</param>
        private void InputGatewayStart(TriggerArs t)
        {
            _inputGateway.Start();
            InvokeOnStart();
        }

        /// <summary>
        ///     Called when [configured].
        /// </summary>
        /// <param name="triggerars">The triggerars.</param>
        protected virtual void OnConfigured(TriggerArs triggerars)
        {
            InvokeSenderGatewayChanged();
        }

        /// <summary>
        ///     Messages the received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        protected virtual void MessageReceived(object sender, OutputGatewayEventHandlerArgs<TMessage> args)
        {
            InvokeOnMessageReceived();

            if (args.Header.CallContext != null && args.Header.CallContext.ContainsKey("DEBUG_MESSAGE"))
            {
                Logger.Warn(string.Format("Ha llegado el Mensaje:{0}", args.SerializedMessage));
                Logger.Warn(string.Format("Tipo del Mensaje:{0}", args.Header.BodyType));
            }

            Session currentSession = SessionFactory.Create();
            if (args.Header.Type == MessageBusType.Reply)
            {
                CallerContext requester = args.Header.CallStack.Pop();
                if (requester.Identification.Equals(Identification))
                {
                    currentSession = requester.Session;
                }
                else
                {
                    Logger.Debug("Reply Message not mine");
                    return;
                }
            }

            Logger.Warn(string.Format("Tipo del Mensaje:{0}", args.Header.BodyType));

            var listTask = new List<Task>();

            //Buscar en los handlers para procesar el mensaje
            foreach (Type type in HandlerRepository.GetHandlersByMessage(args.Message.GetType()))
            {
                Type typeClosure = type;

                listTask.Add(Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            using (IMessageContext messageContext = ContextManager.Instance.CreateNewContext())
                            {
                                Logger.Warn(string.Format("Se abre el handler {0}: ", typeClosure.FullName));
                                var messageSession = (Session) currentSession.Clone();

                                object messageHandler =
                                    messageContext.Resolve(typeClosure);

                                InvokeOnCreatedHandler();

                                InitializeContext(messageContext, args, messageSession);

                                IContextHandler contextHandler = ContextHandlerFactory.Create(messageSession,
                                                                                              ContextManager.Instance
                                                                                                            .CurrentContext
                                                                                                            .MessageInfo
                                                                                                            .CurrentCallContext);

                                InitializeMessageHandler(messageHandler, contextHandler);

                                try
                                {
                                    InvokeMethodHandle(messageHandler, args.Message);
                                }
                                catch (Exception exception)
                                {
                                    Logger.Fatal("Error On Handler", exception);
                                    Logger.Fatal(string.Format("Message Error: {0}", args.SerializedMessage), exception);
                                    InvokeOnErrorHandler(args.Header, args.SerializedMessage, exception, typeClosure);
                                }
                            }

                            InvokeOnDestoyedHandler();
                        }
                        catch (Exception exception)
                        {
                            Logger.Fatal("Error On Task", exception);
                            Logger.Fatal(string.Format("Message Error: {0}", args.SerializedMessage), exception);
                            InvokeOnErrorHandler(args.Header, args.SerializedMessage, exception, typeClosure);
                            //TODO: Poner tramiento de errores sobre la parallel
                        }
                    }));
            }

            Task.WaitAll(listTask.ToArray());
        }

        /// <summary>
        ///     Initializes the message context.
        /// </summary>
        /// <param name="messageContext">The message context.</param>
        /// <param name="args">The args.</param>
        /// <param name="currentSession">The current session.</param>
        private static void InitializeContext(IMessageContext messageContext,
                                              OutputGatewayEventHandlerArgs<TMessage> args,
                                              Session currentSession)
        {
            messageContext.MessageInfo.Body = args.Message;
            messageContext.MessageInfo.Header = (MessageHeader) args.Header.Clone();
            messageContext.MessageInfo.CurrentSession = currentSession;
            messageContext.MessageInfo.CurrentCallContext = args.Header.CallContext != null
                                                                ? (Session) args.Header.CallContext.Clone()
                                                                : SessionFactory.Create();
        }

        /// <summary>
        ///     Sets the property to handler.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        protected static void SetPropertyToHandler(object obj, string propertyName, object value)
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
        protected static void InvokeMethodHandle(object messageHandler, object message)
        {
            MethodInfo method = messageHandler.GetType().GetMethod("HandleMessage", new[] {message.GetType()});
            if (method != null)
            {
                try
                {
                    method.Invoke(messageHandler, new[] {message});
                }
                finally
                {
                    var disposable = messageHandler as IDisposable;
                    if (disposable != null) disposable.Dispose();
                }
            }
        }


        /// <summary>
        ///     Initializes the message handler.
        /// </summary>
        /// <param name="messageHandler">The message handler.</param>
        /// <param name="contextHandler">The context handler.</param>
        protected abstract void InitializeMessageHandler(object messageHandler, IContextHandler contextHandler);

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
        ///     Invokes the sender gateway changed.
        /// </summary>
        protected void InvokeSenderGatewayChanged()
        {
            EventHandler handler = SenderGatewayChanged;
            if (handler != null) handler(this, new EventArgs());
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
                if (OutputGateway != null) OutputGateway.Dispose();
            }
        }

        /// <summary>
        ///     Invokes the on message received.
        /// </summary>
        private void InvokeOnMessageReceived()
        {
            MonitorEventHandler handler = OnMessageReceived;
            if (handler != null)
            {
                Task.Factory.StartNew(() => handler(this, new MonitorEventArgs()));
            }
        }

        /// <summary>
        ///     Invokes the on message sended.
        /// </summary>
        private void InvokeOnMessageSent()
        {
            MonitorEventHandler handler = OnMessageSent;
            if (handler != null) Task.Factory.StartNew(() => handler(this, new MonitorEventArgs()));
        }

        /// <summary>
        ///     Invokes the on created handler.
        /// </summary>
        private void InvokeOnCreatedHandler()
        {
            HandlerMonitorEventHandler handler = OnCreatedHandler;
            if (handler != null) Task.Factory.StartNew(() => handler(this, new HandlerMonitorEventArgs()));
        }

        /// <summary>
        ///     Invokes the on destoyed handler.
        /// </summary>
        private void InvokeOnDestoyedHandler()
        {
            HandlerMonitorEventHandler handler = OnDestoyedHandler;
            if (handler != null) Task.Factory.StartNew(() => handler(this, new HandlerMonitorEventArgs()));
        }

        /// <summary>
        ///     Called when [on error handler].
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="message">The message bus.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="handlerType">Type of the handler.</param>
        private void InvokeOnErrorHandler(MessageHeader header, string message, Exception exception, Type handlerType)
        {
            ErrorOnHandlersEventHandler handler = OnErrorHandler;
            if (handler != null)
                Task.Factory.StartNew(() => handler(this, new ErrorOnHandlersEventHandlerArgs<string>
                    {
                        Header = header,
                        Message = message,
                        Exception = exception,
                        HandlerType = handlerType
                    }));
        }
    }
}