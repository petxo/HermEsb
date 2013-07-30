using HermEsb.Core.Messages;

namespace HermEsb.Core.Clustering.Messages
{
    public interface IHeartBeatClusterMessage : IMessage
    {
        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        /// <value>
        /// The identification.
        /// </value>
        Identification Identification { get; set; }
    }
}