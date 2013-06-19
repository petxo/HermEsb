using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace HermEsb.Core.Handlers.Dynamic
{
    /// <summary>
    /// 
    /// </summary>
    public class HandlerCreator
    {

        private readonly ModuleBuilder _moduleBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerCreator"/> class.
        /// </summary>
        internal HandlerCreator(ModuleBuilder moduleBuilder)
        {
            _moduleBuilder = moduleBuilder;
        }

        /// <summary>
        /// Creates from.
        /// </summary>
        /// <param name="handlerBaseType">Type of the handler base.</param>
        /// <param name="messageBaseType">Type of the message base.</param>
        /// <returns></returns>
        public IDictionary<Type, Type> CreateFrom(Type handlerBaseType, Type messageBaseType)
        {
            var dictionary = new Dictionary<Type, Type>();

            var messageTypes = DerivedTypesFinder.FromType(messageBaseType);
            foreach (var msgType in messageTypes)
            {
                //Creamos el base Type con el tipo concreto del mensaje
                // ya que el handler es generico
                var baseType = handlerBaseType.MakeGenericType(msgType);

                var dynamicHandler = CreateDynamicHandler(baseType, msgType, baseType.GetConstructors());
                dictionary.Add(msgType, dynamicHandler);
            }
            
            return dictionary;
        }

        /// <summary>
        /// Creates the dynamic handler.
        /// </summary>
        /// <param name="handlerBaseType">Type of the handler base.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="constructors">The constructors.</param>
        private Type CreateDynamicHandler(Type handlerBaseType, Type messageType, ConstructorInfo[] constructors)
        {
            var typeBuilder = _moduleBuilder.DefineType(
               GetHandlerName(messageType, handlerBaseType.Namespace),
               TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed,
               handlerBaseType
               );


            foreach (var baseCtor in constructors)
            {
                var parameters = baseCtor.GetParameters();
                var parameterTypes = parameters.Select(p => p.ParameterType).ToArray();
                var requiredCustomModifiers = parameters.Select(p => p.GetRequiredCustomModifiers()).ToArray();
                var optionalCustomModifiers = parameters.Select(p => p.GetOptionalCustomModifiers()).ToArray();

                var constructor = typeBuilder.DefineConstructor(MethodAttributes.Public | 
                        MethodAttributes.SpecialName | 
                        MethodAttributes.RTSpecialName,
                        CallingConventions.Standard,
                     parameterTypes, requiredCustomModifiers, optionalCustomModifiers);

                for (var i = 0; i < parameters.Length; ++i)
                {
                    var parameter = parameters[i];
                    var parameterBuilder = constructor.DefineParameter(i + 1, parameter.Attributes, parameter.Name);
                    if (((int)parameter.Attributes & (int)ParameterAttributes.HasDefault) != 0)
                    {
                        parameterBuilder.SetConstant(parameter.RawDefaultValue);
                    }

                    foreach (var attribute in BuildCustomAttributes(parameter.GetCustomAttributesData()))
                    {
                        parameterBuilder.SetCustomAttribute(attribute);
                    }
                }

                foreach (var attribute in BuildCustomAttributes(baseCtor.GetCustomAttributesData()))
                {
                    constructor.SetCustomAttribute(attribute);
                }

                var emitter = constructor.GetILGenerator();
                emitter.Emit(OpCodes.Nop);

                // Load `this` and call base constructor with arguments
                emitter.Emit(OpCodes.Ldarg_0);
                for (var i = 1; i <= parameters.Length; ++i)
                {
                    emitter.Emit(OpCodes.Ldarg, i);
                }
                emitter.Emit(OpCodes.Call, baseCtor);

                emitter.Emit(OpCodes.Ret);
            }

            return typeBuilder.CreateType();
        }

        private string GetHandlerName(Type type, string ns)
        {
            return string.Format("{1}.{0}Handler", type.Name, ns);
        }

        private static IEnumerable<CustomAttributeBuilder> BuildCustomAttributes(IEnumerable<CustomAttributeData> customAttributes)
        {
            return customAttributes.Select(attribute =>
            {
                var attributeArgs = attribute.ConstructorArguments.Select(a => a.Value).ToArray();
                var namedPropertyInfos = attribute.NamedArguments.Select(a => a.MemberInfo).OfType<PropertyInfo>().ToArray();
                var namedPropertyValues = attribute.NamedArguments.Where(a => a.MemberInfo is PropertyInfo).Select(a => a.TypedValue.Value).ToArray();
                var namedFieldInfos = attribute.NamedArguments.Select(a => a.MemberInfo).OfType<FieldInfo>().ToArray();
                var namedFieldValues = attribute.NamedArguments.Where(a => a.MemberInfo is FieldInfo).Select(a => a.TypedValue.Value).ToArray();
                return new CustomAttributeBuilder(attribute.Constructor, attributeArgs, namedPropertyInfos, namedPropertyValues, namedFieldInfos, namedFieldValues);
            }).ToArray();
        }
    }
}