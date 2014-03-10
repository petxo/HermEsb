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
    public class QueueLoadHandler : MonitoringMessageHandler<IQueueLoadMessage,QueueLoadEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueueLoadHandler"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="queueLoadRepository">The queue load repository.</param>
        /// <param name="queueLoadTranslator">The queue load translator.</param>
        public QueueLoadHandler(
            IServiceInfoService service,
            IMonitoringRepository<QueueLoadEntity> queueLoadRepository,
            ITranslator queueLoadTranslator)
            : base(service,queueLoadRepository, queueLoadTranslator)
        {}

        public override void HandledEntity(QueueLoadEntity entity)
        {
            ServiceInfoService.ModifyQueueLoad(entity);
        }
    }
}