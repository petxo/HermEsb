using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;
using HermEsb.Core.Monitoring.Frequences;
using HermEsb.Core.Processors;
using HermEsb.Core.Processors.Router;

namespace HermEsb.Core.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public class MonitorFactory
    {
        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static MonitorFactory Instance { get; private set; }

        /// <summary>
        /// Creates the specified frequence helper.
        /// </summary>
        /// <param name="frequenceHelper">The frequence helper.</param>
        public static void Create(IFrequenceHelper frequenceHelper)
        {
            var samplerFactory = new SamplerFactory(frequenceHelper);
            Instance = new MonitorFactory(samplerFactory);
        }

        private readonly SamplerFactory _samplerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorFactory"/> class.
        /// </summary>
        /// <param name="samplerFactory">The sampler factory.</param>
        private MonitorFactory(SamplerFactory samplerFactory)
        {
            _samplerFactory = samplerFactory;
        }


        /// <summary>
        /// Creates the specified monitorable processor.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="outputGateway">The output gateway.</param>
        /// <param name="assemblies">The assemblies.</param>
        /// <returns></returns>
        public IMonitor Create(IController controller,
                               IOutputGateway<IMessage> outputGateway,
                               IEnumerable<string> assemblies)
        {
            var samplers = CreateSamplers(assemblies, controller.Processor);

            var monitor = new Monitor(controller, outputGateway);
            foreach (var sampler in samplers)
            {
                monitor.AddSampler(sampler);
            }

            return monitor;
        }

        /// <summary>
        /// Creates the samplers.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <param name="monitorableProcessor">The monitorable processor.</param>
        /// <returns></returns>
        private IEnumerable<ISampler> CreateSamplers(IEnumerable<string> assemblies, IMonitorableProcessor monitorableProcessor)
        {
            return from assemblyName in assemblies 
                            select Assembly.LoadFrom(string.Format(@"{0}/{1}", AppDomain.CurrentDomain.BaseDirectory, assemblyName))
                   into assembly from type in assembly.GetTypes()
                       where IsSampler(type, monitorableProcessor) 
                            select _samplerFactory.Create(type, monitorableProcessor);
        }


        /// <summary>
        /// Determines whether the specified t is sampler.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="monitorableProcessor">The monitorable processor.</param>
        /// <returns>
        /// 	<c>true</c> if the specified t is sampler; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsSampler(Type t, IMonitorableProcessor monitorableProcessor)
        {
            if (t.IsAbstract)
                return false;

            if (monitorableProcessor is ISubscriber)
                return (typeof(Sampler).IsAssignableFrom(t) || typeof(IPublisherSampler).IsAssignableFrom(t));

            return (typeof(Sampler).IsAssignableFrom(t) && !(typeof(IPublisherSampler).IsAssignableFrom(t)));
        }
    }
}