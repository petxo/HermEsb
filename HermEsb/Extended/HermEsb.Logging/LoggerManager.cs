namespace HermEsb.Logging
{


    /// <summary>
    /// 
    /// </summary>
    public static class LoggerManager
    {

        static LoggerManager()
        {
            Instance = new NullLogger();
        }


        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static ILogger Instance { get; private set; }


        /// <summary>
        /// Creates the specified logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public static void Create(ILogger logger)
        {
            Instance = logger;
        }

    }
}