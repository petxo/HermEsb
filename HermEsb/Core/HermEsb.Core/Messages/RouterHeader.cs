using System;

namespace HermEsb.Core.Messages
{
    public class RouterHeader
    {
        public string BodyType { get; set; }
        public MessageBusType Type { get; set; }
        public int Priority { get; set; }
        public Identification Identification { get; set; }
        public DateTime CreatedAt { get; set; }
        public int MessageLength { get; set; }
        public int MessagePosition { get; set; }
    }
}