using HermEsb.Core.Messages;

namespace HermEsb.Core.Handlers.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public interface IMonitoringMessageHandler<TMessage> : IMessageHandler<TMessage> where TMessage : IMessage{}
}