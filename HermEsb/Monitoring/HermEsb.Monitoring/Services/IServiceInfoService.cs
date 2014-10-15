using System;
using System.Collections.Generic;
using HermEsb.Core;
using HermEsb.Core.Messages.Monitoring;
using HermEsb.Monitoring.Entities;

namespace HermEsb.Monitoring.Services
{
    public interface IServiceInfoService : IDisposable
    {
        /// <summary>
        /// Modifies the service queues.
        /// </summary>
        /// <param name="serviceInfoEntity">The service info entity.</param>
        void ModifyServiceQueues(ServiceInfoEntity serviceInfoEntity);

        /// <summary>
        /// Modifies the health info.
        /// </summary>
        /// <param name="healthEntity">The health entity.</param>
        void ModifyHealthInfo(HealthEntity healthEntity);

        /// <summary>
        /// Modifies the status.
        /// </summary>
        /// <param name="heartBeatEntity">The heart beat entity.</param>
        void ModifyStatus(HeartBeatEntity heartBeatEntity);

        /// <summary>
        /// Modifies the transfer velocity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void ModifyTransferVelocity(TransferVelocityEntity entity);

        /// <summary>
        /// Modifies the queue load.
        /// </summary>
        /// <param name="queueLoadEntity">The queue load entity.</param>
        void ModifyQueueLoad(QueueLoadEntity queueLoadEntity);

        /// <summary>
        /// Modifies the message types.
        /// </summary>
        /// <param name="messageTypesEntity">The message types entity.</param>
        void ModifyMessageTypes(MessageTypesEntity messageTypesEntity);

        /// <summary>
        /// Modifies the processing velocity.
        /// </summary>
        /// <param name="processingVelocityEntity">The processing velocity entity.</param>
        void ModifyProcessingVelocity(ProcessingVelocityEntity processingVelocityEntity);

        /// <summary>
        /// Modifies the output messages.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="type">The type.</param>
        /// <param name="messageTypes">The message types.</param>
        void ModifyOutputMessages(Identification identification, ProcessorType type, IEnumerable<MessageType> messageTypes);

        /// <summary>
        /// Occurs when [service not exist].
        /// </summary>
        event ServiceInfoEventHandler ServiceNotExist;
    }
}