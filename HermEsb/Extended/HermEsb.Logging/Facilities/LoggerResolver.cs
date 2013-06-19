using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;

namespace HermEsb.Logging.Facilities
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggerResolver : ISubDependencyResolver
    {
        /// <summary>
        /// Resolves the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="contextHandlerResolver">The context handler resolver.</param>
        /// <param name="model">The model.</param>
        /// <param name="dependency">The dependency.</param>
        /// <returns></returns>
        public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, 
                                ComponentModel model, DependencyModel dependency)
        {
            return LoggerManager.Instance;
        }

        /// <summary>
        /// Determines whether this instance can resolve the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="contextHandlerResolver">The context handler resolver.</param>
        /// <param name="model">The model.</param>
        /// <param name="dependency">The dependency.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can resolve the specified context; otherwise, <c>false</c>.
        /// </returns>
        public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, 
                                ComponentModel model, DependencyModel dependency)
        {
            return dependency.TargetType == typeof(ILogger);
        }
    }
}