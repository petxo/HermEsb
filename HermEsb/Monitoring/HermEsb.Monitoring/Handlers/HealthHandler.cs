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
    public class HealthHandler : MonitoringMessageHandler<IHealthMessage, HealthEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthHandler"/> class.
        /// </summary>
        /// <param name="serviceInfoService">The service info service.</param>
        /// <param name="healthRepository">The health repository.</param>
        /// <param name="healthTranslator">The health translator.</param>
        public HealthHandler(
            IServiceInfoService serviceInfoService,
            IMonitoringRepository<HealthEntity> healthRepository, 
            ITranslator healthTranslator)
            : base(serviceInfoService,healthRepository, healthTranslator) { }

        /// <summary>
        /// Handleds the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void HandledEntity(HealthEntity entity)
        {
            ServiceInfoService.ModifyHealthInfo(entity);
        }
    }
}