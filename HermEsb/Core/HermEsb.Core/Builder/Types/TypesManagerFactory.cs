using HermEsb.Core.Builder.Types.Builders;
using HermEsb.Core.Builder.Types.Repositories;

namespace HermEsb.Core.Builder.Types
{
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