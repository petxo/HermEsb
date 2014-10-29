using System.Runtime.Serialization;
using HermEsb.Core.Messages;

namespace HermEsb.Core.ErrorHandling.Messages
{
    public interface IErrorMessage : IMessage
    {
        /// <summary>
        /// Gets or sets the service id.
        /// </summary>
        /// <value>The service id.</value>
        Identification ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        ExceptionMessage Exception { get; set; }
    }
}