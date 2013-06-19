using System;
using System.Linq;
using System.Reflection;
using HermEsb.Core.Monitoring.Frequences;
using HermEsb.Logging;

namespace HermEsb.Core.Monitoring
{
    /// <summary>
    /// </summary>
    public class SamplerFactory
    {
        private readonly IFrequenceHelper _frequenceHelper;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SamplerFactory" /> class.
        /// </summary>
        /// <param name="frequenceHelper">The frequence helper.</param>
        public SamplerFactory(IFrequenceHelper frequenceHelper)
        {
            _frequenceHelper = frequenceHelper;
        }

        /// <summary>
        ///     Creates the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="monitorableProcessor">The monitorable processor.</param>
        /// <returns></returns>
        public ISampler Create(Type type, IMonitorableProcessor monitorableProcessor)
        {
            var frequenceLevel =
                (SamplerFrequenceLevelAttribute)
                type.GetCustomAttributes(typeof (SamplerFrequenceLevelAttribute), true).FirstOrDefault();

            var sampler = (Sampler) Activator.CreateInstance(type, monitorableProcessor);
            InjectLogger(sampler);
            sampler.SetFrequence(_frequenceHelper.GetFrequence(frequenceLevel.Frequence));
            return sampler;
        }

        /// <summary>
        ///     Injects the logger.
        /// </summary>
        /// <param name="sampler">The sampler.</param>
        private static void InjectLogger(Sampler sampler)
        {
            PropertyInfo propertyInfo = sampler.GetType().GetProperty("Logger");
            if (propertyInfo != null)
                propertyInfo.SetValue(sampler, LoggerManager.Instance, null);
        }
    }
}