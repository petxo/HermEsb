namespace HermEsb.Core.Clustering.Messages
{
    public class HeartBeatClusterMessage : IHeartBeatClusterMessage
    {
        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        /// <value>
        /// The identification.
        /// </value>
        public Identification Identification { get; set; }
    }
}