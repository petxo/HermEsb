using System;
using System.IO;
using System.Text;

namespace HermEsb.Core.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractDataContractSerializer : IDataContractSerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractDataContractSerializer"/> class.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        protected AbstractDataContractSerializer(Encoding encoding)
        {
            Encoding = encoding;
        }

        #region Implementation of IDataContractSerializer
        
        /// <summary>
        /// Gets or sets the encoding.
        /// </summary>
        /// <value>The encoding.</value>
        public Encoding Encoding { get; set;}

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <value>The type of the content.</value>
        public abstract string ContentType { get; }

        /// <summary>
        /// Serializes the specified obj to serialize.
        /// </summary>
        /// <param name="objToSerialize">The obj to serialize.</param>
        /// <returns></returns>
        public virtual string Serialize(object objToSerialize)
        {
            return Serialize(objToSerialize, Encoding);
        }

        /// <summary>
        /// Serializes the specified obj to serialize.
        /// </summary>
        /// <param name="objToSerialize">The obj to serialize.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public abstract string Serialize(object objToSerialize, Encoding encoding);

        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public abstract object Deserialize(Stream stream, Type type);

        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public virtual TEntity Deserialize<TEntity>(Stream stream) where TEntity : class
        {
            return Deserialize(stream, typeof(TEntity)) as TEntity;
        }

        public virtual object Deserialize(string strObject, Encoding encoding, Type type)
        {
            using (var memoryStream = new MemoryStream(encoding.GetBytes(strObject)))
            {
                return Deserialize(memoryStream, type);
            }
        }


        /// <summary>
        /// Deserializes the specified STR objet.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="strObject">The STR objet.</param>
        /// <returns></returns>
        public virtual TEntity Deserialize<TEntity>(string strObject) where TEntity : class
        {
            return Deserialize<TEntity>(strObject, Encoding);
        }

        /// <summary>
        /// Deserializes the specified STR objet.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="strObject">The STR objet.</param>
        /// <param name="encoding">The encoding.</param>
        public virtual TEntity Deserialize<TEntity>(string strObject, Encoding encoding) where TEntity : class
        {
            using (var memoryStream = new MemoryStream(encoding.GetBytes(strObject)))
            {
                return Deserialize<TEntity>(memoryStream);
            }
        }

        #endregion
    }
}