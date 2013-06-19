using RabbitMQ.Util;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public class SharedQueueDecorator : ISharedQueue
    {

        private readonly SharedQueue _sharedQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedQueueDecorator"/> class.
        /// </summary>
        /// <param name="sharedQueue">The shared queue.</param>
        internal SharedQueueDecorator(SharedQueue sharedQueue)
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
        /// Enqueues the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        public void Enqueue(object o)
        {
            _sharedQueue.Enqueue(o);
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
        /// Dequeues the no wait.
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public object DequeueNoWait(object defaultValue)
        {
            return _sharedQueue.DequeueNoWait(defaultValue);
        }

        /// <summary>
        /// Dequeues the specified milliseconds timeout.
        /// </summary>
        /// <param name="millisecondsTimeout">The milliseconds timeout.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public bool Dequeue(int millisecondsTimeout, out object result)
        {
            return _sharedQueue.Dequeue(millisecondsTimeout, out result);
        }
    }
}