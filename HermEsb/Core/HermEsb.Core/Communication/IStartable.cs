using System;

namespace HermEsb.Core.Communication
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TStatus">The type of the status.</typeparam>
    public interface IStartable<out TStatus>
    {

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>The status.</value>
        TStatus Status { get; }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();

        /// <summary>
        /// Occurs when [on start].
        /// </summary>
        event EventHandler OnStart;

        /// <summary>
        /// Occurs when [on stop].
        /// </summary>
        event EventHandler OnStop;
    }
}