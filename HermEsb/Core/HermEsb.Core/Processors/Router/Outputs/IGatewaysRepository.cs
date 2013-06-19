using System;
using System.Collections.Generic;
using HermEsb.Core.Gateways;
using HermEsb.Core.Processors.Router.Subscriptors;

namespace HermEsb.Core.Processors.Router.Outputs
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGatewaysRepository : IDisposable
    {
        /// <summary>
        /// Gets the message senders.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        IEnumerable<IOutputGateway<string>> GetMessageSenders(string typeName);

        /// <summary>
        /// Gets the message types.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SubscriptionKey> GetMessageTypes();

        /// <summary>
        /// Adds the sender.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="service"></param>
        /// <param name="sender">The sender.</param>
        void AddSender(SubscriptionKey type, Identification service, IOutputGateway<string> sender);

        /// <summary>
        /// Removes the sender.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="service"></param>
        /// <param name="sender">The sender.</param>
        void RemoveSender(SubscriptionKey type, Identification service, IOutputGateway<string> sender);

        /// <summary>
        /// Inits this instance.
        /// </summary>
        void Init();
    }
}