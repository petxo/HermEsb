using System.Runtime.Serialization;
using HermEsb.Core.Communication;

namespace HermEsb.Core.Messages.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInputQueueMessage : IMonitoringMessage
    {
        /// <summary>
        /// Gets or sets the input processor queue.
        /// </summary>
        /// <value>The input processor queue.</value>
        string InputProcessorQueue { get; set; }

        /// <summary>
        /// Gets or sets the type of the input processor queue.
        /// </summary>
        /// <value>The type of the input processor queue.</value>
        TransportType InputProcessorQueueTransport { get; set; }

        /// <summary>
        /// Gets or sets the input control queue.
        /// </summary>
        /// <value>The input control queue.</value>
        string InputControlQueue { get; set; }

        /// <summary>
        /// Gets or sets the type of the input control queue.
        /// </summary>
        /// <value>The type of the input control queue.</value>
        TransportType InputControlQueueTransport { get; set; }
    }
}