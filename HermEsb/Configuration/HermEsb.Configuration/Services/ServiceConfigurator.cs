using HermEsb.Configuration.Monitoring;
using HermEsb.Core;
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
        private readonly HermEsbServiceConfig _HermEsbServiceConfig;
        private MonitorConfigurator _monitorConfigurator;

        private IController _controller;
        private IProcessor _processor;
        private Identification _identification;
        private IErrorHandlingController _errorHandlingController;


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceConfigurator"/> class.
        /// </summary>
        /// <param name="HermEsbServiceConfig">The HermEsb service config.</param>
        public ServiceConfigurator(HermEsbServiceConfig HermEsbServiceConfig)
        {
            _HermEsbServiceConfig = HermEsbServiceConfig;
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
            if (_HermEsbServiceConfig.ControlProcessor.Monitor.ElementInformation.IsPresent)
            {
                _monitorConfigurator = new MonitorConfigurator(_HermEsbServiceConfig.ControlProcessor.Monitor, _identification);
            }
            return this;
        }

        /// <summary>
        /// Creates the service.
        /// </summary>
        /// <returns></returns>
        private IService CreateService()
        {
            var service = ServiceFactory.Create(_processor, _controller, _errorHandlingController);
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
                                      Id = _HermEsbServiceConfig.Identification.Id,
                                      Type = _HermEsbServiceConfig.Identification.Type
                                  };

            return this;
        }

        /// <summary>
        /// Creates the control processor.
        /// </summary>
        /// <returns></returns>
        private ServiceConfigurator CreateControlProcessor()
        {
            _controller = new ControlProcessorConfigurator(_HermEsbServiceConfig, _identification).CreateControlProcessor();
            return this;
        }

        /// <summary>
        /// Creates the service processor.
        /// </summary>
        /// <returns></returns>
        private ServiceConfigurator CreateServiceProcessor()
        {
            _processor =
                new ServiceProcessorConfigurator(_HermEsbServiceConfig.ServiceProcessor, _identification).
                    CreateServiceProcessor();

            return this;
        }

        /// <summary>
        /// Creates the monitor.
        /// </summary>
        private ServiceConfigurator CreateErrorHandlingController()
        {
            if (_HermEsbServiceConfig.ErrorHandlingController.ElementInformation.IsPresent)
            {
                var errorHandlingConfigurator = new ErrorHandlingControllerConfigurator(_HermEsbServiceConfig.ErrorHandlingController, _identification);
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