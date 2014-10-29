using Bteam.SimpleStateMachine;
using System;
using System.Threading;
using HermEsb.Core.Communication.EndPoints;

namespace HermEsb.Core.Communication.Channels
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractReceiverChannel : AbstractSenderChannel, IReceiverChannel
    {
        private IStateMachine<EndPointStatus> _endPointStateMachine;
        private Thread _extractTask;
        private readonly Semaphore _semaphore;
        private readonly CountdownEvent _countdown;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractReceiverChannel"/> class.
        /// </summary>
        /// <param name="numberOfParallelTasks">The number of parallel tasks.</param>
        internal AbstractReceiverChannel(int numberOfParallelTasks)
        {
            ConfigureStateMachine();
            _semaphore = new Semaphore(numberOfParallelTasks, numberOfParallelTasks);
            _countdown = new CountdownEvent(1);

            // Retrieve the number of tasks from configuration
            // Create an array of MessageBus returning tasks

            OnReceivedCompleted += (sender, args) => ReceivedCompleted(args.Message);
        }


        /// <summary>
        /// Gets or sets a value indicating whether [reading queue].
        /// </summary>
        /// <value><c>true</c> if [reading queue]; otherwise, <c>false</c>.</value>
        protected bool ReadingQueue { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public EndPointStatus Status
        {
            get
            {
                return _endPointStateMachine.CurrentState;
            }
        }

        /// <summary>
        /// Configures the state machine.
        /// </summary>
        private void ConfigureStateMachine()
        {
            _endPointStateMachine = StateMachineFactory.Create(EndPointStatus.Initializing)
                .Permit(EndPointStatus.Initializing, EndPointStatus.Receiving, t =>
                                                                                   {
                                                                                       InitializeEndPoint();
                                                                                       ReceivingStart();
                                                                                   })
                .Permit(EndPointStatus.Receiving, EndPointStatus.Stopped, OnReceivingStop)
                .Permit(EndPointStatus.Stopped, EndPointStatus.Receiving, t => ReceivingStart());
        }

        /// <summary>
        /// Initializes the end point.
        /// </summary>
        protected abstract void InitializeEndPoint();

        /// <summary>
        /// Called when [receiving stop].
        /// </summary>
        /// <param name="t">The t.</param>
        private void OnReceivingStop(TriggerArs t)
        {
            if (_extractTask != null)
            {
                SpinWait.SpinUntil(() => !ReadingQueue);
                _countdown.Signal();
                _countdown.Wait();
                _extractTask.Join();
            }
        }

        private void ReceivingStart()
        {
            ReadingQueue = true;
            _countdown.Reset();
            _extractTask = new Thread(ExtractMessages);
            _extractTask.Start();
        }

        /// <summary>
        /// Begins the receive.
        /// </summary>
        private void ExtractMessages()
        {
            Logger.Debug("Start Extract Messages");
            try
            {
                StartReceive(TimeSpan.FromSeconds(10));
            }
            catch (Exception exception)
            {
                Logger.Error("Error Extract Message", exception);
                throw;
            }
            
        }

        /// <summary>
        /// Starts the receive.
        /// </summary>
        /// <param name="fromSeconds">From seconds.</param>
        protected abstract void StartReceive(TimeSpan fromSeconds);

        /// <summary>
        /// Occurs when [on received completed].
        /// </summary>
        protected event ChannelMessageReceived OnReceivedCompleted;

        /// <summary>
        /// Invokes the on received completed.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void InvokeOnReceivedCompleted(byte[] message)
        {
            var handler = OnReceivedCompleted;
            if (handler != null) handler(this, new ChannelMessageReceivedArgs { Message = message });
        }


        /// <summary>
        /// Called when [received completed].
        /// </summary>
        /// <param name="message">The message.</param>
        private void ReceivedCompleted(byte[] message)
        {
            try
            {
                Logger.Debug("Channel Received Completed");
                _semaphore.WaitOne();
                _countdown.AddCount();
                var thread = new Thread(() =>
                                            {
                                                try
                                                {
                                                    InvokeOnReceivedMessage(message);
                                                }
                                                catch (Exception exception)
                                                {
                                                    Logger.Error("Error On Received Message", exception);
                                                }
                                                finally
                                                {
                                                    _countdown.Signal();
                                                    _semaphore.Release();
                                                }
                                            });
                thread.Start();
            }
            catch (Exception exception)
            {
                Logger.Error("Error Channel Received Completed", exception);
                throw;
            }
        }

        /// <summary>
        /// Receiveds the uncompleted.
        /// </summary>
        protected void ReceivedUncompleted()
        {
            
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            Logger.Debug(string.Format("Starting Channel..."));
            if (_endPointStateMachine.CanChangeState(EndPointStatus.Receiving))
            {
                _endPointStateMachine.ChangeState(EndPointStatus.Receiving);
                Logger.Debug(string.Format("Channel - Started"));
            }
            else
            {
                Logger.Debug(string.Format("Can't change state channel to Started, current state {0}", _endPointStateMachine.CurrentState));
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            Logger.Debug(string.Format("Stopping Channel..."));
            if (_endPointStateMachine.CanChangeState(EndPointStatus.Stopped))
            {
                _endPointStateMachine.ChangeState(EndPointStatus.Stopped);
                Logger.Debug(string.Format("Channel - Stoped"));
            }
            else
            {
                Logger.Debug(string.Format("Can't change state channel to Stoped, current state {0}", _endPointStateMachine.CurrentState));
            }
        }

        /// <summary>
        /// Occurs when [on start].
        /// </summary>
        public event EventHandler OnStart;

        /// <summary>
        /// Occurs when [on stop].
        /// </summary>
        public event EventHandler OnStop;

        /// <summary>
        /// Occurs when [on received message].
        /// </summary>
        public event EventReceiverEndPointHandler OnReceivedMessage;

        /// <summary>
        /// Invokes the on received message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void InvokeOnReceivedMessage(byte[] message)
        {
            Logger.Debug(string.Format("Message Received channel"));

            var handler = OnReceivedMessage;
            if (handler != null) handler(this, new EventReceiverEndPointHandlerArgs { Message = message });
        }
    }
}