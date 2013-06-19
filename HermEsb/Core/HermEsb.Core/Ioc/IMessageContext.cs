using System;

namespace HermEsb.Core.Ioc
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMessageContext : IDisposable
    {
        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();

        /// <summary>
        /// Resolves the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        object Resolve(Type service);


        /// <summary>
        /// Gets the message info.
        /// </summary>
        /// <value>The message info.</value>
        IMessageInfo MessageInfo { get; }
    }
}