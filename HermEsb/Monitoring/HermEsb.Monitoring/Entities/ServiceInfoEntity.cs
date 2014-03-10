using System.Collections.Generic;
using HermEsb.Core.Communication;
using HermEsb.Core.Messages.Monitoring;
using HermEsb.Core.Processors;

namespace HermEsb.Monitoring.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceInfoEntity : MonitoringEntity 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceInfoEntity"/> class.
        /// </summary>
        public ServiceInfoEntity()
        {
            InputTypes = new List<MessageType>();
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public ProcessorStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the input processor queue.
        /// </summary>
        /// <value>The input processor queue.</value>
        public string InputProcessorQueue { get; set; }

        /// <summary>
        /// Gets or sets the type of the input processor queue.
        /// </summary>
        /// <value>The type of the input processor queue.</value>
        public TransportType InputProcessorQueueTransport { get; set; }

        /// <summary>
        /// Gets or sets the input control queue.
        /// </summary>
        /// <value>The input control queue.</value>
        public string InputControlQueue { get; set; }

        /// <summary>
        /// Gets or sets the type of the input control queue.
        /// </summary>
        /// <value>The type of the input control queue.</value>
        public TransportType InputControlQueueTransport { get; set; }

        /// <summary>
        /// Gets or sets the memory working set.
        /// </summary>
        /// <value>The memory working set.</value>
        public long MemoryWorkingSet { get; set; }

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>The input.</value>
        public VelocityEntity InputTranfer { get; set; }

        /// <summary>
        /// Gets or sets the output.
        /// </summary>
        /// <value>The output.</value>
        public VelocityEntity OutputTransfer { get; set; }

 		/// <summary>
        /// Gets or sets the total messages.
        /// </summary>
        /// <value>The total messages.</value>
        public int TotalMessages { get; set; }

        /// <summary>
        /// Gets or sets the message types.
        /// </summary>
        /// <value>The message types.</value>
        public IList<MessageType> InputTypes { get; set; }

        /// <summary>
        /// Gets or sets the output types.
        /// </summary>
        /// <value>The output types.</value>
        public IList<MessageType> OutputTypes { get; set; }

        /// <summary>
        /// Gets or sets the number messages processed.
        /// </summary>
        /// <value>The number messages processed.</value>
        public int NumberMessagesProcessed { get; set; }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        /// <value>The velocity.</value>
        public float Velocity { get; set; }

        /// <summary>
        /// Gets or sets the latency.
        /// </summary>
        /// <value>The latency.</value>
        public float Latency { get; set; }

        /// <summary>
        /// Gets or sets the peak max latency.
        /// </summary>
        /// <value>The peak max latency.</value>
        public float PeakMaxLatency { get; set; }

        /// <summary>
        /// Gets or sets the peak min latency.
        /// </summary>
        /// <value>The peak min latency.</value>
        public float PeakMinLatency { get; set; }
    }
}