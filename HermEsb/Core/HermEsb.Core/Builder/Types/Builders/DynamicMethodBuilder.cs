using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace HermEsb.Core.Builder.Types.Builders
{
    /// <summary>
    /// 
    /// </summary>
    public static class DynamicMethodBuilder
    {
        private const MethodAttributes ImplicitImplementation = MethodAttributes.Public
                                                                | MethodAttributes.Virtual
                                                                | MethodAttributes.HideBySig;

        /// <summary>
        /// Creates the method.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="methodInfo">The method info.</param>
        public static void CreateMethod(TypeBuilder typeBuilder, MethodInfo methodInfo)
        {
            var methodImpl = typeBuilder.DefineMethod(methodInfo.Name, ImplicitImplementation, methodInfo.ReturnType, 
                                            methodInfo.GetParameters().Select(info => info.ParameterType).ToArray());

            var ilGenerator = methodImpl.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ret);

        }
    }
}