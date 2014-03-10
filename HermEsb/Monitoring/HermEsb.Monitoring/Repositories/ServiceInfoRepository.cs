using HermEsb.Monitoring.Entities;

namespace HermEsb.Monitoring.Repositories
{
    public class ServiceInfoRepository : MonitoringRepository<ServiceInfoEntity>, IServiceInfoRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceInfoRepository"/> class.
        /// </summary>
        /// <param name="mongoHelper">The mongo helper.</param>
        public ServiceInfoRepository(string connectionName) : base(connectionName) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceInfoRepository"/> class.
        /// </summary>
        /// <param name="mongoHelper">The mongo helper.</param>
        /// <param name="collection">The collection.</param>
        public ServiceInfoRepository(string connectionName, string collection) : base(connectionName, collection) { }
    }
}