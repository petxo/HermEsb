namespace HermEsb.Core.Monitoring.Frequences
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFrequenceHelper
    {
        /// <summary>
        /// Changes the frequence.
        /// </summary>
        /// <param name="frequenceLevel">The frequence level.</param>
        /// <param name="time">The time in seconds.</param>
        void ChangeFrequence(FrequenceLevel frequenceLevel, int time);

        /// <summary>
        /// Gets the frequence.
        /// </summary>
        /// <param name="frequenceLevel">The frequence level.</param>
        /// <returns></returns>
        int GetFrequence(FrequenceLevel frequenceLevel);
    }
}