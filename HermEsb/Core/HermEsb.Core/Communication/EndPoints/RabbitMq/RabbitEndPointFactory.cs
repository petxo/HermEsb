using HermEsb.Core.Communication.Channels.RabbitMq;
using HermEsb.Logging;
using System;

namespace HermEsb.Core.Communication.EndPoints.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public class RabbitEndPointFactory : IEndPointFactory
    {
        /// <summary>
        /// Creates the channel.
        /// </summary>
        /// <param name="uri">The URI http://server/exchange/queue/key </param>
        /// <returns></returns>
        public ISenderEndPoint CreateSender(Uri uri)
        {
            var rabbitWrapper = RabbitWrapperFactory.Create(uri, RabbitWrapperType.Output);
            var channel = new RabbitSenderChannel(rabbitWrapper) { Logger = LoggerManager.Instance };
            return new SenderEndPoint(uri, channel) { Logger = LoggerManager.Instance };
        }

        /// <summary>
        /// Creates the receiver.
        /// </summary>
        /// <param name="uri">The URI http://server/exchange/queue/key </param>
        /// <param name="numberOfParallelTasks">The number of parallel tasks.</param>
        /// <returns></returns>
        public IReceiverEndPoint CreateReceiver(Uri uri, int numberOfParallelTasks)
        {
            var rabbitWrapper = RabbitWrapperFactory.Create(uri, RabbitWrapperType.Input);
            var channelController = new RabbitChannelController(rabbitWrapper) { Logger = LoggerManager.Instance };
            var channel = new RabbitReceiverChannel(numberOfParallelTasks, rabbitWrapper) { Logger = LoggerManager.Instance };
            return new ReceiverEndPoint(uri, channelController, channel) { Logger = LoggerManager.Instance };
        }
    }
}