namespace HermEsb.Core.Communication.Channels.Msmq
{
    /// <summary>
    /// 
    /// </summary>
    public class MsmqChannelController : IChannelController
    {
        private readonly IMessageQueue _queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsmqChannelController"/> class.
        /// </summary>
        /// <param name="queue">The queue.</param>
        internal MsmqChannelController(IMessageQueue queue)
        {
            _queue = queue;
        }

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            int count = 0;
            using (var enumerator = _queue.GetMessageEnumerator2())
            {
                while (enumerator.MoveNext())
                    count++;
            }

            return count;
        }

        /// <summary>
        /// Purges this instance.
        /// </summary>
        public void Purge()
        {
            _queue.Purge();
        }
    }
}