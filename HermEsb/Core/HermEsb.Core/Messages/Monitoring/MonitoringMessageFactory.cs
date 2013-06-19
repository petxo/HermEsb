using System;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Processors.Router;

namespace HermEsb.Core.Messages.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public static class MonitoringMessageFactory
    {
        /// <summary>
        /// Creates the specified monitorable processor.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="monitorableProcessor">The monitorable processor.</param>
        /// <returns></returns>
        public static TMessage Create<TMessage>(IMonitorableProcessor monitorableProcessor) 
            where TMessage : IMonitoringMessage, new()
        {
            var message = new TMessage
                              {
                                  Identification = new Identification
                                                       {
                                                           Id = monitorableProcessor.Identification.Id, 
                                                           Type = monitorableProcessor.Identification.Type
                                                       },
                                  Type = monitorableProcessor is ISubscriber? 
                                                                    ProcessorType.Bus :
                                                                    ProcessorType.AutonomousComponent,
                                  UtcTimeTakenSample = DateTime.UtcNow,

                                  BusIdentification = monitorableProcessor.JoinedBusInfo.Identification
                              };
            return message;
        }
    }
}