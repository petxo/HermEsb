using System;
using System.Reflection;
using HermEsb.Logging;
using System.Collections.Concurrent;

namespace HermEsb.Core.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class AssemblyHelper
    {
        private static readonly ConcurrentDictionary<string, Assembly> AssembliesCache;

        static AssemblyHelper()
        {
            AssembliesCache = new ConcurrentDictionary<string, Assembly>();
        }

        /// <summary>
        /// Gets the assembly.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns></returns>
        public static Assembly GetAssembly(string assemblyName)
        {
            Assembly assembly;
            if (!AssembliesCache.TryGetValue(assemblyName, out assembly))
            {
                LoggerManager.Instance.Debug(string.Format(@"Cargando el assembly {0}/{1}.dll", AppDomain.CurrentDomain.BaseDirectory, assemblyName));
                assembly = Assembly.LoadFrom(string.Format(@"{0}/{1}.dll", AppDomain.CurrentDomain.BaseDirectory, assemblyName));
                AssembliesCache.TryAdd(assemblyName, assembly);
            }
            return assembly;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns></returns>
        public static Type GetType(string typeFullName, string assemblyName)
        {
            try
            {
                return GetAssembly(assemblyName).GetType(typeFullName);
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error(string.Format("Error al cargar el Tipo del Assembly: {1},{0}", assemblyName, typeFullName), ex);
                throw;
            }
        }
    }
}