using System.IO;
using System.Messaging;
using System.Text;

namespace HermEsb.Core.Communication.Channels.Msmq
{
    /// <summary>
    /// 
    /// </summary>
    public class TextFormatter : IMessageFormatter
    {
        private readonly Encoding _encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextFormatter"/> class.
        /// </summary>
        public TextFormatter()
        {
            _encoding = Encoding.UTF8;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// When implemented in a class, determines whether the formatter can deserialize the contents of the message.
        /// </summary>
        /// <returns>
        /// true if the formatter can deserialize the message; otherwise, false.
        /// </returns>
        /// <param name="message">The <see cref="T:System.Messaging.Message"/> to inspect. </param>
        public bool CanRead(Message message)
        {
            return message.Body is string;
        }

        /// <summary>
        /// When implemented in a class, reads the contents from the given message and creates an object that contains data from the message.
        /// </summary>
        /// <returns>
        /// The deserialized message.
        /// </returns>
        /// <param name="message">The <see cref="T:System.Messaging.Message"/> to deserialize. </param>
        public object Read(Message message)
        {
            using (var streamReader = new StreamReader(message.BodyStream, _encoding))
            {
                return streamReader.ReadToEnd();
            }
        }

        /// <summary>
        /// When implemented in a class, serializes an object into the body of the message.
        /// </summary>
        /// <param name="message">The <see cref="T:System.Messaging.Message"/> that will contain the serialized object.</param>
        /// <param name="obj">The object to be serialized into the message. </param>
        public void Write(Message message, object obj)
        {
            message.BodyStream = new MemoryStream(_encoding.GetBytes((string)obj));
        }
    }
}