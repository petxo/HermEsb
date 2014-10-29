using System.Runtime.Serialization;
using HermEsb.Core.Messages.Control;

namespace HermEsb.Core.Controller.Messages
{
    public interface IUnsubscriptionMessage : IControlMessage
    {
        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>The service.</value>

        Identification Service { get; set; }
    }
}