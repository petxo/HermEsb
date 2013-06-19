using System.Collections.Generic;

namespace HermEsb.Core.Monitoring.Frequences
{
    /// <summary>
    /// 
    /// </summary>
    public class FrequenceHelper : IFrequenceHelper
    {
        private readonly IDictionary<FrequenceLevel, int> _frequenceList;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrequenceHelper"/> class.
        /// </summary>
        public FrequenceHelper()
        {
            _frequenceList = new Dictionary<FrequenceLevel, int>
                                 {
                                     {FrequenceLevel.Lowest, 600},
                                     {FrequenceLevel.Low, 300},
                                     {FrequenceLevel.Normal, 60},
                                     {FrequenceLevel.High, 30},
                                     {FrequenceLevel.Highest, 10}
                                 };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrequenceHelper"/> class.
        /// </summary>
        /// <param name="frequenceList">The frequence list.</param>
        public FrequenceHelper(IDictionary<FrequenceLevel, int> frequenceList)
        {
            _frequenceList = frequenceList;
        }


        /// <summary>
        /// Changes the frequence.
        /// </summary>
        /// <param name="frequenceLevel">The frequence level.</param>
        /// <param name="time">The time in seconds.</param>
        public void ChangeFrequence(FrequenceLevel frequenceLevel, int time)
        {
            _frequenceList[frequenceLevel] = time;
        }

        /// <summary>
        /// Gets the frequence.
        /// </summary>
        /// <param name="frequenceLevel">The frequence level.</param>
        /// <returns></returns>
        public int GetFrequence(FrequenceLevel frequenceLevel)
        {
            return _frequenceList[frequenceLevel];
        }

    }
}