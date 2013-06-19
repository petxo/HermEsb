namespace Mrwesb.Builder.Types
{
    using System;
    using System.Collections.Generic;
    using Builders;
    using Repositories;

    /// <summary>
    /// 
    /// </summary>
    public static class TypesManagerFactory
    {
        /// <summary>
        /// Defaults the type manager.
        /// </summary>
        /// <returns></returns>
        public static ITypesManager DefaultTypeManager()
        {
            var typeBuilder = TypesBuildersFactory.CreateDynamicTypeBuilder();
            var typesRepository = new TypesRepository(typeBuilder);
            return new TypesManager(typesRepository);
        }

    }
}