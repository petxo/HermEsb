using System;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Ioc;
using HermEsb.Core.Monitoring;

namespace HermEsb.Core.Gateways
{
    /// <summary>
    /// Functionality in a sending endpoint
    /// </summary>
    public interface IOutputGateway<in TMessage> : IMonitorableSenderGateway, IDisposable
    {
        /// <summary>
        /// Sends the specified custom Message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns>Bytes Sent</returns>
        int Send(TMessage msg);


        /// <summary>
        /// Sends the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="priority">The priority.</param>
        /// <returns>Bytes Sent</returns>
        int Send(TMessage msg, int priority);


        /// <summary>
        /// Sends the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="messageInfo">The message info.</param>
        /// <returns></returns>
        int Send(TMessage msg, IMessageInfo messageInfo);

        /// <summary>
        /// Gets the end point.
        /// </summary>
        /// <value>The end point.</value>
        IEndPoint EndPoint { get; }
    }
}
