using HermEsb.Core.Communication.EndPoints;

namespace HermEsb.Core.Communication.Channels
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReceiverChannel : IStartable<EndPointStatus>, ISenderChannel
    {
        /// <summary>
        /// Occurs when [on received message].
        /// </summary>
        event EventReceiverEndPointHandler OnReceivedMessage;
    }
}