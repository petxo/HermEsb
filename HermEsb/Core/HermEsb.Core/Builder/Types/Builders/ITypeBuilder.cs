using System;

namespace HermEsb.Core.Builder.Types.Builders
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITypeBuilder
    {
        /// <summary>
        /// Creates the type from.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        Type CreateTypeFrom(Type t);
    }
}