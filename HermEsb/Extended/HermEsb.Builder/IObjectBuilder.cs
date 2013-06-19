namespace Mrwesb.Builder
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface IObjectBuilder : ITypeRegister
    {
        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T CreateInstance<T>();

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T CreateInstance<T>(params object[] parameters);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        T CreateInstance<T>(Action<T> action);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        T CreateInstance<T>(Action<T> action, params object[] parameters);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        object CreateInstance(Type type);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        object CreateInstance(Type type, params object[] parameters);

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        Type GetRelatedType(Type type);
    }
}