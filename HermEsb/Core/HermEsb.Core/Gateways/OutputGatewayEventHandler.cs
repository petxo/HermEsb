using HermEsb.Core.Messages;

namespace HermEsb.Core.Gateways
{
    /// <summary>
    /// 
    /// </summary>
    public delegate void OutputGatewayEventHandler<TMessage, THeader>(object sender, OutputGatewayEventHandlerArgs<TMessage, THeader> args);
}