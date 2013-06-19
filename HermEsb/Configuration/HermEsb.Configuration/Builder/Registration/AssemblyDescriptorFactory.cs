using System.Collections.Generic;
using System.Reflection;

namespace HermEsb.Configuration.Builder.Registration
{
    /// <summary>
    /// 
    /// </summary>
    public static class AssemblyDescriptorFactory
    {
        /// <summary>
        /// Creates from this assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public static AssemblyDescriptor CreateFromAssembly(Assembly assembly)
        {
            return new AssemblyDescriptor(new List<Assembly> { assembly });
        }

        /// <summary>
        /// Creates from assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <returns></returns>
        public static AssemblyDescriptor CreateFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            return new AssemblyDescriptor(assemblies);
        }
    }
}