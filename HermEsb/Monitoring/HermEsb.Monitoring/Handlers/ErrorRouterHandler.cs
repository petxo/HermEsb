using HermEsb.Core.ErrorHandling.Messages;
using HermEsb.Monitoring.Entities;
using HermEsb.Monitoring.Repositories;
using HermEsb.Monitoring.Services;
using HermEsb.Monitoring.Translators;

namespace HermEsb.Monitoring.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorRouterHandler : MonitoringMessageHandler<IErrorRouterMessage, ErrorRouterEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRouterHandler"/> class.
        /// </summary>
        /// <param name="serviceInfoService">The service info service.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="translator">The translator.</param>
        public ErrorRouterHandler(IServiceInfoService serviceInfoService, IMonitoringRepository<ErrorRouterEntity> repository, ITranslator translator)
            : base(serviceInfoService, repository, translator)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRouterHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="translator">The translator.</param>
        public ErrorRouterHandler(IMonitoringRepository<ErrorRouterEntity> repository, ITranslator translator)
            : base(repository, translator)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRouterHandler"/> class.
        /// </summary>
        /// <param name="translator">The translator.</param>
        public ErrorRouterHandler(ITranslator translator)
            : base(translator)
        {
        }

        public override void HandledEntity(ErrorRouterEntity entity)
        {
            
        }
    }
}