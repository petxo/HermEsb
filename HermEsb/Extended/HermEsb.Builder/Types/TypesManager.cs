using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using HermEsb.Logging;
using Mrwesb.Builder.Types.Repositories;

namespace Mrwesb.Builder.Types
{
    /// <summary>
    /// </summary>
    public class TypesManager : ITypesManager
    {
        private readonly List<Type> _processedTypes;
        private readonly ITypesRepository _typesRepository;
        private SpinLock _spinLockProcessedTypes;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TypesManager" /> class.
        /// </summary>
        /// <param name="typesRepository">The types repository.</param>
        public TypesManager(ITypesRepository typesRepository)
        {
            _processedTypes = new List<Type>();
            _spinLockProcessedTypes = new SpinLock();
            _typesRepository = typesRepository;
        }


        /// <summary>
        ///     Gets the constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public ConstructorInfo GetConstructor(Type type)
        {
            if (!_typesRepository.Exist(type))
            {
                _typesRepository.Add(type);
            }

            return _typesRepository.GetConstructor(type);
        }

        /// <summary>
        ///     Exists the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public bool Exist(Type type)
        {
            return _typesRepository.Exist(type);
        }

        /// <summary>
        ///     Gets the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public Type GetRelatedType(Type type)
        {
            return _typesRepository.GetRelatedType(type);
        }

        /// <summary>
        ///     Adds the type.
        /// </summary>
        /// <param name="types">The types.</param>
        public void AddType(IEnumerable<Type> types)
        {
            foreach (Type type in types)
            {
                AddType(type);
            }
        }

        /// <summary>
        ///     Adds the type.
        /// </summary>
        /// <param name="type">The type.</param>
        public void AddType(Type type)
        {
            if (type == null)
                return;

            if (_typesRepository.Exist(type)) return;

            if (type.IsGenericType && (typeof (IEnumerable<>).IsAssignableFrom(type.GetGenericTypeDefinition())
                                       || typeof (IList<>).IsAssignableFrom(type.GetGenericTypeDefinition())))
            {
                AddType(type.GetElementType());

                foreach (Type interfaceType in type.GetInterfaces())
                {
                    foreach (Type genericArgument in interfaceType.GetGenericArguments())
                    {
                        AddType(genericArgument);
                    }
                }

                return;
            }

            if (type.IsSimpleType())
                return;

            //var typeclousure = type;
            SpinWait.SpinUntil(() => !_spinLockProcessedTypes.IsHeld);
            if (_processedTypes.Contains(type))
                return;
            bool lockTaken = false;
            _spinLockProcessedTypes.Enter(ref lockTaken);
            if (lockTaken)
            {
                try
                {
                    if (!_processedTypes.Contains(type))
                        _processedTypes.Add(type);
                    else
                    {
                        return;
                    }
                }
                finally
                {
                    _spinLockProcessedTypes.Exit();
                }
            }

            AddPropertiesAndFileds(type);
            _typesRepository.Add(type);
        }

        /// <summary>
        ///     Adds the type of the interface to.
        /// </summary>
        /// <param name="interface">The @interface.</param>
        /// <param name="type">The type.</param>
        public void AddInterfaceToType(Type @interface, Type type)
        {
            if (@interface == null)
                return;

            if (@interface.IsSimpleType())
                return;

            if (_typesRepository.Exist(@interface)) return;

            _typesRepository.AddPair(@interface, type);
        }

        /// <summary>
        ///     Adds the properties and fileds.
        /// </summary>
        /// <param name="type">The type.</param>
        private void AddPropertiesAndFileds(Type type)
        {
            foreach (
                FieldInfo field in
                    type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy))
            {
                AddType(field.FieldType);
            }

            foreach (PropertyInfo prop in type.GetProperties())
            {
                try
                {
                    AddType(prop.PropertyType);
                }
                catch (Exception)
                {
                    LoggerManager.Instance.Error(string.Format("Error al crear el tipo de la propiedad: {0}", prop.Name));
                    throw;
                }
            }
        }
    }
}