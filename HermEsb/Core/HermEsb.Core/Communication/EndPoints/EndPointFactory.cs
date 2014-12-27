using System;
using System.Collections.Generic;
#if !MONO
using HermEsb.Core.Communication.EndPoints.Msmq;
#endif
using HermEsb.Core.Communication.EndPoints.RabbitMq;

namespace HermEsb.Core.Communication.EndPoints
{
    ///<summary>
    ///</summary>
    public static class EndPointFactory
    {
        private static readonly IDictionary<TransportType, IEndPointFactory> Factories;

        /// <summary>
        /// Initializes the <see cref="EndPointFactory"/> class.
        /// </summary>
        static EndPointFactory()
        {
            Factories = new Dictionary<TransportType, IEndPointFactory>
                             {
								#if !MONO
                                 { TransportType.Msmq, new MsmqEndPointFactory() },
								#endif
                                 { TransportType.RabbitMq, new RabbitEndPointFactory() }
                             };
        }


        /// <summary>
        /// Creates the receiver end point.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="transportType">The supported transports.</param>
        /// <param name="numberOfParallelTasks">The number of parallel tasks.</param>
        /// <returns></returns>
        public static IReceiverEndPoint CreateReceiverEndPoint(Uri uri, TransportType transportType, int numberOfParallelTasks)
        {
            if (Factories.ContainsKey(transportType))
            {
                return Factories[transportType].CreateReceiver(uri, numberOfParallelTasks);
            }
            
            throw new NotImplementedException("Transporte no implementado");
        }

        /// <summary>
        /// Creates the sender end point.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="transportType">The supported transports.</param>
        /// <returns></returns>
        public static ISenderEndPoint CreateSenderEndPoint(Uri uri, TransportType transportType)
        {
            if (Factories.ContainsKey(transportType))
            {
                return Factories[transportType].CreateSender(uri);
            }

            throw new NotImplementedException("Transporte no implementado");
        }

    }
}