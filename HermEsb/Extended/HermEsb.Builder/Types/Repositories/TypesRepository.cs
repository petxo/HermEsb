using System;
using System.Collections.Concurrent;
using System.Reflection;
using HermEsb.Logging;
using Mrwesb.Builder.Types.Builders;

namespace Mrwesb.Builder.Types.Repositories
{
    /// <summary>
    /// </summary>
    public class TypesRepository : ITypesRepository
    {
        private readonly ITypeBuilder _typeBuilder;
        private readonly ConcurrentDictionary<Type, ConstructorInfo> _typeConstructor;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TypesRepository" /> class.
        /// </summary>
        internal TypesRepository(ITypeBuilder typeBuilder)
        {
            _typeBuilder = typeBuilder;
            _typeConstructor = new ConcurrentDictionary<Type, ConstructorInfo>();
        }

        /// <summary>
        ///     Adds the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public void Add(Type type)
        {
            try
            {
                if (type.IsInterface)
                {
                    _typeConstructor.GetOrAdd(type, _typeBuilder.CreateTypeFrom(type).GetConstructor(Type.EmptyTypes));
                }
                else
                {
                    ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes) ?? type.GetConstructors()[0];
                    _typeConstructor.GetOrAdd(type, constructorInfo);
                }
            }
            catch (Exception)
            {
                LoggerManager.Instance.Error(string.Format("Error al crear el tipo: {0}", type.FullName));
                throw;
            }
        }

        /// <summary>
        ///     Exists the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public bool Exist(Type type)
        {
            return _typeConstructor.ContainsKey(type);
        }

        /// <summary>
        ///     Gets the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public Type GetRelatedType(Type type)
        {
            if (_typeConstructor.ContainsKey(type))
            {
                return _typeConstructor[type].ReflectedType;
            }
            return null;
        }

        /// <summary>
        ///     Gets the constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public ConstructorInfo GetConstructor(Type type)
        {
            ConstructorInfo constructorInfo;
            if (_typeConstructor.TryGetValue(type, out constructorInfo))
            {
                return constructorInfo;
            }
            return null;
        }

        /// <summary>
        ///     Adds the pair.
        /// </summary>
        /// <param name="interface">The @interface.</param>
        /// <param name="type">The type.</param>
        public void AddPair(Type @interface, Type type)
        {
            _typeConstructor.GetOrAdd(@interface, type.GetConstructor(Type.EmptyTypes));
        }
    }
}