using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HermEsb.Core.Handlers.Dynamic;
using HermEsb.Core.Ioc;
using HermEsb.Core.Messages;

namespace HermEsb.Core.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class HandlerRepositoryFactory : IHandlerRepositoryFactory
    {

        /// <summary>
        /// Gets the factory.
        /// </summary>
        /// <typeparam name="THandlerRepositoryFactory">The type of the handler repository factory.</typeparam>
        /// <returns></returns>
        public static IHandlerRepositoryFactory GetFactory<THandlerRepositoryFactory>()
            where THandlerRepositoryFactory : HandlerRepositoryFactory
        {
            return GetFactory<THandlerRepositoryFactory>(ContextManager.Instance);
        }

        /// <summary>
        /// Gets the factory.
        /// </summary>
        /// <typeparam name="THandlerRepositoryFactory">The type of the handler repository factory.</typeparam>
        /// <param name="contextManager">The context manager.</param>
        /// <returns></returns>
        public static IHandlerRepositoryFactory GetFactory<THandlerRepositoryFactory>(IContextManager contextManager)
            where THandlerRepositoryFactory : HandlerRepositoryFactory
        {
            return Activator.CreateInstance(typeof(THandlerRepositoryFactory), contextManager) as IHandlerRepositoryFactory;
        }

        private readonly IContextManager _contextManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerRepositoryFactory"/> class.
        /// </summary>
        /// <param name="contextManager">The context manager.</param>
        protected HandlerRepositoryFactory(IContextManager contextManager)
        {
            _contextManager = contextManager;
        }

        /// <summary>
        /// Creates the specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <returns></returns>
        public HandlerRepository Create(IEnumerable<string> assemblies)
        {
            var handlerRepository = new HandlerRepository();

            foreach (var assemblyName in assemblies)
            {
                var assembly = Assembly.LoadFrom(string.Format(@"{0}/{1}",AppDomain.CurrentDomain.BaseDirectory, assemblyName));

                foreach (var type in assembly.GetTypes())
                {
                    var customAttributes = type.GetCustomAttributes(typeof(DynamicHandlerAttribute), true);
                    if (customAttributes.Length == 0)
                    {
                        var messageType = IsMessageHandler(type);
                        if (messageType != null)
                        {
                            _contextManager.AddComponent(type);
                            handlerRepository.AddHandler(messageType, type);
                        }
                    }
                    else
                    {
                        var dynamicHandler = (DynamicHandlerAttribute)customAttributes[0];
                        var dictionary = HandlerCreatorInstance.Instance.CreateFrom(type, dynamicHandler.BaseType);
                        foreach (var dynHandler in dictionary)
                        {
                            _contextManager.AddComponent(dynHandler.Value);
                            handlerRepository.AddHandler(dynHandler.Key, dynHandler.Value);
                        }
                    }
                }
            }

            return handlerRepository;
        }

        /// <summary>
        /// Determines whether [is message handler] [the specified t].
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        /// 	<c>true</c> if [is message handler] [the specified t]; otherwise, <c>false</c>.
        /// </returns>
        private Type IsMessageHandler(Type t)
        {
            if (t.IsAbstract)
                return null;

            return t.GetInterfaces().Select(GetMessageTypeFromMessageHandler).FirstOrDefault(messageType => messageType != null);
        }

        /// <summary>
        /// Gets the message type from message handler.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private Type GetMessageTypeFromMessageHandler(Type type)
        {
            //if (type.IsAbstract)
            //    return null;

            if (type.IsGenericType)
            {
                Type[] args = type.GetGenericArguments();
                if (args.Length != 1)
                    return null;

                if (!typeof(IMessage).IsAssignableFrom(args[0]))
                    return null;

                if (IsAssignableToHandler(type, args[0]))
                    return args[0];
            }

            return null;
        }

        /// <summary>
        /// Determines whether [is assignable to handler] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="genericArgument">The generic argument.</param>
        /// <returns>
        /// 	<c>true</c> if [is assignable to handler] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        protected abstract bool IsAssignableToHandler(Type type, Type genericArgument);
    }
}