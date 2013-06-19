using System;

namespace HermEsb.Core.Communication.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEndPointFactory
    {
        /// <summary>
        /// Creates the channel.
        /// </summary>
        /// <param name="uri">The URI http://server/exchange/queue/key </param>
        /// <returns></returns>
        ISenderEndPoint CreateSender(Uri uri);

        /// <summary>
        /// Creates the receiver.
        /// </summary>
        /// <param name="uri">The URI http://server/exchange/queue/key </param>
        /// <param name="numberOfParallelTasks">The number of parallel tasks.</param>
        /// <returns></returns>
        IReceiverEndPoint CreateReceiver(Uri uri, int numberOfParallelTasks);
    }
}