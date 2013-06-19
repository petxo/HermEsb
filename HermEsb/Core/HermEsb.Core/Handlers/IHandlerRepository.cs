using System;
using System.Collections.Generic;

namespace HermEsb.Core.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHandlerRepository
    {
        /// <summary>
        /// Adds the handler.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="handlerType">Type of the handler.</param>
        void AddHandler(Type messageType, Type handlerType);

        /// <summary>
        /// Gets the handler.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        IEnumerable<Type> GetHandlersByMessage(Type messageType);


        /// <summary>
        /// Gets the type of the messages.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Type> GetMessageTypes();
    }
}