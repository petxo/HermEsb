using System;
using System.Reflection;
using System.Reflection.Emit;

namespace HermEsb.Core.Handlers.Dynamic
{
    /// <summary>
    /// 
    /// </summary>
    public static class HandlerCreatorInstance
    {

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static HandlerCreator Instance { get; private set; }

        static HandlerCreatorInstance()
        {
            Instance = new HandlerCreator(CreateModuleBuilder(Guid.NewGuid().ToString()));
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