using HermEsb.Logging;
using System;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public class RabbitChannelController : IChannelController
    {

        private readonly IRabbitWrapper _rabbitWrapper;

        private ILogger _logger;

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger
        {
            get { return _logger?? new NullLogger(); }
            set { _logger = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitChannelController"/> class.
        /// </summary>
        /// <param name="rabbitWrapper">The rabbit wrapper.</param>
        public RabbitChannelController(IRabbitWrapper rabbitWrapper)
        {
            _rabbitWrapper = rabbitWrapper;
        }

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            try
            {
                return _rabbitWrapper.Count();
            }
            catch (Exception exception)
            {
                Logger.Error("Error Controller Count", exception);
                throw;
            }
            
        }

        /// <summary>
        /// Purges this instance.
        /// </summary>
        public void Purge()
        {
            _rabbitWrapper.Purge();
        }
    }
}