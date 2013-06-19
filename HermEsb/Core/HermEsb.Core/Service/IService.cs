using System;
using HermEsb.Core.Communication;

namespace HermEsb.Core.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IService : IStartable<ServiceStatus>, IDisposable
    {
        /// <summary>
        /// Occurs when [on stand by].
        /// </summary>
        event EventHandler OnStandBy;
    }
}