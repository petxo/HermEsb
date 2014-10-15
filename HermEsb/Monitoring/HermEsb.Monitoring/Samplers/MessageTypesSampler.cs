using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using HermEsb.Core.Messages.Monitoring;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Monitoring.Frequences;
using HermEsb.Core.Processors.Router.Subscriptors;

namespace HermEsb.Monitoring.Samplers
{
    [SamplerFrequenceLevel(Frequence = FrequenceLevel.Lowest)]
    public class MessageTypesSampler : Sampler, IPublisherSampler
    {
        private readonly ConcurrentDictionary<SubscriptionKey, MessageType> _bagTypes;
        private readonly IMonitorableRouter _monitorableRouter;
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTypesSampler"/> class.
        /// </summary>
        /// <param name="monitorableProcessor">The monitorable processor.</param>
        public MessageTypesSampler(IMonitorableProcessor monitorableProcessor) : base(monitorableProcessor)
        {
            _bagTypes = new ConcurrentDictionary<SubscriptionKey, MessageType>();
            _monitorableRouter = monitorableProcessor as IMonitorableRouter;
        }

        /// <summary>
        /// Samplers the task.
        /// </summary>
        protected override void SamplerTask()
        {
            if (_monitorableRouter == null) return;

            var types = _monitorableRouter.GetMessageTypes();
            UpdateBag(types);

            var messageTypes = MonitoringMessageFactory.Create<MessageTypesMessage>(MonitorableProcessor);
            messageTypes.MessageTypes = _bagTypes.Values.ToList();

            MonitoringSender.Send(messageTypes);
        }

        /// <summary>
        /// Updates the bag.
        /// </summary>
        /// <param name="types">The types.</param>
        private void UpdateBag(IEnumerable<SubscriptionKey> types)
        {
            types.AsParallel().ForAll(x =>
                                        {
                                            if (!_bagTypes.ContainsKey(x))
                                            {
                                                _bagTypes.TryAdd(x,
                                                                 new MessageType
                                                                     { FullName = x.Key});
                                            }
                                        });
        }

        /// <summary>
        /// Configures the sampler.
        /// </summary>
        protected override void ConfigureSampler() { }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing) { }
    }
}
