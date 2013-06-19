namespace Mrwesb.Builder.Types.Repositories
{
    using System;
    using System.Reflection;

    /// <summary>
    /// 
    /// </summary>
    public interface ITypesRepository
    {
        /// <summary>
        /// Adds the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        void Add(Type type);

        /// <summary>
        /// Exists the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        bool Exist(Type type);

        /// <summary>
        /// Gets the constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        ConstructorInfo GetConstructor(Type type);

        /// <summary>
        /// Adds the pair.
        /// </summary>
        /// <param name="interface">The @interface.</param>
        /// <param name="type">The type.</param>
        void AddPair(Type @interface, Type type);

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        Type GetRelatedType(Type type);
    }
}