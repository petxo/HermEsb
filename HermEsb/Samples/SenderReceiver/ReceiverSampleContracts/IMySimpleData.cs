using HermEsb.Core.Messages;

namespace ReceiverSampleContracts
{
    public interface IMySimpleData : IMessage
    {

        string Data { get; set; }

        int Count { get; set; }

    }
}
