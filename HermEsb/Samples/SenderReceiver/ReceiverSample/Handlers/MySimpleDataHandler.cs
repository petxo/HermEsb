using System;
using HermEsb.Core;
using HermEsb.Core.Handlers.Listeners;
using ReceiverSampleContracts;

namespace ReceiverSample.Handlers
{
    public class MySimpleDataHandler : IListenerMessageHandler<IMySimpleData>
    {
        public void HandleMessage(IMySimpleData message)
        {
            Console.WriteLine(message.Data);
        }

        public void Dispose()
        {
            
        }
    }
}