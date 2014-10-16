using System.Runtime.Serialization;
using HermEsb.Core.Messages.Monitoring;

namespace HermEsb.Core.Test.Monitoring
{

    public class MonitoringMessageFake : MonitoringMessage
    {


        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>

        public int Count { get; set; }
    }
}