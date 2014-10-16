using System.Runtime.Serialization;

namespace HermEsb.Core.Controller.Messages
{
    /// <summary>
    /// 
    /// </summary>

    public class SubscriptionTypeMessage
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>

        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the assembly.
        /// </summary>
        /// <value>The name of the assembly.</value>

        public string AssemblyName { get; set; }
    }
}