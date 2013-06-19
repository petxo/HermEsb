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
        /// Enqueues the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        void Enqueue(object o);

        /// <summary>
        /// Dequeues this instance.
        /// </summary>
        /// <returns></returns>
        object Dequeue();

        /// <summary>
        /// Dequeues the no wait.
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        object DequeueNoWait(object defaultValue);

        /// <summary>
        /// Dequeues the specified milliseconds timeout.
        /// </summary>
        /// <param name="millisecondsTimeout">The milliseconds timeout.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        bool Dequeue(int millisecondsTimeout, out object result);
    }
}