using HermEsb.Core.Messages.Monitoring;
using HermEsb.Monitoring.Entities;
using HermEsb.Monitoring.Repositories;
using HermEsb.Monitoring.Services;
using HermEsb.Monitoring.Translators;

namespace HermEsb.Monitoring.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageTypesHandler : MonitoringMessageHandler<MessageTypesMessage, MessageTypesEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTypesHandler"/> class.
        /// </summary>
        /// <param name="serviceInfoService">The service info service.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="translator">The translator.</param>
        public MessageTypesHandler(
            IServiceInfoService serviceInfoService,
            IMonitoringRepository<MessageTypesEntity> repository,
            ITranslator translator)
            : base(serviceInfoService,repository, translator) { }

        /// <summary>
        /// Handleds the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void HandledEntity(MessageTypesEntity entity)
        {
            ServiceInfoService.ModifyMessageTypes(entity);
        }
    }
}