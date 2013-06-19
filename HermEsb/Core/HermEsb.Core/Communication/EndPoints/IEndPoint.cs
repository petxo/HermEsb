using System;

namespace HermEsb.Core.Communication.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEndPoint : IDisposable
    {
        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <value>The URI.</value>
        Uri Uri { get; }

        /// <summary>
        /// Gets the transport.
        /// </summary>
        /// <value>The transport.</value>
        TransportType Transport { get;  }
    }
}