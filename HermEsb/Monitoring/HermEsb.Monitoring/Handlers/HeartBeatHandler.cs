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
    public class HeartBeatHandler : MonitoringMessageHandler<IHeartBeatMessage,HeartBeatEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeartBeatHandler"/> class.
        /// </summary>
        /// <param name="serviceInfoService">The service info service.</param>
        /// <param name="heartBeatRepository">The heart beat repository.</param>
        /// <param name="heartBeatTranslator">The heart beat translator.</param>
        public HeartBeatHandler(
            IServiceInfoService serviceInfoService,
            IMonitoringRepository<HeartBeatEntity> heartBeatRepository,
            ITranslator heartBeatTranslator)
            : base(serviceInfoService,heartBeatRepository, heartBeatTranslator) {}

        /// <summary>
        /// Handleds the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void HandledEntity(HeartBeatEntity entity)
        {
            ServiceInfoService.ModifyStatus(entity);            
        }
    }
}
