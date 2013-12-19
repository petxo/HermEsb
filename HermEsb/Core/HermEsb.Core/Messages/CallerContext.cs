namespace HermEsb.Core.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class CallerContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallerContext"/> class.
        /// </summary>
        internal CallerContext()
        {
            Session = new Session();
        }
        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        /// <value>The identification.</value>
        public Identification Identification { get; set; }

        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>The session.</value>
        public Session Session { get; set; }
    }
}