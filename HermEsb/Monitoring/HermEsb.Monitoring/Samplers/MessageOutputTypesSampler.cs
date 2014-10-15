using System.Collections.Concurrent;
using System.Linq;
using HermEsb.Core.Messages.Monitoring;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Monitoring.Frequences;
using HermEsb.Monitoring.Messages;

namespace HermEsb.Monitoring.Samplers
{
    [SamplerFrequenceLevel(Frequence = FrequenceLevel.Lowest)]
    public class MessageOutputTypesSampler : Sampler
    {
        private readonly ConcurrentDictionary<MessageType, MessageType> _messageTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageOutputTypesSampler"/> class.
        /// </summary>
        /// <param name="monitorableProcessor">The monitorable processor.</param>
        public MessageOutputTypesSampler(IMonitorableProcessor monitorableProcessor)
            : base(monitorableProcessor)
        {
            _messageTypes = new ConcurrentDictionary<MessageType, MessageType>();
        }

        /// <summary>
        /// Samplers the task.
        /// </summary>
        protected override void SamplerTask()
        {
            var messageTypesMessage = MonitoringMessageFactory.Create<OutputTypesMessage>(MonitorableProcessor);
            messageTypesMessage.MessageTypes = _messageTypes.Values.ToList();

            MonitoringSender.Send(messageTypesMessage);
        }

        /// <summary>
        /// Configures the sampler.
        /// </summary>
        protected override void ConfigureSampler()
        {
            MonitorableProcessor.SenderGatewayChanged += (sender, args) => SetSenderEventGateway();
            SetSenderEventGateway();
        }

        /// <summary>
        /// Sets the sender event gateway.
        /// </summary>
        private void SetSenderEventGateway()
        {
            if (MonitorableProcessor.MonitorableSenderGateway != null)
                MonitorableProcessor.MonitorableSenderGateway.SentMessage += MonitorableSenderGatewaySentMessage;
        }

        /// <summary>
        /// Monitorables the sender gateway_ sent message.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        private void MonitorableSenderGatewaySentMessage(object sender, MessageGatewayEventHandlerArgs args)
        {
            var messageType = new MessageType
                                  {
                                      FullName = args.MessageType
                                  };

            _messageTypes.GetOrAdd(messageType, messageType);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            MonitorableProcessor.MonitorableSenderGateway.SentMessage -= MonitorableSenderGatewaySentMessage;
        }
    }
}