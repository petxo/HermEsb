using System;

namespace HermEsb.Core.ErrorHandling
{
    /// <summary>
    /// 
    /// </summary>
    public interface IErrorHandlingController : IDisposable
    {
        /// <summary>
        /// Adds the agent error handling.
        /// </summary>
        /// <param name="agent">The _agent.</param>
        void AddAgentErrorHandling(IAgentErrorHandling agent);

        /// <summary>
        /// Adds the router error handling.
        /// </summary>
        /// <param name="router">The _router.</param>
        void AddRouterErrorHandling(IRouterErrorHandling router);
    }
}