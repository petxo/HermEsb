using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HermEsb.Configuration.Builder.Registration
{
    /// <summary>
    /// 
    /// </summary>
    public static class RegisterTypes
    {
        /// <summary>
        /// Froms the this assembly.
        /// </summary>
        /// <returns></returns>
        public static AssemblyDescriptor FromThisAssembly()
        {
            return AssemblyDescriptorFactory.CreateFromAssembly(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Froms the assembly.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static AssemblyDescriptor FromAssembly(Type type)
        {
            return AssemblyDescriptorFactory.CreateFromAssembly(Assembly.GetAssembly(type));
        }

        /// <summary>
        /// Froms the assembly.
        /// </summary>
        /// <param name="assemblyFile">The assembly file.</param>
        /// <returns></returns>
        public static AssemblyDescriptor FromAssembly(string assemblyFile)
        {
            var assembly = GetAssembly(assemblyFile);

            return AssemblyDescriptorFactory.CreateFromAssembly(assembly);
        }

        /// <summary>
        /// Froms the assemblies.
        /// </summary>
        /// <param name="assemblyFiles">The assembly files.</param>
        /// <returns></returns>
        public static AssemblyDescriptor FromAssemblies(IEnumerable<string> assemblyFiles)
        {
            var assemblies = assemblyFiles.Select(GetAssembly).ToList();

            return AssemblyDescriptorFactory.CreateFromAssemblies(assemblies);
        }

        /// <summary>
        /// Gets the assembly.
        /// </summary>
        /// <param name="assemblyFile">The assembly file.</param>
        /// <returns></returns>
        private static Assembly GetAssembly(string assemblyFile)
        {
            var file = assemblyFile;

            if (File.Exists(string.Format("{0}.dll", assemblyFile)))
            {
                file = string.Format("{0}.dll", assemblyFile);
            }
            else if (File.Exists(string.Format("{0}.exe", assemblyFile)))
            {
                file = string.Format("{0}.exe", assemblyFile);
            }

            return Assembly.LoadFrom(file);
        }
    }
}