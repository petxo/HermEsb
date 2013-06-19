using System;
using System.Collections.Generic;
using System.Reflection;

namespace HermEsb.Core.Builder.Types
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITypesManager
    {
        /// <summary>
        /// Gets the constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        ConstructorInfo GetConstructor(Type type);

        /// <summary>
        /// Adds the type.
        /// </summary>
        /// <param name="type">The type.</param>
        void AddType(Type type);

        /// <summary>
        /// Adds the type.
        /// </summary>
        /// <param name="types">The types.</param>
        void AddType(IEnumerable<Type> types);

        /// <summary>
        /// Adds the type of the interface to.
        /// </summary>
        /// <param name="interface">The @interface.</param>
        /// <param name="type">The type.</param>
        void AddInterfaceToType(Type @interface, Type type);

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        Type GetRelatedType(Type type);

        /// <summary>
        /// Exists the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        bool Exist(Type type);
    }
}