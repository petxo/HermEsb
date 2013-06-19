using System;
using HermEsb.Core.Messages.Control;

namespace HermEsb.Core.Processors.Router.Subscriptors
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISubscriptorsHelper : IDisposable, ISubscriptons
    {
        /// <summary>
        /// Sends the specified identification.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="messageControl">The message control.</param>
        void Send(Identification identification, IControlMessage messageControl);

        /// <summary>
        /// Sends broad cast the specified message bus.
        /// </summary>
        /// <param name="messageControl">The message control.</param>
        void Send(IControlMessage messageControl);

        /// <summary>
        /// Loads the stored subscriptors.
        /// </summary>
        void LoadStoredSubscriptors(Identification identification);

        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>The controller.</value>
        IRouterController Controller { get; set; }
    }
}