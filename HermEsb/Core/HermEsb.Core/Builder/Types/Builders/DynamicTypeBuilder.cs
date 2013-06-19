using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;

namespace HermEsb.Core.Builder.Types.Builders
{
    /// <summary>
    /// Create dynamics types from specified interfaces
    /// </summary>
    public class DynamicTypeBuilder : ITypeBuilder
    {
        private readonly ModuleBuilder _moduleBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicTypeBuilder"/> class.
        /// </summary>
        /// <param name="moduleBuilder">The module builder.</param>
        internal DynamicTypeBuilder(ModuleBuilder moduleBuilder)
        {
            _moduleBuilder = moduleBuilder;
        }

        /// <summary>
        /// Creates the type from.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        public Type CreateTypeFrom(Type t)
        {
            TypeBuilder typeBuilder = _moduleBuilder.DefineType(
                GetNewTypeName(t),
                TypeAttributes.Serializable | TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed,
                typeof(object), new[]{t}
                );

            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);

            SetDataContractAttribute(typeBuilder);

            foreach (var prop in GetAllProperties(t))
            {
                DynamicPropertyBuilder.CreateProperty(typeBuilder, prop);
            }

            foreach (var methodInfo in GetAllMethods(t))
            {
                DynamicMethodBuilder.CreateMethod(typeBuilder, methodInfo);
            }

            typeBuilder.AddInterfaceImplementation(t);

            return typeBuilder.CreateType();
        }

        private static void SetDataContractAttribute(TypeBuilder typeBuilder)
        {
            Type dcaType = typeof (DataContractAttribute);
            var dataContractBuilder = new CustomAttributeBuilder(
                dcaType.GetConstructor(Type.EmptyTypes), new object[0]);

            typeBuilder.SetCustomAttribute(dataContractBuilder);
        }

        /// <summary>
        /// Returns all properties on the given type, going up the inheritance
        /// hierarchy.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static IEnumerable<PropertyInfo> GetAllProperties(Type t)
        {
            var result = new List<PropertyInfo>(t.GetProperties());
            foreach (var interfaceType in t.GetInterfaces())
                foreach (var prop in GetAllProperties(interfaceType))
                    if (!result.Contains(prop))
                        result.Add(prop);

            return result;
        }

        /// <summary>
        /// Gets all methods.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        private static IEnumerable<MethodInfo> GetAllMethods(Type t)
        {
            var result = new List<MethodInfo>(t.GetMethods());
            foreach (var interfaceType in t.GetInterfaces())
                foreach (var method in GetAllMethods(interfaceType))
                    if (!result.Contains(method))
                        result.Add(method);

            return result;
        }



        /// <summary>
        /// Generates a new full name for a type to be generated for the given type.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static string GetNewTypeName(Type t)
        {
            return t.Namespace + "." + t.Name + Suffix;
        }

        private const string Suffix = "Proxy";
        
    }
}