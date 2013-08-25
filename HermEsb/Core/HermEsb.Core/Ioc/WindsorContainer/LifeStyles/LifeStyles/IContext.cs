using System;
using System.Collections.Generic;

namespace HermEsb.Core.Ioc.WindsorContainer.LifeStyles
{
    /// <summary>
    /// </summary>
    public interface IContext : IDisposable
    {
        /// <summary>
        ///     Gets the components.
        /// </summary>
        /// <value>The components.</value>
        IDictionary<string, object> Components { get; }

        /// <summary>
        ///     Inits this instance.
        /// </summary>
        void Init();

        /// <summary>
        ///     Occurs when [ended].
        /// </summary>
        event EventHandler Ended;

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        void Clear();
    }
}