namespace Mrwesb.Builder
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface ITypeRegister
    {
        /// <summary>
        /// Registers the specified types.
        /// </summary>
        /// <param name="types">The types.</param>
        void Register(IEnumerable<Type> types);

        /// <summary>
        /// Registers the specified interface to type.
        /// </summary>
        /// <param name="interfaceToType">Type of the interface to.</param>
        void Register(IEnumerable<KeyValuePair<Type, Type>> interfaceToType);
    }
}