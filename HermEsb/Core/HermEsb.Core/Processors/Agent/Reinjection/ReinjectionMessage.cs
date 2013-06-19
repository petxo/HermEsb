using System;
using HermEsb.Core.Ioc;

namespace HermEsb.Core.Processors.Agent.Reinjection
{
    /// <summary>
    /// 
    /// </summary>
    public class ReinjectionMessage
    {
        /// <summary>
        /// Creates the specified message info.
        /// </summary>
        /// <param name="messageInfo">The message info.</param>
        /// <param name="reinjectionTime">The reinjection time.</param>
        /// <returns></returns>
        public static ReinjectionMessage Create(IMessageInfo messageInfo, TimeSpan reinjectionTime)
        {
            return new ReinjectionMessage
                       {
                           CreatedAt = DateTime.Now, 
                           MessageInfo = messageInfo, 
                           ReinjectionTime = reinjectionTime
                       };
        }

        private ReinjectionMessage()
        {
            
        }

        /// <summary>
        /// Gets or sets the message info.
        /// </summary>
        /// <value>The message info.</value>
        public IMessageInfo MessageInfo { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the reinjection time.
        /// </summary>
        /// <value>The reinjection time.</value>
        public TimeSpan ReinjectionTime { get; set; }
    }
}