using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;
using HermEsb.Core.Messages.Builders;

namespace HermEsb.Core.Publishers
{
    /// <summary>
    /// 
    /// </summary>
    public static class PublisherFactory
    {
        /// <summary>
        /// Creates the bus publisher.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="messageBuilder">The message builder.</param>
        /// <returns></returns>
        public static BusPublisher CreateBusPublisher(IOutputGateway<IMessage> output, IMessageBuilder messageBuilder)
        {
            return new BusPublisher(output, messageBuilder);
        }
    }
}