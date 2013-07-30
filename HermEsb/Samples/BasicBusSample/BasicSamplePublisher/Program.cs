using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicSampleContracts;
using HermEsb.Configuration;

namespace BasicSamplePublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var busPublisher = ConfigurationPublisher.With("config/publisher.xml")
                                                .Log4NetBuilder("config/logging.xml")
                                                .ConfigurePublisher()
                                                .Create();

            Console.WriteLine("Pulsa una tecla para enviar un mensaje, pulsa X para salir.");
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.KeyChar == 'x' || key.KeyChar == 'X') break;
                var messageBasic = busPublisher.MessageBuilder.CreateInstance<MessageBasic>(MessageBasicFactory.FillData);
                for (var i = 0; i < 1000000; i++)
                {
                    busPublisher.Publish(messageBasic);
                }
            }
        }
    }
}
