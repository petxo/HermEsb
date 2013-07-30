using HermEsb.Core.Clustering;
using HermEsb.Core.ErrorHandling;
using HermEsb.Core.Processors;
using HermEsb.Logging;

namespace HermEsb.Core.Service
{
    /// <summary>
    /// </summary>
    public static class ServiceFactory
    {
        /// <summary>
        /// Creates the specified processor.
        /// </summary>
        /// <param name="processor">The processor.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="errorHandlingController">The error handling controller.</param>
        /// <param name="clusterController">The cluster controller.</param>
        /// <returns></returns>
        public static IService Create(IProcessor processor, IController controller,
                                      IErrorHandlingController errorHandlingController,
                                        IClusterController clusterController)
        {
            var service = new Service(processor, controller, errorHandlingController, clusterController);
            InjectDependencies(service);
            return service;
        }

        /// <summary>
        ///     Injects the dependencies.
        /// </summary>
        /// <param name="service">The service.</param>
        private static void InjectDependencies(Service service)
        {
            service.Logger = LoggerManager.Instance;
        }
    }
}