using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace HermEsb.Core.Serialization
{
    public class JsonDataContractSerializer : AbstractDataContractSerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDataContractSerializer"/> class.
        /// </summary>
        public JsonDataContractSerializer()
            : base(Encoding.UTF8)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDataContractSerializer"/> class.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        public JsonDataContractSerializer(Encoding encoding) : base(encoding)
        {
            
        }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <value>The type of the content.</value>
        public override string ContentType
        {
            get
            {
                return "application/json";
            }
        }

        /// <summary>
        /// Serializes the specified obj to serialize.
        /// </summary>
        /// <param name="objToSerialize">The obj to serialize.</param>
        /// <param name="encoding">The encoding.</param>
        public override string Serialize(object objToSerialize, Encoding encoding)
        {
            var dataContractJsonSerializer = new DataContractJsonSerializer(objToSerialize.GetType());
            using (var memoryStream = new MemoryStream())
            {
                dataContractJsonSerializer.WriteObject(memoryStream, objToSerialize);
                return encoding.GetString(memoryStream.GetBuffer(), 0, (int) memoryStream.Length);
            }
        }

        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public override object Deserialize(Stream stream, Type type)
        {
            var dataContractJsonSerializer = new DataContractJsonSerializer(type);
            return dataContractJsonSerializer.ReadObject(stream);
        }

    }
}