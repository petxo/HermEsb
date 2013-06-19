using System;

namespace HermEsb.Core.Listeners
{
    /// <summary>
    /// 
    /// </summary>
    public interface IListener
    {
        /// <summary>
        /// Envia el mensaje a la cola para procesarlo mas tarde.
        /// </summary>
        void ProcessLater();

        /// <summary>
        /// Envia el mensaje a la cola para procesarlo mas tarde.
        /// </summary>
        /// <param name="miliseconds">The miliseconds.</param>
        void ProcessLater(int miliseconds);

        /// <summary>
        /// Envia el mensaje a la cola para procesarlo mas tarde.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        void ProcessLater(TimeSpan timeSpan);
    }
}