using System;
using Bteam.SimpleStateMachine;
using HermEsb.Core.Gateways;
using HermEsb.Core.Ioc;

namespace HermEsb.Core.Processors.Agent.Reinjection
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public class DirectReinjectionEngine<TMessage> : IReinjectionEngine
    {
        private readonly IInputGateway<TMessage> _inputGateway;
        private IStateMachine<ReinjectionStatus> _stateMachine;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectReinjectionEngine{TMessage}" /> class.
        /// </summary>
        /// <param name="inputGateway">The input gateway.</param>
        public DirectReinjectionEngine(IInputGateway<TMessage> inputGateway)
        {
            _inputGateway = inputGateway;
            InitializeStateMachine();
        }

        /// <summary>
        ///     Reinjects the specified message info.
        /// </summary>
        /// <param name="messageInfo">The message info.</param>
        /// <param name="timeSpan"></param>
        public void Reinject(IMessageInfo messageInfo, TimeSpan timeSpan)
        {
            _inputGateway.Reinject(messageInfo.Body, messageInfo.Header);
        }

        public ReinjectionStatus Status
        {
            get { return _stateMachine.CurrentState; }
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public void Start()
        {
            _stateMachine.TryChangeState(ReinjectionStatus.Started);
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public void Stop()
        {
            _stateMachine.TryChangeState(ReinjectionStatus.Stopped);
        }

        /// <summary>
        ///     Occurs when [on start].
        /// </summary>
        public event EventHandler OnStart;

        /// <summary>
        ///     Occurs when [on stop].
        /// </summary>
        public event EventHandler OnStop;

        private void InitializeStateMachine()
        {
            _stateMachine = StateMachineFactory.Create(ReinjectionStatus.Stopped);
            _stateMachine.Permit(ReinjectionStatus.Stopped, ReinjectionStatus.Started, ars => InvokeOnStart());
            _stateMachine.Permit(ReinjectionStatus.Started, ReinjectionStatus.Stopped, ars => InvokeOnStop());
        }

        /// <summary>
        ///     Invokes the on start.
        /// </summary>
        private void InvokeOnStart()
        {
            EventHandler handler = OnStart;
            if (handler != null) handler(this, new EventArgs());
        }

        /// <summary>
        ///     Invokes the on stop.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="System.EventArgs" /> instance containing the event data.
        /// </param>
        private void InvokeOnStop()
        {
            EventHandler handler = OnStop;
            if (handler != null) handler(this, new EventArgs());
        }
    }
}