using HermEsb.Core.Communication.EndPoints;

namespace HermEsb.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReceiver
    {
        /// <summary>
        /// Gets the end point.
        /// </summary>
        /// <value>The end point.</value>
        IEndPoint ReceiverEndPoint { get; }
    }
}