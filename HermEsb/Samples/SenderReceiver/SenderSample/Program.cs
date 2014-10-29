using System;
using HermEsb.Configuration;
using HermEsb.Configuration.Builder.Registration;
using HermEsb.Configuration.MessageBuilder;
using ReceiverSampleContracts;

namespace SenderSample
{
    class Program
    {
        static void Main(string[] args)
        {

            var busPublisher = ConfigurationPublisher.With("config/sender.xml")
                                                .Log4NetBuilder("config/logging.xml")
                                                .ConfigureMessageBuilder().With(
                                                                            RegisterTypes.FromAssembly(typeof(IMySimpleData))
                                                                                .Pick()
                                                                                .WithService.FirstInterface()
                                                                         ).AllInterfaceToClass()
                                                .ConfigurePublisher()
                                                .Create();
            Console.WriteLine("Pulsa un tecla para enviar un mensaje, o X para salir.");
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.KeyChar == 'x' || key.KeyChar == 'X') break;

                var mySimpleData = busPublisher.MessageBuilder.CreateInstance<IMySimpleData>(data =>
                {
                    data.Data = "Hello Receiver";
                    data.Count = 10;
                });

                busPublisher.Publish(mySimpleData);
            }
        }
    }
}
