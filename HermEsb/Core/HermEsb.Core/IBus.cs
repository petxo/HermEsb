using System;
using HermEsb.Core.Messages;

namespace HermEsb.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBus : IPublisher
    {
        /// <summary>
        /// Envia la respuesta de una peticion específica
        /// </summary>
        /// <param name="replyMessage">Mensaje.</param>
        void Reply(IMessage replyMessage);

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