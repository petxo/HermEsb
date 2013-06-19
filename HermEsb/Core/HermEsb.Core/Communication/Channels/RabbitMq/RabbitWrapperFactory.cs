using HermEsb.Logging;
using System;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public static class RabbitWrapperFactory
    {
        /// <summary>
        /// Creates the specified queue name.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="rabbitWrapperType">Type of the rabbit wrapper.</param>
        /// <returns></returns>
        public static IRabbitWrapper Create(Uri uri, RabbitWrapperType rabbitWrapperType)
        {
            var rabbitConnectionProvider = new RabbitConnectionProvider(uri) { Logger = LoggerManager.Instance };

            return new RabbitWrapper(rabbitConnectionProvider, rabbitWrapperType) { Logger = LoggerManager.Instance };
        }
    }
}