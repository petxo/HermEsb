namespace Mrwesb.Builder.Types.Builders
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// 
    /// </summary>
    public static class TypesBuildersFactory
    {
        /// <summary>
        /// Creates the dynamic type builder.
        /// </summary>
        /// <returns></returns>
        public static ITypeBuilder CreateDynamicTypeBuilder()
        {
            var moduleBuilder = CreateModuleBuilder(Guid.NewGuid().ToString());

            return new DynamicTypeBuilder(moduleBuilder);
        }

        /// <summary>
        /// Creates the module builder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private static ModuleBuilder CreateModuleBuilder(string name)
        {
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName(name),
                AssemblyBuilderAccess.Run
                );

            return assemblyBuilder.DefineDynamicModule(name);
        }
        
    }
}