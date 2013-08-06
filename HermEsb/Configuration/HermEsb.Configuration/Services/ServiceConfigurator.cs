using HermEsb.Configuration.Monitoring;
using HermEsb.Core;
using HermEsb.Core.Clustering;
using HermEsb.Core.ErrorHandling;
using HermEsb.Core.Processors;
using HermEsb.Core.Service;

namespace HermEsb.Configuration.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceConfigurator : IConfigurator
    {
        private readonly HermEsbServiceConfig _hermEsbServiceConfig;
        private MonitorConfigurator _monitorConfigurator;

        private IController _controller;
        private IProcessor _processor;
        private Identification _identification;
        private IErrorHandlingController _errorHandlingController;


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceConfigurator"/> class.
        /// </summary>
        /// <param name="hermEsbServiceConfig">The HermEsb service config.</param>
        public ServiceConfigurator(HermEsbServiceConfig hermEsbServiceConfig)
        {
            _hermEsbServiceConfig = hermEsbServiceConfig;
        }

        /// <summary>
        /// Configures this instance.
        /// </summary>
        public void Configure()
        {
            LoadIdentification()
            .CreateMonitor()
            .CreateErrorHandlingController();
        }

        /// <summary>
        /// Creates the Service
        /// </summary>
        /// <returns></returns>
        public IService Create()
        {
            return CreateControlProcessor()
                .CreateServiceProcessor()
                .CreateService();
        }

        /// <summary>
        /// Creates the monitor.
        /// </summary>
        private ServiceConfigurator CreateMonitor()
        {
            if (_hermEsbServiceConfig.ControlProcessor.Monitor.ElementInformation.IsPresent)
            {
                _monitorConfigurator = new MonitorConfigurator(_hermEsbServiceConfig.ControlProcessor.Monitor, _identification);
            }
            return this;
        }

        /// <summary>
        /// Creates the service.
        /// </summary>
        /// <returns></returns>
        private IService CreateService()
        {
            var service = ServiceFactory.Create(_processor, _controller, _errorHandlingController, ClusterControllerFactory.NullController);
            if (_monitorConfigurator != null)
            {
                var monitor = _monitorConfigurator.Configure()
                                                  .Create(_controller);
                _controller.Monitor = monitor;
            }
            return service;
        }

        /// <summary>
        /// Loads the identification.
        /// </summary>
        /// <returns></returns>
        private ServiceConfigurator LoadIdentification()
        {
            _identification = new Identification
                                  {
                                      Id = _hermEsbServiceConfig.Identification.Id,
                                      Type = _hermEsbServiceConfig.Identification.Type
                                  };

            return this;
        }

        /// <summary>
        /// Creates the control processor.
        /// </summary>
        /// <returns></returns>
        private ServiceConfigurator CreateControlProcessor()
        {
            _controller = new ControlProcessorConfigurator(_hermEsbServiceConfig, _identification).CreateControlProcessor();
            return this;
        }

        /// <summary>
        /// Creates the service processor.
        /// </summary>
        /// <returns></returns>
        private ServiceConfigurator CreateServiceProcessor()
        {
            _processor =
                new ServiceProcessorConfigurator(_hermEsbServiceConfig.ServiceProcessor, _identification).
                    CreateServiceProcessor();

            return this;
        }

        /// <summary>
        /// Creates the monitor.
        /// </summary>
        private ServiceConfigurator CreateErrorHandlingController()
        {
            if (_hermEsbServiceConfig.ErrorHandlingController.ElementInformation.IsPresent)
            {
                var errorHandlingConfigurator = new ErrorHandlingControllerConfigurator(_hermEsbServiceConfig.ErrorHandlingController, _identification);
                _errorHandlingController = errorHandlingConfigurator.Create();
            }
            else
            {
                _errorHandlingController = ErrorHandlingControllerFactory.NullController;
            }
            return this;
        }

    }
}