using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Messages;

namespace HermEsb.Core.Clustering
{
    public interface IClusterController
    {
        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();
        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();
        /// <summary>
        /// Gets the end point cluster input.
        /// </summary>
        /// <value>
        /// The end point cluster input.
        /// </value>
        IEndPoint EndPointClusterInput { get; }
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void SendMessage(IMessage message);
    }
}