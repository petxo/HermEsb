using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;

namespace HermEsb.Core.Processors
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfigurableProcessor : IProcessor
    {
        /// <summary>
        /// Configures the output gateway.
        /// </summary>
        /// <param name="outputGateway">The output gateway.</param>
        void ConfigureOutputGateway(IOutputGateway<IMessage> outputGateway);
    }
}