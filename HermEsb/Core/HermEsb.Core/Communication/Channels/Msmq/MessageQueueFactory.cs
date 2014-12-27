using HermEsb.Logging;
using System;
using System.Messaging;

#if !MONO
namespace HermEsb.Core.Communication.Channels.Msmq
{
    /// <summary>
    /// 
    /// </summary>
    public static class MessageQueueFactory
    {

        /// <summary>
        /// Creates the queue.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="messageFormatter">The message formatter.</param>
        /// <returns></returns>
        public static IMessageQueue CreateQueue(Uri uri, IMessageFormatter messageFormatter)
        {
            var messageQueueDecorator = new MessageQueueDecorator(FindQueue(uri.LocalPath))
                                            {
                                                Formatter = messageFormatter
                                            };
            return messageQueueDecorator;
        }

        /// <summary>
        /// Finds the queue.
        /// </summary>
        /// <param name="queuePath">The queue path.</param>
        /// <returns></returns>
        private static MessageQueue FindQueue(string queuePath)
        {
            try
            {
                LoggerManager.Instance.Debug(string.Format("Search Queue {0}", queuePath));
                if (!MessageQueue.Exists(queuePath))
                {
                    LoggerManager.Instance.Debug(string.Format("Creating Queue {0}", queuePath));
                    return MessageQueue.Create(queuePath);
                }
            }
            catch (Exception)
            {
                // Error Find Queue, the queue can be remote.

            }

            return new MessageQueue(queuePath);
        }
    }
}
#endif