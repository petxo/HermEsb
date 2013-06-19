namespace HermEsb.Core.Gateways
{
    /// <summary>
    /// 
    /// </summary>
    public delegate void OutputGatewayEventHandler<TMessage>(object sender, OutputGatewayEventHandlerArgs<TMessage> args);
}