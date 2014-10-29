using System;
using HermEsb.Configuration;
using HermEsb.Configuration.Logging;

namespace ReceiverSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = ConfigurationHelper.With("config/receiver.xml")
                .Log4NetBuilder("config/logging.xml")
                .ConfigureListener()
                .Create();

            service.Start();

            Console.WriteLine("Pulsa X para parar el servicio");
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.KeyChar == 'x' || key.KeyChar == 'X') break;
            }
            service.Stop();
        }
    }
}
