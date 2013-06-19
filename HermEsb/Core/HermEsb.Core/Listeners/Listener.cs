using Bteam.SimpleStateMachine;
using HermEsb.Core.Communication;
using HermEsb.Core.Gateways;
using HermEsb.Core.Handlers;
using HermEsb.Core.Ioc;
using HermEsb.Core.Logging;
using HermEsb.Core.Messages;
using HermEsb.Logging;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermEsb.Core.Listeners
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public class Listener<TMessage> : IStartable<ListenerStatus>, IDisposable, ILoggable, IListener where TMessage : IMessage
    {
        private readonly IInputGateway<TMessage> _inputGateway;
        private readonly IHandlerRepository _handlerRepository;
        private IStateMachine<ListenerStatus> _stateMachine;
        private ILogger _logger;

        internal Listener(IInputGateway<TMessage> inputGateway,
                            IHandlerRepository handlerRepository)
        {
            _inputGateway = inputGateway;
            _inputGateway.OnMessage += MessageReceived;
            _handlerRepository = handlerRepository;
            ConfigureStateMachine();
        }

        private void ConfigureStateMachine()
        {
            _stateMachine = StateMachineFactory.Create(ListenerStatus.Initializing)
                .Permit(ListenerStatus.Initializing, ListenerStatus.Started, InputGatewayStart)
                .Permit(ListenerStatus.Started, ListenerStatus.Stopped, InputGatewayStop)
                .Permit(ListenerStatus.Stopped, ListenerStatus.Started, InputGatewayStart);

        }

        /// <summary>
        /// Inputs the gateway stop.
        /// </summary>
        /// <param name="triggerars">The triggerars.</param>
        private void InputGatewayStop(TriggerArs triggerars)
        {
            _inputGateway.Stop();
            InvokeOnStop();
        }

        /// <summary>
        /// Inputs the gateway start.
        /// </summary>
        /// <param name="triggerars">The triggerars.</param>
        private void InputGatewayStart(TriggerArs triggerars)
        {
            _inputGateway.Start();
            InvokeOnStart();
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public ListenerStatus Status
        {
            get { return _stateMachine.CurrentState; }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            _stateMachine.TryChangeState(ListenerStatus.Started);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            _stateMachine.TryChangeState(ListenerStatus.Stopped);
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
                _inputGateway.Dispose();
            }
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger
        {
            get { return _logger ?? new NullLogger(); }
            set { _logger = value; }
        }

        private void MessageReceived(object sender, OutputGatewayEventHandlerArgs<TMessage> args)
        {
            var listTask = new List<Task>();

            //Buscar en los handlers para procesar el mensaje
            foreach (var type in _handlerRepository.GetHandlersByMessage(args.Message.GetType()))
            {
                var typeClosure = type;

                listTask.Add(Task.Factory.StartNew(() =>
                {
                    try
                    {
                        using (var messageContext = ContextManager.Instance.CreateNewContext())
                        {
                            InitializeContext(messageContext, args);

                            using (var messageHandler = (IDisposable)messageContext.Resolve(typeClosure))
                            {
                                InitializeMessageHandler(messageHandler);
                                try
                                {
                                    InvokeMethodHandle(messageHandler, args.Message);
                                }
                                catch (Exception exception)
                                {
                                    Logger.Fatal("Error On Handler", exception);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Error Message Received", ex);
                    }
                }));
            }

            Task.WaitAll(listTask.ToArray());
        }

        /// <summary>
        /// Initializes the message context.
        /// </summary>
        /// <param name="messageContext">The message context.</param>
        /// <param name="args">The args.</param>
        private static void InitializeContext(IMessageContext messageContext,
                                                OutputGatewayEventHandlerArgs<TMessage> args)
        {
            messageContext.MessageInfo.Body = args.Message;
            messageContext.MessageInfo.Header = args.Header;
        }

        /// <summary>
        /// Envia el mensaje a la cola para procesarlo mas tarde.
        /// </summary>
        public void ProcessLater()
        {
            lock (ContextManager.Instance.CurrentContext.MessageInfo)
            {
                if (!ContextManager.Instance.CurrentContext.MessageInfo.IsReinjected)
                {
                    _inputGateway.Reinject(ContextManager.Instance.CurrentContext.MessageInfo.Body,
                                            ContextManager.Instance.CurrentContext.MessageInfo.Header);
                    ContextManager.Instance.CurrentContext.MessageInfo.IsReinjected = true;
                }
            }
        }


        /// <summary>
        /// Envia el mensaje a la cola para procesarlo mas tarde.
        /// </summary>
        /// <param name="miliseconds">The miliseconds.</param>
        public void ProcessLater(int miliseconds)
        {
            Thread.Sleep(miliseconds);
            ProcessLater();
        }

        /// <summary>
        /// Envia el mensaje a la cola para procesarlo mas tarde.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        public void ProcessLater(TimeSpan timeSpan)
        {
            Thread.Sleep(timeSpan);
            ProcessLater();
        }


        /// <summary>
        /// Initializes the message handler.
        /// </summary>
        /// <param name="messageHandler">The message handler.</param>
        protected void InitializeMessageHandler(object messageHandler)
        {
            SetPropertyToHandler(messageHandler, "Listener", this);
        }

        /// <summary>
        /// Sets the property to handler.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        protected static void SetPropertyToHandler(object obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property != null)
            {
                property.SetValue(obj, value, null);
            }
        }

        /// <summary>
        /// Invokes the method handle.
        /// </summary>
        /// <param name="messageHandler">The message handler.</param>
        /// <param name="message">The message.</param>
        private static void InvokeMethodHandle(object messageHandler, object message)
        {

            var method = messageHandler.GetType().GetMethod("HandleMessage", new[] { message.GetType() });
            if (method != null)
            {
                method.Invoke(messageHandler, new[] { message });
            }
        }
    }
}