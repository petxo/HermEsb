using System;
using System.Collections.Generic;
using BteamMongoDB;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using HermEsb.Core.Ioc.WindsorContainer;
using HermEsb.Monitoring.Repositories;
using HermEsb.Monitoring.Services;
using HermEsb.Monitoring.Translators;
using MongoDB.Driver.Builders;

namespace HermEsb.Monitoring.Installer
{
    /// <summary>
    /// 
    /// </summary>
    public class MonitoringWindsorInstaller : IWindsorInstaller
    {
        private IList<string> _collectionList;
        public MonitoringWindsorInstaller()
        {
            _collectionList = new List<string> {
                "HealthEntity", 
                "HeartBeatEntity", 
                "QueueLoadEntity", 
                "ProcessingVelocityEntity", 
                "TransferVelocityEntity" 
            };
        }
        /// <summary>
        /// Installs the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(
                Component.For(typeof(IMonitoringRepository<>))
                         .ImplementedBy(typeof(MonitoringRepository<>))
                         .LifestyleScoped<MessageScopeAccesor>()
                         .DependsOn(Dependency.OnValue("connectionName", "MonitoringStatistics"))
                );

            container.Register(
                Component.For<IServiceInfoRepository>()
                         .ImplementedBy<ServiceInfoRepository>()
                         .LifestyleScoped<MessageScopeAccesor>()
                         .Named("BusInfoRepository")
                         .DependsOn(Dependency.OnValue("connectionName", "MonitoringStatistics"))
                         .DependsOn(Dependency.OnValue("collection", "BusInfo"))
                );

            container.Register(
                Component.For<IServiceInfoRepository>()
                            .ImplementedBy<ServiceInfoRepository>()
                            .LifestyleScoped<MessageScopeAccesor>()
                            .Named("ServiceInfoRepository")
                            .DependsOn(Dependency.OnValue("connectionName", "MonitoringStatistics"))
                            .DependsOn(Dependency.OnValue("collection", "ServiceInfo"))
                );


            container.Register(Component.For<ITranslator>()
                                   .ImplementedBy<Translator>().LifeStyle.Singleton.Named("Translator"));

            container.Register(
                Component.For<IServiceInfoService>()
                        .ImplementedBy<ServiceInfoService>()
                        .LifestyleScoped<MessageScopeAccesor>()
                        .Named("ServiceInfoService")
                        .DynamicParameters((k, d) =>
                                               {
                                                   d["busRepository"] =
                                                       k.Resolve<IServiceInfoRepository>("BusInfoRepository");
                                                   d["serviceRepository"] =
                                                       k.Resolve<IServiceInfoRepository>("ServiceInfoRepository");
                                               })
                );

            foreach (var collection in _collectionList)
            {
                MongoHelperProvider.Instance.GetHelper("MonitoringStatistics").Repository.GetCollection(collection)
                    .EnsureIndex(new IndexKeysBuilder().Descending("UtcTimeTakenSample"),
                        IndexOptions.SetBackground(true).SetSparse(true).SetTimeToLive(TimeSpan.FromDays(7)));

                MongoHelperProvider.Instance.GetHelper("MonitoringStatistics").Repository.GetCollection(collection)
                    .EnsureIndex(new IndexKeysBuilder().Ascending("Identification._id").Descending("UtcTimeTakenSample"),
                        IndexOptions.SetBackground(true).SetSparse(true));
            }

            MongoHelperProvider.Instance.GetHelper("MonitoringStatistics").Repository.GetCollection("ErrorHandlerEntity")
                .EnsureIndex(new IndexKeysBuilder().Descending("SolvedAt"),
                    IndexOptions.SetBackground(true).SetSparse(true).SetTimeToLive(TimeSpan.FromDays(3)));

            MongoHelperProvider.Instance.GetHelper("MonitoringStatistics").Repository.GetCollection("ErrorHandlerEntity")
                .EnsureIndex(new IndexKeysBuilder().Ascending("Status"),
                    IndexOptions.SetBackground(true).SetSparse(true));

            MongoHelperProvider.Instance.GetHelper("MonitoringStatistics").Repository.GetCollection("ErrorHandlerEntity")
                .EnsureIndex(new IndexKeysBuilder().Ascending("ServiceId._id").Descending("UtcSuccessAt"),
                    IndexOptions.SetBackground(true).SetSparse(true));

            MongoHelperProvider.Instance.GetHelper("MonitoringStatistics").Repository.GetCollection("ErrorHandlerEntity")
                .EnsureIndex(new IndexKeysBuilder().Descending("UtcSuccessAt"),
                    IndexOptions.SetBackground(true).SetSparse(true));
        }
    }
}