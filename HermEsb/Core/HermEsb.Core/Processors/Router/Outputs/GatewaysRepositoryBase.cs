using System;
using System.Collections.Generic;
using HermEsb.Core.Gateways;
using HermEsb.Core.Processors.Router.Subscriptors;

namespace HermEsb.Core.Processors.Router.Outputs
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class GatewaysRepositoryBase : IGatewaysRepository
    {
        private readonly IHierarchicalKeyEngine<Type> _hierarchicalKeyEngine;

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewaysRepositoryBase"/> class.
        /// </summary>
        /// <param name="hierarchicalKeyEngine">The hierarchical key engine.</param>
        protected GatewaysRepositoryBase(IHierarchicalKeyEngine<Type> hierarchicalKeyEngine)
        {
            _hierarchicalKeyEngine = hierarchicalKeyEngine;
        }

        /// <summary>
        /// Gets the hierarchical key engine.
        /// </summary>
        /// <value>The hierarchical key engine.</value>
        protected IHierarchicalKeyEngine<Type> HierarchicalKeyEngine
        {
            get { return _hierarchicalKeyEngine; }
        }

        /// <summary>
        /// Gets the message senders.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        public virtual IEnumerable<IOutputGateway<string>> GetMessageSenders(string typeName)
        {
            return _hierarchicalKeyEngine.GetMessageSenders(typeName);
        }

        /// <summary>
        /// Gets the message types.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<SubscriptionKey> GetMessageTypes();

        /// <summary>
        /// Adds the sender.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="service"></param>
        /// <param name="sender">The sender.</param>
        public abstract void AddSender(SubscriptionKey type, Identification service, IOutputGateway<string> sender);

        /// <summary>
        /// Removes the sender.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="service"></param>
        /// <param name="sender">The sender.</param>
        public abstract void RemoveSender(SubscriptionKey type, Identification service, IOutputGateway<string> sender);

        /// <summary>
        /// Inits this instance.
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected abstract void Dispose(bool disposing);

    }
}