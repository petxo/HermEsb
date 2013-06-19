namespace HermEsb.Core.ErrorHandling
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRouterErrorHandling
    {
        /// <summary>
        /// Occurs when [on router error].
        /// </summary>
        event ErrorOnRouterEventHandler OnRouterError;
    }
}