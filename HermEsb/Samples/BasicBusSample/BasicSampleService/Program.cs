using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicSampleContracts;
using HermEsb.Configuration;
using HermEsb.Configuration.Builder.Registration;
using HermEsb.Configuration.Ioc;
using HermEsb.Configuration.Logging;
using HermEsb.Configuration.MessageBuilder;
using HermEsb.Logging;
using HermEsb.Logging.L4N;

namespace BasicSampleService
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerManager.Create(Log4NetFactory.CreateFrom("config/logging.xml"));
            var service = ConfigurationHelper.With("config/service.xml")
                                                                .Log4NetBuilder("config/logging.xml")
                                                                .ConfigureMessageBuilder()
                                                                    .With(
                                                                            RegisterTypes.FromAssembly(typeof(MessageBasic))
                                                                                .Pick()
                                                                                .WithService.FirstInterface()
                                                                         ).AllInterfaceToClass()
                                                                .ConfigureService()
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
