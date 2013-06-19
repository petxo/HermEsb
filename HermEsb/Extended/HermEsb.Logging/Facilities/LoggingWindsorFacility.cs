using Castle.MicroKernel.Facilities;

namespace HermEsb.Logging.Facilities
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggingWindsorFacility : AbstractFacility
    {
        /// <summary>
        /// Inits this instance.
        /// </summary>
        protected override void Init()
        {
            Kernel.Resolver.AddSubResolver(new LoggerResolver());
        }
    }
}