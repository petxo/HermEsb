using System.Runtime.Serialization;

namespace HermEsb.Core.Controller.Messages
{
    /// <summary>
    /// 
    /// </summary>

    public class UnsubscriptionMessage : IUnsubscriptionMessage
    {
        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>The service.</value>

        public Identification Service { get; set; }
    }
}