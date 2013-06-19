using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace HermEsb.Core.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlDataContractSerializer : AbstractDataContractSerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDataContractSerializer"/> class.
        /// </summary>
        public XmlDataContractSerializer()
            : base(Encoding.Default)
        {
            
        }

        #region Implementation of IDataContractSerializer
        
        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <value>The type of the content.</value>
        public override string ContentType
        {
            get
            {
                return "application/xml";
            }
        }

        /// <summary>
        /// Serializes the specified obj to serialize.
        /// </summary>
        /// <param name="objToSerialize">The obj to serialize.</param>
        /// <param name="encoding">The encoding.</param>
        public override string Serialize(object objToSerialize, Encoding encoding)
        {
            var dataContractJsonSerializer = new DataContractSerializer(objToSerialize.GetType());
            using (var memoryStream = new MemoryStream())
            {
                dataContractJsonSerializer.WriteObject(memoryStream, objToSerialize);
                return encoding.GetString(memoryStream.GetBuffer());
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
            var dataContractJsonSerializer = new DataContractSerializer(type);
            return dataContractJsonSerializer.ReadObject(stream);
        }

        #endregion
    }
}