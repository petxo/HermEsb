using HermEsb.Core.Messages;

namespace HermEsb.Core.Publishers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBusPublisher : IPublisher
    {
        /// <summary>
        /// Publishes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        void Publish(IMessage message, int priority);

        /// <summary>
        /// Publishes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="callContext">The call context.</param>
        void Publish(IMessage message, int priority, Session callContext);
    }
}