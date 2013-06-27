using System;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle.Scoped;

namespace HermEsb.Core.Ioc.WindsorContainer
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageLifetimeScope : ILifetimeScope
    {
        private static readonly Action<Burden> EmptyOnAfterCreated = delegate { };
        private readonly object _lock = new object();
        private readonly Action<Burden> _onAfterCreated;
        private IScopeCache _scopeCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageLifetimeScope"/> class.
        /// </summary>
        /// <param name="scopeCache">The scope cache.</param>
        /// <param name="onAfterCreated">The on after created.</param>
        public MessageLifetimeScope(IScopeCache scopeCache = null, Action<Burden> onAfterCreated = null)
        {
            _scopeCache = scopeCache ?? new ScopeCache();
            _onAfterCreated = onAfterCreated ?? EmptyOnAfterCreated;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            lock (_lock)
            {
                if (_scopeCache == null)
                {
                    return;
                }
                var disposableCache = _scopeCache as IDisposable;
                if (disposableCache != null)
                {
                    disposableCache.Dispose();
                }
                _scopeCache = null;
            }
        }

        /// <summary>
        /// Gets the cached instance.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="createInstance">The create instance.</param>
        /// <returns></returns>
        public Burden GetCachedInstance(ComponentModel model, ScopedInstanceActivationCallback createInstance)
        {
            lock (_lock)
            {
                Burden burden = _scopeCache[model];
                if (burden == null)
                {
                    _scopeCache[model] = burden = createInstance(_onAfterCreated);
                }
                return burden;
            }
        }
    }
}