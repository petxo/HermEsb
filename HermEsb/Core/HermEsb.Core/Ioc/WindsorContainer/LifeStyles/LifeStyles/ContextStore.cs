using System;
using System.Threading;

namespace HermEsb.Core.Ioc.WindsorContainer.LifeStyles
{
    /// <summary>
    /// </summary>
    public static class ContextStore
    {
        /// <summary>
        /// </summary>
        private const string CONTEXT = "Context";

        /// <summary>
        ///     Creates the context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void SaveContext(IContext context)
        {
            LocalDataStoreSlot ctxSlot = Thread.GetNamedDataSlot(CONTEXT);
            Thread.SetData(ctxSlot, context);
        }

        /// <summary>
        ///     Gets the context.
        /// </summary>
        /// <returns></returns>
        public static IContext GetContext()
        {
            LocalDataStoreSlot ctxSlot = Thread.GetNamedDataSlot(CONTEXT);
            return (IContext) Thread.GetData(ctxSlot);
        }

        /// <summary>
        ///     Disposes the context.
        /// </summary>
        public static void DisposeContext()
        {
            IContext context = GetContext();
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}