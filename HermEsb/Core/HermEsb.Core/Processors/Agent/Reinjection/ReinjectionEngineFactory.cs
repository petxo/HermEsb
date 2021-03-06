﻿using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;

namespace HermEsb.Core.Processors.Agent.Reinjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ReinjectionEngineFactory
    {

        /// <summary>
        /// Creates the direct engine.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="inputGateway">The input gateway.</param>
        /// <returns></returns>
        public static IReinjectionEngine CreateDefaultEngine<TMessage>(IInputGateway<TMessage, MessageHeader> inputGateway)
        {
            return new DirectReinjectionEngine<TMessage>(inputGateway);
        }

         
    }
}