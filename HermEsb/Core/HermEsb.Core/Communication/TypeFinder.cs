using System;
using System.Collections.Concurrent;
using HermEsb.Core.Helpers;

namespace HermEsb.Core.Communication
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeFinder
    {
        private static readonly ConcurrentDictionary<string, Type> _types = new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns></returns>
        public static Type GetType(string fullName, string assemblyName)
        {
            var key = string.Format("{0},{1}", fullName, assemblyName);
            Type typeret;
            if (_types.TryGetValue(key, out typeret ))
            {
                return typeret;
            }

            var type = AssemblyHelper.GetType(fullName, assemblyName);
            return _types.GetOrAdd(key, type);
        }
    }
}