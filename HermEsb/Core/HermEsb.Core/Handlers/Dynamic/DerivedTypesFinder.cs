using System;
using System.Collections.Generic;
using System.Linq;

namespace HermEsb.Core.Handlers.Dynamic
{
    /// <summary>
    /// 
    /// </summary>
    public static class DerivedTypesFinder
    {
        /// <summary>
        /// Froms the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IList<Type> FromType(Type type)
        {
            var types = new List<Type>();

            FindTypes(type, types);

            return types;
        }


        /// <summary>
        /// Finds the types.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        private static bool FindTypes(Type type, IList<Type> types)
        {
            var found = false;
            foreach (var assemblyType in type.Assembly.GetTypes().Where(t => t.IsInterface && t.GetInterface(type.FullName) != null))
            {
                if (!FindTypes(assemblyType, types))
                {
                    if(!types.Contains(assemblyType))
                        types.Add(assemblyType);
                }
                found = true;
            }

            return found;
        }
    }
}