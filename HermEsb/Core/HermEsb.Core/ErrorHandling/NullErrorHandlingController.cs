namespace HermEsb.Core.ErrorHandling
{
    /// <summary>
    /// 
    /// </summary>
    public class NullErrorHandlingController : IErrorHandlingController
    {
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            
        }

        /// <summary>
        /// Adds the agent error handling.
        /// </summary>
        /// <param name="agent">The _agent.</param>
        public void AddAgentErrorHandling(IAgentErrorHandling agent)
        {
            
        }

        /// <summary>
        /// Adds the router error handling.
        /// </summary>
        /// <param name="router">The _router.</param>
        public void AddRouterErrorHandling(IRouterErrorHandling router)
        {
            
        }
    }
}