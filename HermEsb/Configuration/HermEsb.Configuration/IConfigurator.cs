using HermEsb.Core.Service;

namespace HermEsb.Configuration
{
    public interface IConfigurator
    {
        IService Create();
        void Configure();
    }
}