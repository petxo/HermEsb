using System.Runtime.Serialization;

namespace HermEsb.Core.ErrorHandling.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionMessage
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the inner exception.
        /// </summary>
        /// <value>The inner exception.</value>
        public ExceptionMessage InnerException { get; set; }

        /// <summary>
        /// Gets or sets the call stack.
        /// </summary>
        /// <value>The call stack.</value>
        public string StackTrace { get; set; }
    }
}