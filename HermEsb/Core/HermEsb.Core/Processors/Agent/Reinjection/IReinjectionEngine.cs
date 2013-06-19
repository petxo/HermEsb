using System;
using HermEsb.Core.Communication;
using HermEsb.Core.Ioc;

namespace HermEsb.Core.Processors.Agent.Reinjection
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReinjectionEngine : IStartable<ReinjectionStatus>
    {
        /// <summary>
        /// Reinjects the specified message info.
        /// </summary>
        /// <param name="messageInfo">The message info.</param>
        /// <param name="timeSpan">The time span.</param>
        void Reinject(IMessageInfo messageInfo, TimeSpan timeSpan);
    }
}