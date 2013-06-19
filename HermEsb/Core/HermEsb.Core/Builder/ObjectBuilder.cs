using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using HermEsb.Core.Builder.Types;

namespace HermEsb.Core.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectBuilder : IObjectBuilder
    {
        private readonly ITypesManager _typesManager;
        private SpinLock _spinLock;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectBuilder"/> class.
        /// </summary>
        /// <param name="typesManager">The types repository.</param>
        internal ObjectBuilder(ITypesManager typesManager)
        {
            _typesManager = typesManager;
            _spinLock = new SpinLock();
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateInstance<T>()
        {
            return (T) CreateInstance(typeof(T));
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public T CreateInstance<T>(Action<T> action)
        {
            var instance = CreateInstance<T>();
            action.Invoke(instance);

            return instance;
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public object CreateInstance(Type type)
        {
            var constructor = _typesManager.GetConstructor(type);
            return constructor != null ? constructor.Invoke(null) : FormatterServices.GetUninitializedObject(type);
        }


        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateInstance<T>(params object[] parameters)
        {
            return (T)CreateInstance(typeof(T), parameters);
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public T CreateInstance<T>(Action<T> action, params object[] parameters)
        {
            var instance = CreateInstance<T>(parameters);
            action.Invoke(instance);

            return instance;
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public object CreateInstance(Type type, params object[] parameters)
        {
            var constructor = _typesManager.GetConstructor(type);
            return constructor != null ? constructor.Invoke(parameters) : FormatterServices.GetUninitializedObject(type);
        }

        /// <summary>
        /// Registers the specified types.
        /// </summary>
        /// <param name="types">The types.</param>
        public void Register(IEnumerable<Type> types)
        {
            _typesManager.AddType(types);
        }

        /// <summary>
        /// Registers the specified interface to type.
        /// </summary>
        /// <param name="interfaceToType">Type of the interface to.</param>
        public void Register(IEnumerable<KeyValuePair<Type, Type>> interfaceToType)
        {
            foreach (var pair in interfaceToType)
            {
                _typesManager.AddInterfaceToType(pair.Key, pair.Value);
            }

        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public Type GetRelatedType(Type type)
        {
            SpinWait.SpinUntil(() => !_spinLock.IsHeld);
            if (!_typesManager.Exist(type))
            {
                AddType(type);
            }
            return _typesManager.GetRelatedType(type);
        }

        private void AddType(Type type)
        {
            var lockTaken = false;
            _spinLock.Enter(ref lockTaken);
            if (lockTaken)
            {
                try
                {
                    if (!_typesManager.Exist(type))
                    {
                        _typesManager.AddType(type);
                    }
                }
                finally
                {
                    _spinLock.Exit();
                }
            }
        }
    }
}