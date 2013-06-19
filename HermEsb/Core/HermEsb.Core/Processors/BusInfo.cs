namespace HermEsb.Core.Processors
{
    /// <summary>
    /// 
    /// </summary>
    public class BusInfo : IBusInfo
    {
        /// <summary>
        /// Gets or sets the empty.
        /// </summary>
        /// <value>The empty.</value>
        public static BusInfo Create()
        {
            return new BusInfo();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusInfo"/> class.
        /// </summary>
        private BusInfo()
        {
            Identification = new Identification();
        }

        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        /// <value>The identification.</value>
        public Identification Identification { get; set; }
    }
}