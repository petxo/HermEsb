using System;
using System.Threading;
using System.Threading.Tasks;
using Bteam.SimpleStateMachine;
using HermEsb.Core.Clustering.Messages;
using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;
using HermEsb.Core.Processors;

namespace HermEsb.Core.Clustering.Actions
{
    public class HeartBeatActionCluster : IActionCluster
    {
        private Task _task;
        private readonly IStateMachine<ProcessorStatus> _stateMachine;
        private readonly IOutputGateway<IMessage> _outputGateway;
        private readonly Identification _identification;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeartBeatActionCluster"/> class.
        /// </summary>
        /// <param name="outputGateway">The output gateway.</param>
        /// <param name="identification">The identification.</param>
        public HeartBeatActionCluster(IOutputGateway<IMessage> outputGateway, Identification identification)
        {
            _outputGateway = outputGateway;
            _identification = identification;
            _stateMachine = StateMachineFactory.Create(ProcessorStatus.Initializing)
                                               .Permit(ProcessorStatus.Initializing, ProcessorStatus.Configured)
                                               .Permit(ProcessorStatus.Configured, ProcessorStatus.Started,
                                                       OnStart)
                                               .Permit(ProcessorStatus.Started, ProcessorStatus.Stopped,
                                                       OnStop)
                                               .Permit(ProcessorStatus.Stopped, ProcessorStatus.Started,
                                                       OnStart)
                                               .Permit(ProcessorStatus.Stopped, ProcessorStatus.Configured);

            _stateMachine.ChangeState(ProcessorStatus.Configured);

            _task = new Task(Action);
        }

        private void Action()
        {
            while (_stateMachine.CurrentState == ProcessorStatus.Started)
            {
                var heartBeatClusterMessage = new HeartBeatClusterMessage { Identification = _identification };
                _outputGateway.Send(heartBeatClusterMessage);
                Thread.Sleep(TimeSpan.FromSeconds(60));
            }
        }

        private void OnStop(TriggerArs triggerars)
        {
            _task.Wait();
        }

        private void OnStart(TriggerArs triggerars)
        {
            _task.Start();
        }

        public void Start()
        {
            _stateMachine.TryChangeState(ProcessorStatus.Started);
        }

        public void Stop()
        {
            _stateMachine.TryChangeState(ProcessorStatus.Stopped);
        }
    }
}