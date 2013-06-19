using System;
using HermEsb.Core.Processors;

namespace HermEsb.Core.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMonitorableProcessor : IIdentificable
    {
        /// <summary>
        /// Gets or sets the joined bus info.
        /// </summary>
        /// <value>The joined bus info.</value>
        IBusInfo JoinedBusInfo { get; set; }

        /// <summary>
        /// Gets the input gateway.
        /// </summary>
        /// <value>The input gateway.</value>
        IMonitorableReceiverGateway MonitorableInputGateway { get; }

        /// <summary>
        /// Gets the monitorable sender gateway.
        /// </summary>
        /// <value>The monitorable sender gateway.</value>
        IMonitorableSenderGateway MonitorableSenderGateway { get; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>The status.</value>
        ProcessorStatus ProcessorStatus { get; }

        /// <summary>
        /// Occurs when [on message received].
        /// </summary>
        event MonitorEventHandler OnMessageReceived;

        /// <summary>
        /// Occurs when [on message sended].
        /// </summary>
        event MonitorEventHandler OnMessageSent;

        /// <summary>
        /// Occurs when [on created handler].
        /// </summary>
        event HandlerMonitorEventHandler OnCreatedHandler;

        /// <summary>
        /// Occurs when [on destoyed handler].
        /// </summary>
        event HandlerMonitorEventHandler OnDestoyedHandler;

        /// <summary>
        /// Occurs when [sender gateway changed].
        /// </summary>
        event EventHandler SenderGatewayChanged;
    }
}