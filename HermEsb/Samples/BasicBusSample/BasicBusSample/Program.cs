using System;
using System.IO;
using HermEsb.Configuration;
using HermEsb.Configuration.Logging;
using HermEsb.Logging;
using HermEsb.Logging.L4N;

namespace BasicBusSample
{
    class Program
    {
        static void Main(string[] args)
        {

            LoggerManager.Create(Log4NetFactory.CreateFrom("config/logging.xml"));
            LoggerManager.Instance.Debug("Instanciando el servicio");

            try
            {
                var service = ConfigurationHelper.With(Path.Combine(Environment.CurrentDirectory, "config/service.xml"))
                    .Log4NetBuilder("config/logging.xml")
                    .ConfigureBus()
                    .Create();

                service.Start();

                Console.WriteLine("Pulsa X para parar");
                while (true)
                {
                    var key = Console.ReadKey(true);
                    if (key.KeyChar == 'x' || key.KeyChar == 'X') break;
                }
                service.Stop();
                service.Dispose();
                Console.WriteLine("Bus parado");
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error("Error Iniciar el servicio", ex);
                throw;
            }
        }
    }
}
