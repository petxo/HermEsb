using System.Runtime.Serialization;

namespace HermEsb.Core.ErrorHandling.Messages
{

    public class ErrorMessage : IErrorMessage
    {
        /// <summary>
        /// Gets or sets the service id.
        /// </summary>
        /// <value>The service id.</value>
        public Identification ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public ExceptionMessage Exception { get; set; }
    }
}