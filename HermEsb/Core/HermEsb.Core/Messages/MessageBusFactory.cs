
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HermEsb.Core.Serialization;

namespace HermEsb.Core.Messages
{
    /// <summary>
    /// This factory create the bus messages
    /// </summary>
    public static class MessageBusFactory
    {
        private static readonly IDictionary<Type, string> KeyDictionary;
        private static SpinLock _spinLock;


        /// <summary>
        /// Initializes the <see cref="MessageBusFactory"/> class.
        /// </summary>
        static MessageBusFactory()
        {
            KeyDictionary = new Dictionary<Type, string>();
            _spinLock = new SpinLock();
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static string GetKey(Type type)
        {
            SpinWait.SpinUntil(() => !_spinLock.IsHeld);
            if (!KeyDictionary.ContainsKey(type))
            {
                CreateKey(type);
            }
            return KeyDictionary[type];
        }

        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="type">The type.</param>
        private static void CreateKey(Type type)
        {
            var lockTaken = false;
            _spinLock.Enter(ref lockTaken);
            if (lockTaken)
            {
                if (!KeyDictionary.ContainsKey(type))
                {
                    var @interface = GetInterface(type);
                    KeyDictionary.Add(type, string.Format("{0},{1}", @interface, @interface.Assembly.GetName().Name));
                }
                _spinLock.Exit();
            }
        }

        /// <summary>
        /// Gets the interface.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static Type GetInterface(Type type)
        {
#if !__MonoCS__
			var interfaces = type.GetInterfaces().ToList();
            var baseInterfaces = GetBaseInterfaces(type);
            interfaces = interfaces.Except(baseInterfaces).ToList();
#else
			var baseInterfaces = type.GetInterfaces();
			var interfaces = baseInterfaces.Except(baseInterfaces.SelectMany(iface => iface.GetInterfaces()));
#endif
            return interfaces.Any() ? interfaces.First() : type;
        }

        /// <summary>
        /// Gets the base interfaces.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static IEnumerable<Type> GetBaseInterfaces(Type type)
        {
            var baseInterfaces = new List<Type>();
            if (type.BaseType != null)
            {
                var interfaces = type.BaseType.GetInterfaces();
                if (interfaces.Any())
                    baseInterfaces.AddRange(interfaces.ToList());
            }
            return baseInterfaces;
        }

        /// <summary>
        /// Creates the specified identification.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="message">The message.</param>
        /// <param name="dataContractSerializer">The data contract serializer.</param>
        /// <returns></returns>
        public static MessageBus Create(Identification identification, IMessage message, IDataContractSerializer dataContractSerializer)
        {
            var messageBus = new MessageBus
                                 {
                                     Header =
                                         {
                                             CreatedAt = DateTime.UtcNow,
                                             IdentificationService =
                                                 {
                                                     Id = identification.Id,
                                                     Type = identification.Type
                                                 },
                                             BodyType = GetKey(message.GetType()),
                                             Type = MessageBusType.Generic,
                                             EncodingCodePage = dataContractSerializer.Encoding.CodePage
                                         },
                                     Body = dataContractSerializer.Serialize(message)
                                 };

            return messageBus;
        }
    }
}