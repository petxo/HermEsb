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

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        double Time { get; set; }
    }
}