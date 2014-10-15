using HermEsb.Monitoring.Entities;
using HermEsb.Monitoring.Messages;
using HermEsb.Monitoring.Repositories;
using HermEsb.Monitoring.Services;
using HermEsb.Monitoring.Translators;

namespace HermEsb.Monitoring.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessingVelocityHandler : MonitoringMessageHandler<IProcessingVelocityMessage, ProcessingVelocityEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessingVelocityHandler"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="processingVelocityRepository">The processing velocity repository.</param>
        /// <param name="processingVelocityTranslator">The processing velocity translator.</param>
        public ProcessingVelocityHandler(
            IServiceInfoService service,
            IMonitoringRepository<ProcessingVelocityEntity> processingVelocityRepository,
            ITranslator processingVelocityTranslator)
            : base(service,processingVelocityRepository, processingVelocityTranslator)
        { }

        public override void HandledEntity(ProcessingVelocityEntity entity)
        {
            ServiceInfoService.ModifyProcessingVelocity(entity);
        }
    }
}