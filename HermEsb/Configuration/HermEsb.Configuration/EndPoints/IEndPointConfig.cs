using HermEsb.Core.Communication;

namespace HermEsb.Configuration.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEndPointConfig
    {
        /// <summary>
        /// Gets or sets the transport.
        /// </summary>
        /// <value>The transport.</value>
        TransportType Transport { get; }

        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <value>The URI.</value>
        string Uri { get; }
    }
}