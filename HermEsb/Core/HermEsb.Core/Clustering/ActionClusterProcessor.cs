using System.Collections.Generic;
using Bteam.SimpleStateMachine;
using HermEsb.Core.Clustering.Actions;
using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;
using HermEsb.Core.Processors;

namespace HermEsb.Core.Clustering
{
    public class ActionClusterProcessor : IActionClusterProcessor
    {
        private readonly IList<IActionCluster> _actionsCluster;
        private IStateMachine<ProcessorStatus> _stateMachine;
        private readonly Identification _identification;

        public ActionClusterProcessor( Identification identification, IOutputGateway<IMessage> outputGateway)
        {
            _identification = identification;
            _actionsCluster = new List<IActionCluster> { new HeartBeatActionCluster(outputGateway, _identification) };
            ConfigureStateMachine();
        }

        private void ConfigureStateMachine()
        {
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
        }

        public void Start()
        {
            _stateMachine.TryChangeState(ProcessorStatus.Started);
        }

        public void Stop()
        {
            _stateMachine.TryChangeState(ProcessorStatus.Stopped);
        }

        private void OnStop(TriggerArs triggerars)
        {
            foreach (var actionCluster in _actionsCluster)
            {
                actionCluster.Stop();
            }
        }

        private void OnStart(TriggerArs triggerars)
        {
            foreach (var actionCluster in _actionsCluster)
            {
                actionCluster.Start();
            }
        }
    }
}