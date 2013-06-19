using Bteam.SimpleStateMachine;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HermEsb.Core.Communication.EndPoints;

namespace HermEsb.Core.Communication.Channels
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractReceiverChannel : AbstractSenderChannel, IReceiverChannel
    {
        private Task[] _messageExtractionTasks;
        private IStateMachine<EndPointStatus> _endPointStateMachine;
        private readonly int _numberOfParallelTasks;
        private int _numberOfCurrentTasks;
        private Task _extractTask;
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractReceiverChannel"/> class.
        /// </summary>
        /// <param name="numberOfParallelTasks">The number of parallel tasks.</param>
        internal AbstractReceiverChannel(int numberOfParallelTasks)
        {
            ConfigureStateMachine();
            _numberOfParallelTasks = numberOfParallelTasks;

            // Retrieve the number of tasks from configuration
            // Create an array of MessageBus returning tasks
            _messageExtractionTasks = new Task[_numberOfParallelTasks];
            _numberOfCurrentTasks = 0;

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
                Task.WaitAll(_messageExtractionTasks.Where(task => task != null).ToArray());

                foreach (var messageExtractionTask in _messageExtractionTasks.Where(task => task != null))
                {
                    messageExtractionTask.Dispose();
                }
                _messageExtractionTasks = new Task[_numberOfParallelTasks];
                _numberOfCurrentTasks = 0;

                Task.WaitAny(_extractTask);
                _cancellationTokenSource.Cancel();
                _extractTask.Dispose();
            }
        }

        private void ReceivingStart()
        {
            ReadingQueue = true;
            _cancellationTokenSource = new CancellationTokenSource();
            _extractTask = Task.Factory.StartNew(ExtractMessages, _cancellationTokenSource.Token);
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
        protected void InvokeOnReceivedCompleted(string message)
        {
            var handler = OnReceivedCompleted;
            if (handler != null) handler(this, new ChannelMessageReceivedArgs { Message = message });
        }


        /// <summary>
        /// Called when [received completed].
        /// </summary>
        /// <param name="message">The message.</param>
        private void ReceivedCompleted(string message)
        {
            try
            {
                Logger.Debug("Channel Received Completed");
                int nextFreeTaskIndex;
                if (_numberOfCurrentTasks == _numberOfParallelTasks)
                {
                    nextFreeTaskIndex = Task.WaitAny(_messageExtractionTasks);
                    _messageExtractionTasks[nextFreeTaskIndex].Dispose();
                }
                else
                {
                    nextFreeTaskIndex = _numberOfCurrentTasks;
                    _numberOfCurrentTasks++;
                }

                // Start a new parallel task in the next free place holder
                _messageExtractionTasks[nextFreeTaskIndex] = Task.Factory.StartNew(() => InvokeOnReceivedMessage(message));

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
        private void InvokeOnReceivedMessage(string message)
        {
            Logger.Debug(string.Format("Message Received channel"));

            var handler = OnReceivedMessage;
            if (handler != null) handler(this, new EventReceiverEndPointHandlerArgs { Message = message });
        }
    }
}