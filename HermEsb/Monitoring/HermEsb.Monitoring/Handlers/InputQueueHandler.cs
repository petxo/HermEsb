using HermEsb.Core.Messages.Monitoring;
using HermEsb.Monitoring.Entities;
using HermEsb.Monitoring.Services;
using HermEsb.Monitoring.Translators;

namespace HermEsb.Monitoring.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class InputQueueHandler : MonitoringMessageHandler<InputQueueMessage,ServiceInfoEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputQueueHandler"/> class.
        /// </summary>
        /// <param name="serviceInfoService">The service info service.</param>
        /// <param name="translator">The translator.</param>
        public InputQueueHandler(
                IServiceInfoService serviceInfoService,
                ITranslator translator)
            : base(serviceInfoService,null,translator){}

        /// <summary>
        /// Handleds the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void HandledEntity(ServiceInfoEntity entity)
        {
            ServiceInfoService.ModifyServiceQueues(entity);
        }
    }

}