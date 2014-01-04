using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISharedQueue
    {
        /// <summary>
        /// Closes this instance.
        /// </summary>
        void Close();

        /// <summary>
        /// Dequeues this instance.
        /// </summary>
        /// <returns></returns>
        object Dequeue();

        /// <summary>
        /// Dequeues the specified milliseconds timeout.
        /// </summary>
        /// <param name="millisecondsTimeout">The milliseconds timeout.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        bool Dequeue(int millisecondsTimeout, out BasicDeliverEventArgs result);
    }
}