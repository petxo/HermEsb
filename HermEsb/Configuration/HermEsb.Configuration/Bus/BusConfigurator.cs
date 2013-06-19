using HermEsb.Configuration.Monitoring;
using HermEsb.Configuration.Services;
using HermEsb.Core;
using HermEsb.Core.ErrorHandling;
using HermEsb.Core.Monitoring;
using HermEsb.Core.Processors;
using HermEsb.Core.Service;
using HermEsb.Logging;

namespace HermEsb.Configuration.Bus
{
    /// <summary>
    /// 
    /// </summary>
    public class BusConfigurator : IConfigurator
    {
        private readonly HermEsbConfig _hermEsbConfig;
        private IController _controller;
        private MonitorConfigurator _errorHandlingConfigurator;
        private IErrorHandlingController _errorHandlingController;
        private Identification _identification;
        private IProcessor _processor;


        /// <summary>
        /// Initializes a new instance of the <see cref="BusConfigurator"/> class.
        /// </summary>
        /// <param name="hermEsbConfig">The herm esb config.</param>
        public BusConfigurator(HermEsbConfig hermEsbConfig)
        {
            _hermEsbConfig = hermEsbConfig;
        }

        /// <summary>
        ///     Creates the bus
        /// </summary>
        /// <returns></returns>
        public IService Create()
        {
            LoggerManager.Instance.Debug("Creando el servicio de BUS");
            return CreateControlProcessor()
                .CreateServiceProcessor()
                .CreateService();
        }

        /// <summary>
        ///     Configures this instance.
        /// </summary>
        public void Configure()
        {
            LoadIdentification()
                .CreateMonitor()
                .CreateErrorHandlingController();
        }

        /// <summary>
        ///     Creates the service.
        /// </summary>
        /// <returns></returns>
        private IService CreateService()
        {
            IService service = ServiceFactory.Create(_processor, _controller, _errorHandlingController);
            if (_errorHandlingConfigurator != null)
            {
                IMonitor monitor = _errorHandlingConfigurator.Create(_controller);
                _controller.Monitor = monitor;
            }
            return service;
        }

        /// <summary>
        ///     Creates the monitor.
        /// </summary>
        private BusConfigurator CreateMonitor()
        {
            if (_hermEsbConfig.RouterControlProcessor.Monitor.ElementInformation.IsPresent)
            {
                _errorHandlingConfigurator = new MonitorConfigurator(_hermEsbConfig.RouterControlProcessor.Monitor,
                                                                     _identification);
                _errorHandlingConfigurator.Configure();
            }
            return this;
        }

        /// <summary>
        ///     Creates the service processor.
        /// </summary>
        /// <returns></returns>
        private BusConfigurator CreateServiceProcessor()
        {
            _processor = new RouterProcessorConfigurator(_hermEsbConfig, _identification).CreateRouterProcessor();
            return this;
        }

        /// <summary>
        ///     Creates the control processor.
        /// </summary>
        /// <returns></returns>
        private BusConfigurator CreateControlProcessor()
        {
            var routerControlConfigurator = new RouterControlConfigurator(_hermEsbConfig, _identification);
            _controller = routerControlConfigurator.CreateControlProcessor();
            return this;
        }

        /// <summary>
        ///     Loads the identification.
        /// </summary>
        /// <returns></returns>
        private BusConfigurator LoadIdentification()
        {
            LoggerManager.Instance.Debug("Cargando la identificacion");
            _identification = new Identification
                {
                    Id = _hermEsbConfig.Identification.Id,
                    Type = _hermEsbConfig.Identification.Type
                };
            return this;
        }

        /// <summary>
        ///     Creates the monitor.
        /// </summary>
        private BusConfigurator CreateErrorHandlingController()
        {
            if (_hermEsbConfig.ErrorHandlingController.ElementInformation.IsPresent)
            {
                var errorHandlingConfigurator =
                    new ErrorHandlingControllerConfigurator(_hermEsbConfig.ErrorHandlingController, _identification);
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