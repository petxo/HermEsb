using HermEsb.Logging;

namespace HermEsb.Core.Logging
{

    /// <summary>
    /// 
    /// </summary>
    public interface ILoggable
    {
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        ILogger Logger { get; set; }
    }
}