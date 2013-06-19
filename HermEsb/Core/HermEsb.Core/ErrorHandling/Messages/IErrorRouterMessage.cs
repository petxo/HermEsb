using System.Runtime.Serialization;
using HermEsb.Core.Messages;

namespace HermEsb.Core.ErrorHandling.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public interface IErrorRouterMessage : IErrorMessage
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [DataMember]
        MessageBus Message { get; set; }
    }
}