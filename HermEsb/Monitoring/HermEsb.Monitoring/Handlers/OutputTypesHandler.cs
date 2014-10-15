using System;
using HermEsb.Core.Handlers.Monitoring;
using HermEsb.Monitoring.Messages;
using HermEsb.Monitoring.Services;

namespace HermEsb.Monitoring.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class OutputTypesHandler : IMonitoringMessageHandler<IOutputTypesMessage>
    {
        private readonly IServiceInfoService _serviceInfoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTypesHandler"/> class.
        /// </summary>
        /// <param name="serviceInfoService">The service info service.</param>
        public OutputTypesHandler(IServiceInfoService serviceInfoService)
        {
            _serviceInfoService = serviceInfoService;
        }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleMessage(IOutputTypesMessage message)
        {
            _serviceInfoService.ModifyOutputMessages(message.Identification, message.Type, message.MessageTypes);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _serviceInfoService.Dispose();
            }
        }
    }

}