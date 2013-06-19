using System;
using System.Collections.Generic;
using HermEsb.Core.Builder.Types;

namespace HermEsb.Core.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectBuilderFactory
    {

        /// <summary>
        /// Defaults the object builder.
        /// </summary>
        /// <returns></returns>
        public static IObjectBuilder DefaultObjectBuilder()
        {
            var defaultTypeManager = TypesManagerFactory.DefaultTypeManager();
            return new ObjectBuilder(defaultTypeManager);
        }


        /// <summary>
        /// Defaults the object builder.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        public static IObjectBuilder DefaultObjectBuilder(IEnumerable<Type> types)
        {
            var defaultTypeManager = TypesManagerFactory.DefaultTypeManager();
            defaultTypeManager.AddType(types);
            return new ObjectBuilder(defaultTypeManager);
        }

        /// <summary>
        /// Defaults the object builder.
        /// </summary>
        /// <param name="interfaceToType">Type of the interface to.</param>
        /// <returns></returns>
        public static IObjectBuilder DefaultObjectBuilder(IEnumerable<KeyValuePair<Type, Type>> interfaceToType)
        {
            var defaultTypeManager = TypesManagerFactory.DefaultTypeManager();

            foreach (var pair in interfaceToType)
            {
                defaultTypeManager.AddInterfaceToType(pair.Key, pair.Value);
            }
            
            return new ObjectBuilder(defaultTypeManager);
        }

    }
}