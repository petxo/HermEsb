using System;
using HermEsb.Core.Ioc;

namespace HermEsb.Core.Handlers.Monitoring
{
    /// <summary>
    /// 
    /// </summary>
    public class MonitoringHandlerRepositoryFactory : HandlerRepositoryFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonitoringHandlerRepositoryFactory"/> class.
        /// </summary>
        /// <param name="contextManager">The context manager.</param>
        public MonitoringHandlerRepositoryFactory(IContextManager contextManager)
            : base(contextManager)
        {
        }

        /// <summary>
        /// Determines whether [is assignable to handler] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="genericArgument">The generic argument.</param>
        /// <returns>
        /// 	<c>true</c> if [is assignable to handler] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        protected override bool IsAssignableToHandler(Type type, Type genericArgument)
        {
            var handlerType = typeof(IMonitoringMessageHandler<>).MakeGenericType(genericArgument);
            return handlerType.IsAssignableFrom(type);
        }
    }
}