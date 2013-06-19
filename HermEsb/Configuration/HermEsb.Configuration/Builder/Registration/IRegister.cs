using System;
using System.Collections.Generic;

namespace HermEsb.Configuration.Builder.Registration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRegister
    {
        /// <summary>
        /// Alls the interface to class.
        /// </summary>
        /// <returns></returns>
        IEnumerable<KeyValuePair<Type, Type>> AllInterfaceToClass();

        /// <summary>
        /// Alls the types.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Type> AllTypes();
    }
}