namespace HermEsb.Logging.L4N
{
    /// <summary>
    /// 
    /// </summary>
    public static class Log4NetFactory
    {
        /// <summary>
        /// Defaults the logger.
        /// </summary>
        /// <returns></returns>
        public static ILogger DefaultLogger()
        {
            return new Log4NetWrapper();
        }

        /// <summary>
        /// Creates from.
        /// </summary>
        /// <param name="pathConfig">The path config.</param>
        /// <returns></returns>
        public static ILogger CreateFrom(string pathConfig)
        {
            return new Log4NetWrapper(pathConfig);
        }
        
    }
}