using HermEsb.Core.Builder;

namespace HermEsb.Core.Messages.Builders
{
    /// <summary>
    /// 
    /// </summary>
    public static class MessageBuilderFactory
    {

        /// <summary>
        /// Creates the default builder.
        /// </summary>
        /// <param name="objectBuilder">The object builder.</param>
        /// <returns></returns>
        public static IMessageBuilder CreateDefaultBuilder(IObjectBuilder objectBuilder)
        {
            MessageBuilder.Create(objectBuilder);
            return MessageBuilder.Instance;
        }

        /// <summary>
        /// Creates the default builder.
        /// </summary>
        /// <returns></returns>
        public static IMessageBuilder CreateDefaultBuilder()
        {
            MessageBuilder.Create(ObjectBuilderFactory.DefaultObjectBuilder());
            return MessageBuilder.Instance;
        }

    }
}