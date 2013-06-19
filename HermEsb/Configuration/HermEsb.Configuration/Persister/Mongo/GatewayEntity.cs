namespace HermEsb.Configuration.Persister.Mongo
{
    public class GatewayEntity
    {
        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>The URI.</value>
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public int Transport { get; set; }
    }
}