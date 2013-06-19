using HermEsb.Core.Messages.Control;
using HermEsb.Core.Processors.Router.Subscriptors;

namespace HermEsb.Core.Processors
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRouterController : IController
    {
        /// <summary>
        /// Publishes the specified identification.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="message">The message.</param>
        void Publish(Identification identification, IControlMessage message);


        /// <summary>
        /// Gets the subscriptors.
        /// </summary>
        /// <value>The subscriptors.</value>
        ISubscriptons Subscriptons { get; }
    }
}