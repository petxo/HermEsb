using System.Runtime.Serialization;
using HermEsb.Core.Messages.Monitoring;

namespace HermEsb.Core.Test.Monitoring
{
    [DataContract]
    public class MonitoringMessageFake : MonitoringMessage
    {


        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        [DataMember]
        public int Count { get; set; }
    }
}