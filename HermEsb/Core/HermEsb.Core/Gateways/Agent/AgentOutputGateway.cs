using System;
using HermEsb.Core.Communication.EndPoints;
using HermEsb.Core.Ioc;
using HermEsb.Core.Messages;
using HermEsb.Core.Serialization;

namespace HermEsb.Core.Gateways.Agent
{
    /// <summary>
    /// 
    /// </summary>
    public class AgentOutputGateway: AbstractOutputGateway<IMessage>
    {
        private readonly Identification _identification;
        private int _defaultPriority;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentOutputGateway"/> class.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="senderEndPoints">The sender end points.</param>
        /// <param name="defaultPriority">The default priority.</param>
        internal AgentOutputGateway(Identification identification, ISenderEndPoint senderEndPoints, int defaultPriority = 0)
            : base(senderEndPoints)
        {
            _identification = identification;
            _defaultPriority = defaultPriority;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentOutputGateway"/> class.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="senderEndPoints">The sender end points.</param>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        /// <param name="defaultPriority">The default priority.</param>
        internal AgentOutputGateway(Identification identification, 
                                    ISenderEndPoint senderEndPoints, 
                                    IDataContractSerializer dataContractSerializer, 
                                    int defaultPriority = 0)
            : base(senderEndPoints, dataContractSerializer)
        {
            _identification = identification;
            _defaultPriority = defaultPriority;
        }

        /// <summary>
        /// Sends the specified MSG.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        /// <returns>Bytes Sent</returns>
        public override int Send(IMessage message, int priority)
        {
            try
            {
                var messageBus = MessageBusFactory.Create(_identification, message, DataContractSerializer);
                messageBus.Header.Priority = priority;

                var callerContext = CallerContextFactory.Create(_identification);
                messageBus.Header.CallStack.Push(callerContext);

                var serializedMessage = DataContractSerializer.Serialize(messageBus);
                SenderEndPoint.Send(serializedMessage, priority);
                InvokeSentMessage(messageBus.Header.BodyType, serializedMessage.Length);
                return serializedMessage.Length;
            }
            catch (Exception ex)
            {
                Logger.Error("OutputGateway - Error Send Message", ex);
                throw;
            }
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public override int Send(IMessage message)
        {
            return Send(message, _defaultPriority);
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageInfo">The message info.</param>
        /// <returns></returns>
        public override int Send(IMessage message, IMessageInfo messageInfo)
        {
            try
            {
                var messageBus = MessageBusFactory.Create(_identification, message, DataContractSerializer);

                //messageBus.Header = (MessageHeader) messageInfo.Header.Clone();
                messageBus.Header.Priority = messageInfo.Header.Priority;
                messageBus.Header.CallStack = messageInfo.Header.CallStack;
                messageBus.Header.CallContext = messageInfo.CurrentCallContext;

                if (!messageInfo.IsReply)
                {
                    var callerContext = CallerContextFactory.Create(_identification, messageInfo.CurrentSession);
                    messageBus.Header.CallStack.Push(callerContext);
                }
                else
                {
                    messageBus.Header.Type = MessageBusType.Reply;
                }

                var serializedMessage = DataContractSerializer.Serialize(messageBus);

                if (!messageInfo.IsReply)
                {
                    messageBus.Header.CallStack.Pop();
                }

                SenderEndPoint.Send(serializedMessage, messageInfo.Header.Priority);
                InvokeSentMessage(messageBus.Header.BodyType, serializedMessage.Length);
                return serializedMessage.Length;
            }
            catch (Exception ex)
            {
                Logger.Error("OutputGateway - Error Send Message", ex);
                throw;
            }
        }


        /// <summary>
        /// Gets the identification.
        /// </summary>
        /// <returns></returns>
        public Identification GetIdentification()
        {
            return _identification;
        }
    }
}