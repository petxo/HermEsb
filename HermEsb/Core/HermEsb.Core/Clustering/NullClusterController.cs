using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Messages;

namespace HermEsb.Core.Clustering
{
    public class NullClusterController : IClusterController
    {
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            
        }

        /// <summary>
        /// Gets the end point cluster input.
        /// </summary>
        /// <value>
        /// The end point cluster input.
        /// </value>
        public IEndPoint EndPointClusterInput { get; private set; }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessage(IMessage message)
        {
            
        }
    }
}