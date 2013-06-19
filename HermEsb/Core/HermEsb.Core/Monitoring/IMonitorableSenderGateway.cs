namespace HermEsb.Core.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMonitorableSenderGateway : IMonitorableGateway
    {
        /// <summary>
        /// Occurs when [sent message].
        /// </summary>
        event MessageGatewayEventHandler SentMessage;
    }
}