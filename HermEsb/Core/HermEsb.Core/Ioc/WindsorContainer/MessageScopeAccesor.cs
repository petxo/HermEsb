using System;
using System.Collections.Concurrent;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle.Scoped;

namespace HermEsb.Core.Ioc.WindsorContainer
{
    public class MessageScopeAccesor : IScopeAccessor
    {
        private static readonly ConcurrentDictionary<Guid, ILifetimeScope> collection =
            new ConcurrentDictionary<Guid, ILifetimeScope>();

        public void Dispose()
        {
            foreach (var scope in collection)
            {
                scope.Value.Dispose();
            }
            collection.Clear();
        }

        public ILifetimeScope GetScope(CreationContext context)
        {
            return collection.GetOrAdd(ContextManager.Instance.CurrentContext.MessageInfo.Header.MessageId,
                                       id => new MessageLifetimeScope());
        }
    }
}