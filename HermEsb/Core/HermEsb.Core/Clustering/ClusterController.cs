using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;

namespace HermEsb.Core.Clustering
{
    public class ClusterController : IClusterController
    {
        private readonly IActionClusterProcessor _actionClusterProcessor;
        private readonly IOutputGateway<IMessage> _outputGateway;
        private readonly IEndPoint _endPointInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClusterController" /> class.
        /// </summary>
        /// <param name="actionClusterProcessor">The action cluster processor.</param>
        /// <param name="endPointInfo">The end point info.</param>
        public ClusterController(IActionClusterProcessor actionClusterProcessor, IOutputGateway<IMessage> outputGateway, IEndPoint endPointInfo)
        {
            _actionClusterProcessor = actionClusterProcessor;
            _outputGateway = outputGateway;
            _endPointInfo = endPointInfo;
        }

        /// <summary>
        /// Gets the end point cluster input.
        /// </summary>
        /// <value>
        /// The end point cluster input.
        /// </value>
        public IEndPoint EndPointClusterInput
        {
            get { return _endPointInfo; }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            _actionClusterProcessor.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            _actionClusterProcessor.Stop();
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessage(IMessage message)
        {
            _outputGateway.Send(message);
        }
    }
}