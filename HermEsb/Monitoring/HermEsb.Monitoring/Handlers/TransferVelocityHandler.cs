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
    public class TransferVelocityHandler : MonitoringMessageHandler<ITransferVelocityMessage, TransferVelocityEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransferVelocityHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="translator">The translator.</param>
        /// <param name="serviceInfoService">The service info service.</param>
        public TransferVelocityHandler(IMonitoringRepository<TransferVelocityEntity> repository, 
                                        ITranslator translator, 
                                        IServiceInfoService serviceInfoService)
            : base(serviceInfoService, repository, translator)
        {
        }

        /// <summary>
        /// Handleds the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void HandledEntity(TransferVelocityEntity entity)
        {
            ServiceInfoService.ModifyTransferVelocity(entity);
        }
    }
}