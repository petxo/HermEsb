using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.ErrorHandling;
using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;

namespace HermEsb.Core.Clustering
{
    /// <summary>
    /// 
    /// </summary>
    public static class ClusterControllerFactory
    {
        /// <summary>
        /// Initializes the <see cref="ErrorHandlingControllerFactory"/> class.
        /// </summary>
        static ClusterControllerFactory()
        {
            NullController = new NullClusterController();
        }

        /// <summary>
        /// Creates the specified output gateway.
        /// </summary>
        /// <param name="outputGateway">The output gateway.</param>
        /// <param name="identification">The identification.</param>
        /// <param name="endPointInfo">The end point info.</param>
        /// <returns></returns>
        public static IClusterController Create(IOutputGateway<IMessage> outputGateway, Identification identification, IEndPoint endPointInfo)
        {
            var actionClusterProcessor = new ActionClusterProcessor(identification, outputGateway);
            return new ClusterController(actionClusterProcessor, outputGateway, endPointInfo);
        }


        /// <summary>
        /// Gets or sets the null controller.
        /// </summary>
        /// <value>The null controller.</value>
        public static IClusterController NullController { get; private set; }
    }
}