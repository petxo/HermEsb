using System;
using System.Collections.Generic;
using HermEsb.Configuration.Services;
using HermEsb.Core;
using HermEsb.Core.Gateways;
using HermEsb.Core.Gateways.Agent;
using HermEsb.Core.Messages;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Monitoring.Frequences;
using HermEsb.Core.Processors;

namespace HermEsb.Configuration.Monitoring
{
    public class MonitorConfigurator
    {
        private readonly MonitorConfig _monitorConfig;
        private readonly Identification _identification;
        private IOutputGateway<IMessage> _output;
        private readonly IList<string> _assemblies;


        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorConfigurator"/> class.
        /// </summary>
        /// <param name="monitorConfig">The monitor config.</param>
        /// <param name="identification">The identification.</param>
        public MonitorConfigurator(MonitorConfig monitorConfig, Identification identification)
        {
            _monitorConfig = monitorConfig;
            _identification = identification;
            _assemblies = new List<string>();
        }

        /// <summary>
        /// Configures this instance.
        /// </summary>
        /// <returns></returns>
        public MonitorConfigurator Configure()
        {
            CreateMonitorFactory()
                .CreateOutput()
                .LoadAssemblies();
            
            return this;
        }

        /// <summary>
        /// Loads the assemblies.
        /// </summary>
        /// <returns></returns>
        private MonitorConfigurator LoadAssemblies()
        {
            _assemblies.Add(string.Format("{0}.dll", GetType().Assembly.GetName().Name));

            foreach (HandlerAssemblyConfig samplersAssembly in _monitorConfig.SamplersAssemblies)
            {
                _assemblies.Add(samplersAssembly.Assembly);
            }
            return this;
        }

        /// <summary>
        /// Creates the output.
        /// </summary>
        /// <returns></returns>
        private MonitorConfigurator CreateOutput()
        {
            var uri = new Uri(_monitorConfig.Output.Uri);
            _output = AgentGatewayFactory.CreateOutputGateway(_identification, uri, _monitorConfig.Output.Transport);
            return this;
        }

        /// <summary>
        /// Creates the monitor factory.
        /// </summary>
        /// <returns></returns>
        private MonitorConfigurator CreateMonitorFactory()
        {
            var frequenceHelper = new FrequenceHelper();
            if (_monitorConfig.FrequenceLevel.Highest.HasValue)
            {
                frequenceHelper.ChangeFrequence(FrequenceLevel.Highest, _monitorConfig.FrequenceLevel.Highest.Value);
            }
            if (_monitorConfig.FrequenceLevel.High.HasValue)
            {
                frequenceHelper.ChangeFrequence(FrequenceLevel.High, _monitorConfig.FrequenceLevel.High.Value);
            }
            if (_monitorConfig.FrequenceLevel.Normal.HasValue)
            {
                frequenceHelper.ChangeFrequence(FrequenceLevel.Normal, _monitorConfig.FrequenceLevel.Normal.Value);
            }
            if (_monitorConfig.FrequenceLevel.Low.HasValue)
            {
                frequenceHelper.ChangeFrequence(FrequenceLevel.Low, _monitorConfig.FrequenceLevel.Low.Value);
            }
            if (_monitorConfig.FrequenceLevel.Lowest.HasValue)
            {
                frequenceHelper.ChangeFrequence(FrequenceLevel.Lowest, _monitorConfig.FrequenceLevel.Lowest.Value);
            }
            MonitorFactory.Create(frequenceHelper);

            return this;
        }

        /// <summary>
        /// Creates the specified controller.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        public IMonitor Create(IController controller)
        {
            return MonitorFactory.Instance.Create(controller, _output, _assemblies);
        }
    }
}