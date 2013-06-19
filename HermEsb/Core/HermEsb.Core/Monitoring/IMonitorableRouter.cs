using System.Collections.Generic;
using HermEsb.Core.Processors.Router.Subscriptors;

namespace HermEsb.Core.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMonitorableRouter : IMonitorableProcessor
    {
        /// <summary>
        /// Gets the message types.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SubscriptionKey> GetMessageTypes();
    }
}