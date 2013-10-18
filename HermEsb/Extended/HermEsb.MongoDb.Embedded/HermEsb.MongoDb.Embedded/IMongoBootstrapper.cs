namespace HermEsb.Extended.MongoDb.Embedded
{
    public interface IMongoBootstrapper
    {
        void Startup(MongoContextType context = MongoContextType.Live);
        void Shutdown();
    }
}