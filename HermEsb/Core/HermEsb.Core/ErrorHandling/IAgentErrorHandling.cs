namespace HermEsb.Core.ErrorHandling
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAgentErrorHandling
    {
        /// <summary>
        /// Occurs when [on error handler].
        /// </summary>
        event ErrorOnHandlersEventHandler OnErrorHandler; 
    }
}