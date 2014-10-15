using System;
using System.Collections.Generic;
using BteamMongoDB.Repository;
using HermEsb.Core;
using HermEsb.Core.Messages.Monitoring;
using HermEsb.Monitoring.Entities;
using HermEsb.Monitoring.Repositories;
using HermEsb.Monitoring.Specifications;
using MongoDB.Bson;

namespace HermEsb.Monitoring.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceInfoService : IServiceInfoService
    {
        private readonly IServiceInfoRepository _busRepository;
        private readonly IServiceInfoRepository _serviceRepository;

        /// <summary>
        /// Occurs when [service not exist].
        /// </summary>
        public event ServiceInfoEventHandler ServiceNotExist;


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceInfoService"/> class.
        /// </summary>
        /// <param name="busRepository">The bus repository.</param>
        /// <param name="serviceRepository">The service repository.</param>
        public ServiceInfoService(
            IServiceInfoRepository busRepository,
            IServiceInfoRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
            _busRepository = busRepository;
        }

        /// <summary>
        /// Assigns the heart beat.
        /// </summary>
        /// <param name="serviceInfoEntity">The heart beat heartBeatEntity.</param>
        public void ModifyServiceQueues(ServiceInfoEntity serviceInfoEntity)
        {
            if (!IdentitySpec.Instance.IsSatisfiedBy(serviceInfoEntity.Identification)) return;
            SaveQueueInfo(serviceInfoEntity);
        }

        /// <summary>
        /// Modifies the healt service.
        /// </summary>
        /// <param name="healthEntity">The health heartBeatEntity.</param>
        public void ModifyHealthInfo(HealthEntity healthEntity)
        {
            if (!IdentitySpec.Instance.IsSatisfiedBy(healthEntity.Identification)) return;
            SaveHealthInfo(healthEntity);
        }

        /// <summary>
        /// Modifies the status.
        /// </summary>
        /// <param name="heartBeatEntity">The heartBeatEntity.</param>
        public void ModifyStatus(HeartBeatEntity heartBeatEntity)
        {
            if (!IdentitySpec.Instance.IsSatisfiedBy(heartBeatEntity.Identification)) return;
            SaveStatus(heartBeatEntity);
        }

        /// <summary>
        /// Modifies the queue load.
        /// </summary>
        /// <param name="queueLoadEntity">The queue load entity.</param>
        public void ModifyQueueLoad(QueueLoadEntity queueLoadEntity)
        {
            if (!IdentitySpec.Instance.IsSatisfiedBy(queueLoadEntity.Identification)) return;
            SaveQueueLoad(queueLoadEntity);
        }

        /// <summary>
        /// Modifies the message types.
        /// </summary>
        /// <param name="messageTypesEntity">The message types entity.</param>
        public void ModifyMessageTypes(MessageTypesEntity messageTypesEntity)
        {
            if (!IdentitySpec.Instance.IsSatisfiedBy(messageTypesEntity.Identification)) return;
            SaveMessageTypes(messageTypesEntity);
        }

        /// <summary>
        /// Modifies the processing velocity.
        /// </summary>
        /// <param name="processingVelocityEntity">The processing velocity entity.</param>
        public void ModifyProcessingVelocity(ProcessingVelocityEntity processingVelocityEntity)
        {
            if (!IdentitySpec.Instance.IsSatisfiedBy(processingVelocityEntity.Identification)) return;
            SaveProcessingVelocity(processingVelocityEntity);
        }

        

        /// <summary>
        /// Saves the queue info.
        /// </summary>
        /// <param name="serviceInfoEntity">The service info entity.</param>
        private void SaveQueueInfo(ServiceInfoEntity serviceInfoEntity)
        {
            var repository = GetReopsitory(serviceInfoEntity);
            var serviceInfo = GetServiceInfo(serviceInfoEntity.Identification, repository);

            UpdateEntity(serviceInfo, serviceInfoEntity,
                (s =>
                     {
                         s.InputControlQueue = serviceInfoEntity.InputControlQueue;
                         s.InputControlQueueTransport = serviceInfoEntity.InputControlQueueTransport;
                         s.InputProcessorQueue = serviceInfoEntity.InputProcessorQueue;
                         s.InputProcessorQueueTransport = serviceInfoEntity.InputProcessorQueueTransport;
                     }));

            Save(serviceInfo, repository, bd =>
                                              {
                                                  bd.SetValue(be => be.InputControlQueue,serviceInfoEntity.InputControlQueue);
                                                  bd.SetValue(be => be.InputControlQueueTransport,serviceInfoEntity.InputControlQueueTransport);
                                                  bd.SetValue(be => be.InputProcessorQueue,serviceInfoEntity.InputProcessorQueue);
                                                  bd.SetValue(be => be.InputProcessorQueueTransport,serviceInfoEntity.InputProcessorQueueTransport);
                                              });

        }

        /// <summary>
        /// Saves the health info.
        /// </summary>
        /// <param name="healthEntity">The health entity.</param>
        private void SaveHealthInfo(HealthEntity healthEntity)
        {
            var repository = GetReopsitory(healthEntity);
            var serviceInfo = GetServiceInfo(healthEntity.Identification, repository);

            UpdateEntity(serviceInfo,healthEntity, (s => s.MemoryWorkingSet = healthEntity.MemoryWorkingSet));

            Save(serviceInfo, repository, bd => bd.SetValue(be => be.MemoryWorkingSet, serviceInfo.MemoryWorkingSet));
        }

        /// <summary>
        /// Saves the status.
        /// </summary>
        /// <param name="heartBeatEntity">The heart beat entity.</param>
        private void SaveStatus(HeartBeatEntity heartBeatEntity)
        {
            var repository = GetReopsitory(heartBeatEntity);
            var serviceInfo = GetOrCreateServiceInfo(heartBeatEntity.Identification, repository);

            UpdateEntity(serviceInfo, heartBeatEntity,(s => s.Status = heartBeatEntity.Status));
            Save(serviceInfo, repository, (bd => bd.SetValue(be => be.Status, serviceInfo.Status)));
        }

        /// <summary>
        /// Saves the message types.
        /// </summary>
        /// <param name="messageTypesEntity">The message types entity.</param>
        private void SaveMessageTypes(MessageTypesEntity messageTypesEntity)
        {
            var repository = GetReopsitory(messageTypesEntity);
            var serviceInfo = GetServiceInfo(messageTypesEntity.Identification, repository);

            UpdateEntity(serviceInfo, messageTypesEntity, (s => s.InputTypes = messageTypesEntity.MessageTypes));
            Save(serviceInfo, repository, (bd => bd.SetValue(be => be.InputTypes, serviceInfo.InputTypes)));
        }

        /// <summary>
        /// Modifies the transfer velocity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void ModifyTransferVelocity(TransferVelocityEntity entity)
        {
            var repository = GetReopsitory(entity);
            var serviceInfo = GetServiceInfo(entity.Identification, repository);

            UpdateEntity(serviceInfo, entity, (s => { }));

            Save(serviceInfo, repository, (bd =>
                                                {
                                                    bd.SetValue(be => be.InputTranfer, entity.Input);
                                                    bd.SetValue(be => be.OutputTransfer, entity.Output);
                                                }));
        }

        /// <summary>
        /// Modifies the output messages.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="type">The type.</param>
        /// <param name="messageTypes">The message types.</param>
        public void ModifyOutputMessages(Identification identification, ProcessorType type, IEnumerable<MessageType> messageTypes)
        {
            var repository = GetReopsitory(identification, type);
            var serviceInfo = GetServiceInfo(identification, repository);
            Save(serviceInfo, repository, (bd => bd.SetValue(be => be.OutputTypes, messageTypes)));
        }

        /// <summary>
        /// Saves the queue load.
        /// </summary>
        /// <param name="queueLoadEntity">The queue load entity.</param>
        private void SaveQueueLoad(QueueLoadEntity queueLoadEntity)
        {
            var repository = GetReopsitory(queueLoadEntity);
            var serviceInfo = GetServiceInfo(queueLoadEntity.Identification, repository);

            UpdateEntity(serviceInfo, queueLoadEntity, (s => s.TotalMessages = queueLoadEntity.TotalMessages));
            Save(serviceInfo, repository, (bd => bd.SetValue(be => be.TotalMessages, serviceInfo.TotalMessages)));
        }

        /// <summary>
        /// Saves the processing velocity.
        /// </summary>
        /// <param name="processingVelocityEntity">The processing velocity entity.</param>
        private void SaveProcessingVelocity(ProcessingVelocityEntity processingVelocityEntity)
        {
            var repository = GetReopsitory(processingVelocityEntity);
            var serviceInfo = GetServiceInfo(processingVelocityEntity.Identification, repository);

            UpdateEntity(serviceInfo, processingVelocityEntity, (s =>
                                                                     {
                                                                         s.NumberMessagesProcessed =processingVelocityEntity.NumberMessagesProcessed;
                                                                         s.Velocity = processingVelocityEntity.Velocity;
                                                                         s.Latency = processingVelocityEntity.Latency;
                                                                         s.PeakMaxLatency = processingVelocityEntity.PeakMaxLatency;
                                                                         s.PeakMinLatency = processingVelocityEntity.PeakMinLatency;
                                                                     }));

            Save(serviceInfo, repository, (bd =>
                                               {
                                                   bd.SetValue(be => be.NumberMessagesProcessed, serviceInfo.NumberMessagesProcessed);
                                                   bd.SetValue(be => be.Velocity, serviceInfo.Velocity);
                                                   bd.SetValue(be => be.Latency, serviceInfo.Latency);
                                                   bd.SetValue(be => be.PeakMaxLatency, serviceInfo.PeakMaxLatency);
                                                   bd.SetValue(be => be.PeakMinLatency, serviceInfo.PeakMinLatency);
                                               }));
        }

        /// <summary>
        /// Sets the value monitoring.
        /// </summary>
        /// <param name="bd">The bd.</param>
        /// <param name="serviceInfo">The service info.</param>
        private static void SetValueMonitoring(IModifierExpression<ServiceInfoEntity, ObjectId> bd, ServiceInfoEntity serviceInfo)
        {
            bd.SetValue(be => be.BusIdentification, serviceInfo.BusIdentification);
            bd.SetValue(be => be.Type, serviceInfo.Type);
            bd.SetValue(be => be.UtcTimeTakenSample, serviceInfo.UtcTimeTakenSample);
        }

        /// <summary>
        /// Updates the heartBeatEntity.
        /// </summary>
        /// <param name="monitoringEntity">The health heartBeatEntity.</param>
        /// <param name="serviceInfo">The service info.</param>
        /// <param name="updateValues">The update values.</param>
        private static void UpdateEntity(ServiceInfoEntity serviceInfo,MonitoringEntity monitoringEntity, Action<ServiceInfoEntity> updateValues)
        {
            if (serviceInfo == null) return;

            updateValues(serviceInfo);
            serviceInfo.UtcTimeTakenSample = monitoringEntity.UtcTimeTakenSample;
            serviceInfo.BusIdentification = monitoringEntity.BusIdentification;
            serviceInfo.Identification = monitoringEntity.Identification;
            serviceInfo.Type = monitoringEntity.Type;
        }

        /// <summary>
        /// Gets the reopsitory.
        /// </summary>
        /// <param name="entity">The heartBeatEntity.</param>
        /// <returns></returns>
        private IServiceInfoRepository GetReopsitory(MonitoringEntity entity)
        {
            return IsBus(entity) ? _busRepository : _serviceRepository;
        }

        /// <summary>
        /// Gets the reopsitory.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private IServiceInfoRepository GetReopsitory(Identification identification, ProcessorType type)
        {
            return IsBus(identification, type) ? _busRepository : _serviceRepository;
        }

        /// <summary>
        /// Determines whether the specified service info heartBeatEntity is bus.
        /// </summary>
        /// <param name="serviceInfoEntity">The service info heartBeatEntity.</param>
        /// <returns>
        /// 	<c>true</c> if the specified service info heartBeatEntity is bus; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsBus(MonitoringEntity serviceInfoEntity)
        {
            return IdentitySpec.Instance.IsSatisfiedBy(serviceInfoEntity.Identification) &&
                   BusTypeSpec.Instance.IsSatisfiedBy(serviceInfoEntity.Type);
        }

        /// <summary>
        /// Determines whether the specified identification is bus.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        /// 	<c>true</c> if the specified identification is bus; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsBus(Identification identification, ProcessorType type)
        {
            return IdentitySpec.Instance.IsSatisfiedBy(identification) &&
                   BusTypeSpec.Instance.IsSatisfiedBy(type);
        }


        /// <summary>
        /// Saves the heartBeatEntity.
        /// </summary>
        /// <param name="serviceInfoEntity">The service info heartBeatEntity.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="updateAction">The update action.</param>
        private static void Save(
            ServiceInfoEntity serviceInfoEntity, 
            IServiceInfoRepository repository,
            Action<IModifierExpression<ServiceInfoEntity, ObjectId>> updateAction)
        {
            if (serviceInfoEntity == null) return;

            if (serviceInfoEntity.Id == ObjectId.Empty)
                repository.Save(serviceInfoEntity);
            else
            {
                repository.Update(serviceInfoEntity.Id, bd =>
                                                            {
                                                                updateAction(bd);
                                                                SetValueMonitoring(bd, serviceInfoEntity);
                                                            });
            }
        }

        /// <summary>
        /// Gets the service info.
        /// </summary>
        /// <param name="serviceIdentification">The service info heartBeatEntity.</param>
        /// <param name="repository">The repository.</param>
        /// <returns></returns>
        private ServiceInfoEntity GetServiceInfo(Identification serviceIdentification, IServiceInfoRepository repository)
        {
            var infoEntity = repository.FindOne(new {Identification = new {serviceIdentification.Id, serviceIdentification.Type}});
            if (infoEntity == null)
            {
                InvokeServiceNotExist(new ServiceInfoEventHandlerArgs());
            }

            return infoEntity;
        }

        /// <summary>
        /// Gets the or create service info.
        /// </summary>
        /// <param name="serviceIdentification">The service identification.</param>
        /// <param name="repository">The repository.</param>
        /// <returns></returns>
        private static ServiceInfoEntity GetOrCreateServiceInfo(Identification serviceIdentification, IServiceInfoRepository repository)
        {
            return repository.FindOne(new { Identification = new { serviceIdentification.Id, serviceIdentification.Type } }) ??
                new ServiceInfoEntity
                {
                    Identification = new Identification
                    {
                        Id = serviceIdentification.Id,
                        Type = serviceIdentification.Type
                    }
                };
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _busRepository.Dispose();
                _serviceRepository.Dispose();
            }
        }

        /// <summary>
        /// Invokes the service not exist.
        /// </summary>
        /// <param name="args">The args.</param>
        private void InvokeServiceNotExist(ServiceInfoEventHandlerArgs args)
        {
            var handler = ServiceNotExist;
            if (handler != null) handler(this, args);
        }

    }
}
