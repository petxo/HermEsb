namespace HermEsb.Extended.MongoDb.Embedded
{
    public class MongoBootstrapper : IMongoBootstrapper
    {
        private readonly IMongoDeployer _deployer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoBootstrapper"/> class.
        /// </summary>
        /// <param name="deployer">The deployer.</param>
        public MongoBootstrapper(IMongoDeployer deployer)
        {
            _deployer = deployer;
        }

        #region IMongoBootstrapper Members

        public virtual void Startup(MongoContextType context = MongoContextType.Live)
        {
            _deployer.Deploy(context == MongoContextType.Test);
            if (_deployer.IsRunning()) return;
            _deployer.Run();

        }

        public virtual void Shutdown()
        {
            _deployer.Kill();
        }

        #endregion
    }
}