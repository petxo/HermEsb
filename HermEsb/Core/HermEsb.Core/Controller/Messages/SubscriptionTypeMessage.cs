using System.Runtime.Serialization;

namespace HermEsb.Core.Controller.Messages
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class SubscriptionTypeMessage
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the assembly.
        /// </summary>
        /// <value>The name of the assembly.</value>
        [DataMember]
        public string AssemblyName { get; set; }
    }
}