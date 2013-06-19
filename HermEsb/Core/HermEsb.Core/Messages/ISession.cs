using System;
using System.Collections.Generic;

namespace HermEsb.Core.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISession : IDictionary<string, object>, ICloneable
    {
    }
}