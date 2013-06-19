using System;

namespace HermEsb.Core.Ioc
{
    /// <summary>
    /// 
    /// </summary>
    public interface IContextManager
    {
        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void AddComponent<T>() where T : class;

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="type">The type.</param>
        void AddComponent(Type type);

        /// <summary>
        /// Creates the new context.
        /// </summary>
        /// <returns></returns>
        IMessageContext CreateNewContext();

        /// <summary>
        /// Gets the current context.
        /// </summary>
        /// <value>The current context.</value>
        IMessageContext CurrentContext { get; }

        /// <summary>
        /// Determines whether [has current context].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [has current context]; otherwise, <c>false</c>.
        /// </returns>
        bool HasCurrentContext();
    }
}