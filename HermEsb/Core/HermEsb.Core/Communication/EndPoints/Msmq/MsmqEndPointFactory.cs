#if !MONO
using HermEsb.Core.Communication.Channels.Msmq;
using HermEsb.Logging;
using System;

namespace HermEsb.Core.Communication.EndPoints.Msmq
{
    /// <summary>
    /// 
    /// </summary>
    public class MsmqEndPointFactory : IEndPointFactory
    {
        /// <summary>
        /// Creates the receiver.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="numberOfParallelTasks">The number of parallel tasks.</param>
        /// <returns></returns>
        public IReceiverEndPoint CreateReceiver(Uri uri, int numberOfParallelTasks)
        {
            var messageFormatter = new TextFormatter();
            var messageQueue = MessageQueueFactory.CreateQueue(uri, messageFormatter);
            var channel = new MsmqReceiverChannel(numberOfParallelTasks, messageQueue);
            var channelController = new MsmqChannelController(messageQueue);

            return new ReceiverEndPoint(uri, channelController, channel)
                       {
                           Logger = LoggerManager.Instance
                       };
        }

        /// <summary>
        /// Creates the sender.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public ISenderEndPoint CreateSender(Uri uri)
        {
            var messageFormatter = new TextFormatter();
            var messageQueue = MessageQueueFactory.CreateQueue(uri, messageFormatter);
            var channel = new MsmqSenderChannel(messageQueue);

            return new SenderEndPoint(uri, channel)
                        {
                            Logger = LoggerManager.Instance
                        };
        }
    }
}
#endif