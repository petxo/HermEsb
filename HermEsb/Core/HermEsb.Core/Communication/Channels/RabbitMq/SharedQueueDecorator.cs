using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Util;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public class SharedQueueDecorator : ISharedQueue
    {

        private readonly SharedQueue<BasicDeliverEventArgs> _sharedQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedQueueDecorator"/> class.
        /// </summary>
        /// <param name="sharedQueue">The shared queue.</param>
        internal SharedQueueDecorator(SharedQueue<BasicDeliverEventArgs> sharedQueue)
        {
            _sharedQueue = sharedQueue;
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            _sharedQueue.Close();
        }


        /// <summary>
        /// Dequeues this instance.
        /// </summary>
        /// <returns></returns>
        public object Dequeue()
        {
            return _sharedQueue.Dequeue();
        }

        /// <summary>
        /// Dequeues the specified milliseconds timeout.
        /// </summary>
        /// <param name="millisecondsTimeout">The milliseconds timeout.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public bool Dequeue(int millisecondsTimeout, out BasicDeliverEventArgs result)
        {
            return _sharedQueue.Dequeue(millisecondsTimeout, out result);
        }
    }
}