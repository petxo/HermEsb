namespace HermEsb.Core.Communication.EndPoints
{
    ///<summary>
    ///</summary>
    public interface ISenderEndPoint : IEndPoint
    {

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Send(string message);


        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        void Send(string message, int priority);

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="priority">The priority.</param>
        void Send(byte[] message, int priority);
    }
}