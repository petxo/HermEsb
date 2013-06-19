namespace HermEsb.Core.Communication.Channels
{
    /// <summary>
    /// 
    /// </summary>
    public interface IChannelController
    {
        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Purges this instance.
        /// </summary>
        void Purge();
    }
}