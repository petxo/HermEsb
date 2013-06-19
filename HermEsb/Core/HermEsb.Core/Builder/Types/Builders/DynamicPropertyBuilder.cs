using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;

namespace HermEsb.Core.Builder.Types.Builders
{
    /// <summary>
    /// Create a specified property inside TypeBuilder
    /// </summary>
    public static class DynamicPropertyBuilder
    {
        private const MethodAttributes GetSetAttributes = MethodAttributes.Public 
                                                        | MethodAttributes.SpecialName 
                                                        | MethodAttributes.HideBySig 
                                                        | MethodAttributes.Final 
                                                        | MethodAttributes.Virtual 
                                                        | MethodAttributes.VtableLayoutMask;
        /// <summary>
        /// Creates the property.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="prop">The prop.</param>
        public static void CreateProperty(TypeBuilder typeBuilder, PropertyInfo prop)
        {
            var fieldBuilder = typeBuilder.DefineField(
                "field_" + prop.Name,
                prop.PropertyType,
                FieldAttributes.Private);

            var propBuilder = typeBuilder.DefineProperty(
                prop.Name,
                prop.Attributes | PropertyAttributes.HasDefault,
                prop.PropertyType,
                null);

            SetDataMemberAttribute(propBuilder);

            CreateGetMethod(typeBuilder, prop, fieldBuilder, propBuilder);
            CreateSetMethod(typeBuilder, prop, fieldBuilder, propBuilder);
        }

        private static void SetDataMemberAttribute(PropertyBuilder typeBuilder)
        {
            var dmbType = typeof(DataMemberAttribute);
            var dataMemberBuilder = new CustomAttributeBuilder(
                dmbType.GetConstructor(Type.EmptyTypes), new object[0]);

            typeBuilder.SetCustomAttribute(dataMemberBuilder);
        }

        /// <summary>
        /// Creates the set method.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="prop">The prop.</param>
        /// <param name="fieldBuilder">The field builder.</param>
        /// <param name="propBuilder">The prop builder.</param>
        private static void CreateSetMethod(TypeBuilder typeBuilder, PropertyInfo prop, FieldInfo fieldBuilder, PropertyBuilder propBuilder)
        {
            var setMethodBuilder = typeBuilder.DefineMethod(
                "set_" + prop.Name,
                GetSetAttributes,
                null,
                new[] { prop.PropertyType });

            var setIl = setMethodBuilder.GetILGenerator();
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);
            setIl.Emit(OpCodes.Ret);

            propBuilder.SetSetMethod(setMethodBuilder);
        }

        /// <summary>
        /// Creates the get method.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="prop">The prop.</param>
        /// <param name="fieldBuilder">The field builder.</param>
        /// <param name="propBuilder">The prop builder.</param>
        private static void CreateGetMethod(TypeBuilder typeBuilder, PropertyInfo prop, FieldInfo fieldBuilder, PropertyBuilder propBuilder)
        {
            var getMethodBuilder = typeBuilder.DefineMethod(
                "get_" + prop.Name,
                GetSetAttributes,
                prop.PropertyType,
                Type.EmptyTypes);

            var getIl = getMethodBuilder.GetILGenerator();
            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);
            propBuilder.SetGetMethod(getMethodBuilder);
        }
    }
}