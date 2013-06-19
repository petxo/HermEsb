using System.Runtime.Serialization;

namespace HermEsb.Core.ErrorHandling.Messages
{
    [DataContract]
    [KnownType(typeof(Identification))]
    public class ErrorMessage : IErrorMessage
    {
        /// <summary>
        /// Gets or sets the service id.
        /// </summary>
        /// <value>The service id.</value>
        [DataMember]
        public Identification ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        [DataMember]
        public ExceptionMessage Exception { get; set; }
    }
}