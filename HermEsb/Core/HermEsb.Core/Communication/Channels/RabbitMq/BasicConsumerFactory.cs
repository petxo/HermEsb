using HermEsb.Logging;
using System;
using RabbitMQ.Client;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public static class BasicConsumerFactory
    {
        /// <summary>
        /// Creates the queue.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="queueName">Name of the queue.</param>
        /// <returns></returns>
        public static IQueueBasicConsumer CreateQueueConsumer(IModel channel, string queueName)
        {
            try
            {
                var consumer = new QueueingBasicConsumer(channel);
                channel.BasicQos(0, 3000, false);
                channel.BasicConsume(queueName, false, consumer);

                ISharedQueue sharedQueue = new SharedQueueDecorator(consumer.Queue);
                return new QueueBasicConsumerDecorator(consumer, sharedQueue);
            }
            catch (Exception exception)
            {
                LoggerManager.Instance.Error("Error Create Consumer", exception);
                
                throw;
            }
        }
    }
}