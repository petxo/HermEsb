using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace HermEsb.Core.Serialization
{
    public class XmlSchemaSerializer : AbstractDataContractSerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlSchemaSerializer"/> class.
        /// </summary>
        public XmlSchemaSerializer()
            : base(Encoding.UTF8){ }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <value>The type of the content.</value>
        public override string ContentType
        {
            get
            {
                return "text/xml";
            }
        }

        /// <summary>
        /// Serializes the specified obj to serialize.
        /// </summary>
        /// <param name="objToSerialize">The obj to serialize.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public override string Serialize(object objToSerialize, Encoding encoding)
        {
            var serializer = new XmlSerializer(objToSerialize.GetType());
            using (var memoryStream = new MemoryStream())
            {
                serializer.Serialize(memoryStream, objToSerialize);
                return Encoding.UTF8.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
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
            var dataContractJsonSerializer = new XmlSerializer(type);
            return dataContractJsonSerializer.Deserialize(stream);
        }
    }
}