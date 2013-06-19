using System;
using System.Collections.Generic;
using System.Text;
using HermEsb.Core.Builder;
using HermEsb.Core.Serialization;
using HermEsb.Logging;

namespace HermEsb.Core.Messages.Builders
{
    /// <summary>
    /// </summary>
    public class MessageBuilder : IMessageBuilder
    {
        private readonly IObjectBuilder _objectBuilder;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageBuilder" /> class.
        /// </summary>
        /// <param name="objectBuilder">The object builder.</param>
        private MessageBuilder(IObjectBuilder objectBuilder)
        {
            _objectBuilder = objectBuilder;
        }

        /// <summary>
        ///     Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static MessageBuilder Instance { get; internal set; }

        /// <summary>
        ///     Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateInstance<T>() where T : IMessage
        {
            return _objectBuilder.CreateInstance<T>();
        }

        /// <summary>
        ///     Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public T CreateInstance<T>(Action<T> action) where T : IMessage
        {
            return _objectBuilder.CreateInstance(action);
        }

        /// <summary>
        ///     Registers the specified types.
        /// </summary>
        /// <param name="types">The types.</param>
        public void Register(IEnumerable<Type> types)
        {
            _objectBuilder.Register(types);
        }

        /// <summary>
        ///     Registers the specified interface to type.
        /// </summary>
        /// <param name="interfaceToType">Type of the interface to.</param>
        public void Register(IEnumerable<KeyValuePair<Type, Type>> interfaceToType)
        {
            _objectBuilder.Register(interfaceToType);
        }

        /// <summary>
        ///     Creates the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public object CreateInstance(Type type, Action<object> action)
        {
            object instance = _objectBuilder.CreateInstance(type);
            action.Invoke(instance);
            return instance;
        }

        /// <summary>
        ///     Creates the instance.
        /// </summary>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        /// <param name="type">The type.</param>
        /// <param name="serializedObject">The serialized object.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public object CreateInstance(IDataContractSerializer dataContractSerializer, Type type, string serializedObject,
                                     Encoding encoding)
        {
            Type relatedType = _objectBuilder.GetRelatedType(type);
            if (relatedType == null)
            {
                LoggerManager.Instance.Error(string.Format("Mierda el related Type es nulo {0}", type.FullName));
            }
            return dataContractSerializer.Deserialize(serializedObject, encoding, relatedType);
        }

        /// <summary>
        ///     Creates the specified object builder.
        /// </summary>
        /// <param name="objectBuilder">The object builder.</param>
        public static void Create(IObjectBuilder objectBuilder)
        {
            Instance = new MessageBuilder(objectBuilder);
        }
    }
}