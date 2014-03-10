using System;
using AutoMapper;
using HermEsb.Core.ErrorHandling.Messages;
using HermEsb.Core.Messages.Monitoring;
using HermEsb.Monitoring.Entities;
using HermEsb.Monitoring.Messages;

namespace HermEsb.Monitoring.Translators
{
    /// <summary>
    /// 
    /// </summary>
    public class Translator : BaseTranslator
    {
        static Translator()
        {
            Mapper.CreateMap<IHealthMessage, HealthEntity>();
            Mapper.CreateMap<IHeartBeatMessage, HeartBeatEntity>();
            Mapper.CreateMap<IMessageTypesMessage, MessageTypesEntity>();
            Mapper.CreateMap<IProcessingVelocityMessage, ProcessingVelocityEntity>();
            Mapper.CreateMap<IQueueLoadMessage, QueueLoadEntity>();
            Mapper.CreateMap<IHeartBeatMessage, ServiceInfoEntity>();
            Mapper.CreateMap<IInputQueueMessage, ServiceInfoEntity>();
            Mapper.CreateMap<ITransferVelocityMessage, TransferVelocityEntity>();
            Mapper.CreateMap<VelocityMessage, VelocityEntity>();
            Mapper.CreateMap<IErrorRouterMessage, ErrorRouterEntity>().ForMember(x => x.UtcSuccessAt, s => s.MapFrom(y => DateTime.UtcNow));
            Mapper.CreateMap<IErrorHandlerMessage, ErrorHandlerEntity>().ForMember(x => x.UtcSuccessAt, s => s.MapFrom( y => DateTime.UtcNow));
        }

    }
}