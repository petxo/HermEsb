using System;
using HermEsb.Core.Communication;
using HermEsb.Core.Monitoring;

namespace HermEsb.Core.Processors
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProcessor : IStartable<ProcessorStatus>, IDisposable, IReceiver, IMonitorableProcessor
    {
        
    }
}