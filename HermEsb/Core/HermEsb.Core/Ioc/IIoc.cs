using System;

namespace HermEsb.Core.Ioc
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIoc : IDisposable
    {
        /// <summary>
        /// Releases the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        void Release(object instance);

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
        /// Adds the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IIoc AddComponent<T>() where T : class;

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        IIoc AddComponent(Type type);

#if !__MonoCS__
        /// <summary>
        /// News the context.
        /// </summary>
        IDisposable CreateContext();
#else
		/// <summary>
		/// News the context.
		/// </summary>
		void CreateContext();
#endif

        /// <summary>
        /// Disposes the context.
        /// </summary>
        void DisposeContext();
    }
}