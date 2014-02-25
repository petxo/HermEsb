using System;
using BasicSampleContracts;
using HermEsb.Core;
using HermEsb.Core.Handlers.Service;

namespace BasicSampleService.Handlers
{
    public class MessageBasicHandler : IServiceMessageHandler<IMessageBasic>
    {
        public void HandleMessage(IMessageBasic message)
        {
            Console.WriteLine("Hola son las {0}", message.Fecha);
        }

        public void Dispose()
        {
            
        }

        public IBus Bus { get; set; }
    }
}