using System;
using System.Text;
using HermEsb.Core.Builder;
using HermEsb.Core.Serialization;

namespace HermEsb.Core.Messages.Builders
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMessageBuilder : ITypeRegister
    {
        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T CreateInstance<T>() where T : IMessage;

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        T CreateInstance<T>(Action<T> action) where T : IMessage;

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        object CreateInstance(Type type, Action<object> action);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        /// <param name="type">The type.</param>
        /// <param name="serializedObject">The serialized object.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        object CreateInstance(IDataContractSerializer dataContractSerializer, Type type, string serializedObject, Encoding encoding);
    }
}