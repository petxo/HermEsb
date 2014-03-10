using System;
using HermEsb.Core.Handlers.Monitoring;
using HermEsb.Core.Listeners;
using HermEsb.Core.Messages;
using HermEsb.Monitoring.Repositories;
using HermEsb.Monitoring.Services;
using HermEsb.Monitoring.Translators;
using MongoDB.Bson;
using Mrwesb.Data.Mongo;

namespace HermEsb.Monitoring.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class MonitoringMessageHandler<TMessage,TEntity> : IMonitoringMessageHandler<TMessage>
        where TEntity : AbstractMongoEntity<ObjectId> 
        where TMessage : IMessage
    {
        private readonly IServiceInfoService _serviceInfoService;
        private readonly IMonitoringRepository<TEntity> _repository;
        private readonly ITranslator _translator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitoringMessageHandler&lt;TMessage, TEntity&gt;"/> class.
        /// </summary>
        /// <param name="serviceInfoService">The service info service.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="translator">The translator</param>
        protected MonitoringMessageHandler(
            IServiceInfoService serviceInfoService,
            IMonitoringRepository<TEntity> repository,
            ITranslator translator)
            : this(repository,translator)
        {
            _serviceInfoService = serviceInfoService;
            _serviceInfoService.ServiceNotExist += OnServiceNotExist;
        }

        /// <summary>
        /// Gets or sets the bus.
        /// </summary>
        /// <value>The bus.</value>
        public IListener Listener { get; set; }

        /// <summary>
        /// Called when [service not exist].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        private void OnServiceNotExist(object sender, ServiceInfoEventHandlerArgs args)
        {
            Listener.ProcessLater(TimeSpan.FromSeconds(2));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitoringMessageHandler&lt;TMessage, TEntity&gt;"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="translator">The translator</param>
        protected MonitoringMessageHandler(
            IMonitoringRepository<TEntity> repository,
            ITranslator translator)
            : this(translator)
        {
            _repository = repository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitoringMessageHandler&lt;TMessage, TEntity&gt;"/> class.
        /// </summary>
        /// <param name="translator">The translator.</param>
        protected MonitoringMessageHandler(
            ITranslator translator)
        {
            _translator = translator;
        }

        /// <summary>
        /// Gets the service info service.
        /// </summary>
        /// <value>The service info service.</value>
        public IServiceInfoService ServiceInfoService
        {
            get { return _serviceInfoService; }
        }

        /// <summary>
        /// Gets the translator.
        /// </summary>
        /// <value>The translator.</value>
        public ITranslator Translator
        {
            get { return _translator; }
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>The repository.</value>
        public IMonitoringRepository<TEntity> Repository
        {
            get { return _repository; }
        }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleMessage(TMessage message)
        {
            var entity = Translator.Translate<TMessage, TEntity>(message);
            if (Repository != null) Repository.Save(entity);
            HandledEntity(entity);
        }

        /// <summary>
        /// Handleds the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public abstract void HandledEntity(TEntity entity);

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
                
                if (_repository != null) _repository.Dispose();
                if (_serviceInfoService != null)
                {
                    _serviceInfoService.ServiceNotExist -= OnServiceNotExist;
                    _serviceInfoService.Dispose();
                }
            }
        }
    }
}