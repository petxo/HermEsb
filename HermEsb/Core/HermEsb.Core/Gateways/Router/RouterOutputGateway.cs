using System;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Ioc;
using HermEsb.Core.Serialization;

namespace HermEsb.Core.Gateways.Router
{
    /// <summary>
    /// 
    /// </summary>
    public class RouterOutputGateway : AbstractOutputGateway<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouterOutputGateway"/> class.
        /// </summary>
        /// <param name="senderEndPoint">The sender end points.</param>
        public RouterOutputGateway(ISenderEndPoint senderEndPoint)
            : base(senderEndPoint)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouterOutputGateway"/> class.
        /// </summary>
        /// <param name="senderEndPoint">The sender end points.</param>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        public RouterOutputGateway(ISenderEndPoint senderEndPoint, IDataContractSerializer dataContractSerializer)
            : base(senderEndPoint, dataContractSerializer)
        {
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public override int Send(string message)
        {
            return Send(message, 0);
        }

        /// <summary>
        /// Sends the specified MSG.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        public override int Send(string message, int priority)
        {
            SenderEndPoint.Send(message, priority);
            return message.Length;
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageInfo">The message info.</param>
        /// <returns></returns>
        public override int Send(string message, IMessageInfo messageInfo)
        {
            try
            {
                SenderEndPoint.Send(message, messageInfo.Header.Priority);
                return message.Length;
            }
            catch (Exception ex)
            {
                Logger.Fatal("Error Send Message", ex);
                return 0;
            }
        }
    }
}