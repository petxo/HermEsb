using System.Collections.Generic;
using HermEsb.Core.Messages.Monitoring;

namespace HermEsb.Monitoring.Entities
{
    public class MessageTypesEntity : MonitoringEntity
    {
        public IList<MessageType> MessageTypes { get; set; }
    }
}