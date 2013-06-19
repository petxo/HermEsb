using System;
using System.Runtime.Serialization;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public class RabbitReconnectionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitReconnectionException"/> class.
        /// </summary>
        public RabbitReconnectionException()
            : base("Se ha alcanzado el maximo de reconexiones para el end point")
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitReconnectionException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public RabbitReconnectionException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitReconnectionException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public RabbitReconnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitReconnectionException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        protected RabbitReconnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}