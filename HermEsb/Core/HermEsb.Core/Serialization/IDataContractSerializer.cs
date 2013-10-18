using System;
using System.IO;
using System.Text;

namespace HermEsb.Core.Serialization
{
    public interface IDataContractSerializer
    {
        /// <summary>
        /// Gets or sets the encoding.
        /// </summary>
        /// <value>The encoding.</value>
        Encoding Encoding { get; set; }

        /// <summary>
        /// Serializes the specified obj to serialize.
        /// </summary>
        /// <param name="objToSerialize">The obj to serialize.</param>
        /// <returns></returns>
        string Serialize(object objToSerialize);

        /// <summary>
        /// Serializes the specified obj to serialize.
        /// </summary>
        /// <param name="objToSerialize">The obj to serialize.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        string Serialize(object objToSerialize, Encoding encoding);


        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        object Deserialize(Stream stream, Type type);

        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        TEntity Deserialize<TEntity>(Stream stream) where TEntity : class;

        /// <summary>
        /// Deserializes the specified STR objet.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="strObjet">The STR objet.</param>
        /// <returns></returns>
        TEntity Deserialize<TEntity>(string strObjet) where TEntity : class;

        /// <summary>
        /// Deserializes the specified STR objet.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="strObjet">The STR objet.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        TEntity Deserialize<TEntity>(string strObjet, Encoding encoding) where TEntity : class;

        /// <summary>
        /// Deserializes the specified STR object.
        /// </summary>
        /// <param name="strObject">The STR object.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        object Deserialize(string strObject, Encoding encoding, Type type);

        /// <summary>
        /// Deserializes the specified STR objet.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        TEntity Deserialize<TEntity>(byte[] obj) where TEntity : class;
    }
}