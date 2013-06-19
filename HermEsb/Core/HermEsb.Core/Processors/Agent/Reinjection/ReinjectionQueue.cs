using System;
using System.Collections.Generic;
using HermEsb.Core.Ioc;

namespace HermEsb.Core.Processors.Agent.Reinjection
{
    public class ReinjectionQueue
    {
        private Queue<ReinjectionMessage> _reinjectionQueue;

        public ReinjectionQueue()
        {
            _reinjectionQueue = new Queue<ReinjectionMessage>();
        }

        public void Enqueue(IMessageInfo messageInfo, TimeSpan reinjectionTime)
        {
            
        }
    }
}