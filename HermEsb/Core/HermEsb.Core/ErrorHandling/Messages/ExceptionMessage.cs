using System.Runtime.Serialization;

namespace HermEsb.Core.ErrorHandling.Messages
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ExceptionMessage
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the inner exception.
        /// </summary>
        /// <value>The inner exception.</value>
        [DataMember]
        public ExceptionMessage InnerException { get; set; }

        /// <summary>
        /// Gets or sets the call stack.
        /// </summary>
        /// <value>The call stack.</value>
        [DataMember]
        public string StackTrace { get; set; }
    }
}