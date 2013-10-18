using System;

namespace HermEsb.Extended.MongoDb.Embedded
{
    public interface IMongoDeployer : IDisposable
    {
        /// <summary>
        /// Deploys the specified redeploy.
        /// </summary>
        /// <param name="redeploy">if set to <c>true</c> [redeploy].</param>
        void Deploy(bool redeploy = false);
        
        /// <summary>
        /// Kills this instance.
        /// </summary>
        void Kill();

        /// <summary>
        /// Determines whether this instance is running.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </returns>
        bool IsRunning();

        /// <summary>
        /// Runs this instance.
        /// </summary>
        void Run();

        /// <summary>
        /// Determines whether this instance is deployed.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is deployed; otherwise, <c>false</c>.
        /// </returns>
        bool IsDeployed();

        /// <summary>
        /// Removes this instance.
        /// </summary>
        void Remove();
    }
}