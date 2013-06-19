using HermEsb.Core.Builder;
using HermEsb.Core.Messages.Builders;

namespace HermEsb.Configuration.MessageBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConfigurationMessageBuilder
    {
        static ConfigurationMessageBuilder()
        {
            MessageBuilder = MessageBuilderFactory.CreateDefaultBuilder(ObjectBuilderFactory.DefaultObjectBuilder());
        }

        /// <summary>
        /// Gets the object builder.
        /// </summary>
        /// <value>The object builder.</value>
        public static IMessageBuilder MessageBuilder { get; private set; }

        /// <summary>
        /// Configures the object builder.
        /// </summary>
        /// <param name="configurationHelper">The configuration helper.</param>
        /// <returns></returns>
        public static ConfigurationBuilderHelper<ConfigurationHelper> ConfigureMessageBuilder(this ConfigurationHelper configurationHelper)
        {
            return new ConfigurationBuilderHelper<ConfigurationHelper>(configurationHelper, MessageBuilder);
        }

        /// <summary>
        /// Configures the message builder.
        /// </summary>
        /// <param name="configurationHelper">The configuration helper.</param>
        /// <returns></returns>
        public static ConfigurationBuilderHelper<ConfigurationPublisher> ConfigureMessageBuilder(this ConfigurationPublisher configurationHelper)
        {
            return new ConfigurationBuilderHelper<ConfigurationPublisher>(configurationHelper, MessageBuilder);
        }
    }
}