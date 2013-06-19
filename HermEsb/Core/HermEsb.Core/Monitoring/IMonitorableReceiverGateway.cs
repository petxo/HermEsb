namespace HermEsb.Core.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMonitorableReceiverGateway : IMonitorableGateway
    {
        /// <summary>
        /// Occurs when [on message bus received].
        /// </summary>
        event MessageGatewayEventHandler OnMessageBusReceived;

        /// <summary>
        /// Counts the items in gateway.
        /// </summary>
        /// <returns></returns>
        int Count();
    }
}