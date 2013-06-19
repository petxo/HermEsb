namespace HermEsb.Core.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public static class CallerContextFactory
    {
        /// <summary>
        /// Creates the specified identification.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="session">The session.</param>
        /// <returns></returns>
        public static CallerContext Create(Identification identification, Session session)
        {
            return new CallerContext { Identification = identification, Session = session };
        }

        /// <summary>
        /// Creates the specified identification.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <returns></returns>
        public static CallerContext Create(Identification identification)
        {
            return new CallerContext { Identification = identification };
        }

    }
}