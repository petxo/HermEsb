using System.Collections.Generic;
using HermEsb.Core.Messages.Control;

namespace HermEsb.Core.Test.Processors.Router
{
    public class FakeReplayMessage : IControlMessage
    {
        public FakeReplayMessage()
        {
            Session = new Dictionary<string, object>();
        }

        public IDictionary<string, object> Session { get; set; }
    }
}