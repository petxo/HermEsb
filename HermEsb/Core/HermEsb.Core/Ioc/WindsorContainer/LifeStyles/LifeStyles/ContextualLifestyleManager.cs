using System;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;

namespace HermEsb.Core.Ioc.WindsorContainer.LifeStyles
{
    /// <summary>
    /// </summary>
    public class ContextualLifestyleManager : AbstractLifestyleManager
    {
        private readonly string _contextualObjectId =
            "ContextualManager_" + Guid.NewGuid();

        /// <summary>
        ///     Resolves the specified creation context.
        /// </summary>
        /// <param name="creationContext">The creation context.</param>
        /// <returns></returns>
        public object Resolve(CreationContext creationContext)
        {
            IContext context = GetContext(creationContext);
            if (!context.Components.ContainsKey(_contextualObjectId))
            {
                context.Components.Add(_contextualObjectId, base.Resolve(creationContext, Kernel.ReleasePolicy));
                ContextStore.SaveContext(context);
            }
            return context.Components[_contextualObjectId];
        }

        /// <summary>
        ///     Gets the context.
        /// </summary>
        /// <param name="creationContext">The creation context.</param>
        /// <returns></returns>
        private IContext GetContext(CreationContext creationContext)
        {
            IContext context = ContextStore.GetContext();
            if (context == null)
            {
                //No existe el contexto para el hilo actual
                //Creamo el contexto
                context = new ContextThread();
                context.Init();
                context.Ended += ((sender, e) =>
                    {
                        var contextSender = (IContext) sender;
                        foreach (object component in contextSender.Components.Values)
                        {
                            Kernel.ReleaseComponent(component);
                        }
                        contextSender.Components.Clear();
                        ContextStore.SaveContext(null);
                    });

                ContextStore.SaveContext(context);
            }
            return context;
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        public override void Dispose()
        {
        }
    }
}