using System.Collections.Generic;
using HermEsb.Core.Gateways;
using HermEsb.Core.Processors.Router.Subscriptors;
using HermEsb.Logging;

namespace HermEsb.Core.Processors.Router.Outputs
{
    /// <summary>
    /// </summary>
    public class MemoryGatewaysRepository : GatewaysRepositoryBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MemoryGatewaysRepository" /> class.
        /// </summary>
        public MemoryGatewaysRepository() : base(new TypeHierarchicalKeyEngine())
        {
        }


        /// <summary>
        ///     Gets the message senders.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        public override IEnumerable<IOutputGateway<byte[]>> GetMessageSenders(string typeName)
        {
            return HierarchicalKeyEngine.GetMessageSenders(typeName);
        }

        /// <summary>
        ///     Gets the message types.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<SubscriptionKey> GetMessageTypes()
        {
            return HierarchicalKeyEngine.GetKeys();
        }

        /// <summary>
        ///     Adds the sender.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="service"></param>
        /// <param name="sender">The sender.</param>
        public override void AddSender(SubscriptionKey type, Identification service, IOutputGateway<byte[]> sender)
        {
            LoggerManager.Instance.Debug(string.Format("Add Sender: {0}", type.Key));
            HierarchicalKeyEngine.Add(type, service, sender);
        }

        /// <summary>
        ///     Removes the sender.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="service"></param>
        /// <param name="sender">The sender.</param>
        public override void RemoveSender(SubscriptionKey type, Identification service, IOutputGateway<byte[]> sender)
        {
            HierarchicalKeyEngine.Remove(type, service, sender);
        }

        /// <summary>
        ///     Inits this instance.
        /// </summary>
        public override void Init()
        {
            HierarchicalKeyEngine.Clear();
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var output in HierarchicalKeyEngine.GetOutputGateways())
                {
                    output.Dispose();
                }
            }
        }
    }
}