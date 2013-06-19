using HermEsb.Core.Monitoring;

namespace HermEsb.Core.Communication.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReceiverEndPoint : IEndPoint, IStartable<EndPointStatus>, IMonitorableEndPoint
    {
        /// <summary>
        /// Occurs when [on received message].
        /// </summary>
        event EventReceiverEndPointHandler OnReceivedMessage;

        /// <summary>
        /// Purges this instance.
        /// </summary>
        void Purge();

        /// <summary>
        /// Reinjects the specified serialized message.
        /// </summary>
        /// <param name="serializedMessage">The serialized message.</param>
        /// <param name="priority"></param>
        void Reinject(string serializedMessage, int priority);
    }
}