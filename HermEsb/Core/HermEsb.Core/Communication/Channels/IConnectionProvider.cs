namespace HermEsb.Core.Communication.Channels
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TConnection">The type of the connection.</typeparam>
    public interface IConnectionProvider<TConnection>
    {
        /// <summary>
        /// Connects this instance.
        /// </summary>
        /// <returns></returns>
        TConnection Connect();
    }
}