using System.Collections.Generic;
using HermEsb.Core.Gateways;
using HermEsb.Core.Processors.Router.Subscriptors;

namespace HermEsb.Core.Processors.Router.Outputs
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public interface IHierarchicalKeyEngine<TKey>
    {
        /// <summary>
        /// Gets the message senders.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        IEnumerable<IOutputGateway<string>> GetMessageSenders(string key);

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="service"></param>
        /// <param name="outputGateway">The output gateway.</param>
        void Add(SubscriptionKey key, Identification service, IOutputGateway<string> outputGateway);

        /// <summary>
        /// Removes the specified output gateway.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="service"></param>
        /// <param name="outputGateway">The output gateway.</param>
        void Remove(SubscriptionKey key, Identification service, IOutputGateway<string> outputGateway);

        /// <summary>
        /// Gets the keys.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SubscriptionKey> GetKeys();

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets the output gateways.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IOutputGateway<string>> GetOutputGateways();
    }
}