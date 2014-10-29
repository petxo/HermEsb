using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicSampleContracts;
using HermEsb.Configuration;
using HermEsb.Configuration.MessageBuilder;

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
                var messageBasic = busPublisher.MessageBuilder.CreateInstance<MessageBasic>(basic =>
                {
                    basic.Fecha = DateTime.UtcNow;
                    basic.Nombre = "Lorem ipsum dolor sit amet, iusto utamur consequuntur mel an. Te dolore mediocritatem sed, etiam affert pri te. Vix habeo civibus efficiendi ne, tamquam nominati duo no. Pro soleat scriptorem ad." +
                                    "Suas debitis fierent ne mea, mea an deleniti imperdiet. No aliquip nominati quo, id est nostrum reformidans. In vim quas iisque nostrum, in his sale altera putent, ea eum decore lobortis. Ut labore aperiam definiebas vel, vocent nominavi ei vim. Te mundi option conclusionemque vim, quo id nibh nonumes. An alii persius quo." +
                                    "Iusto vivendum signiferumque his ea, sed eros sale temporibus ne, at vix mucius cetero. Ne eum sale libris mollis, id singulis imperdiet voluptatum sit. Nam quis aliquando vulputate in, eu tempor docendi pri. Justo dissentiunt his te. Ad movet integre inciderint vis. Eu vim eros solum definitionem, dico adipisci est id. Eam soleat sapientem interesset id." +
                                    "Essent reprehendunt his id, fugit aliquid explicari pro eu. Mel ex pertinax expetendis consectetuer, ex affert alienum vulputate mei, nam facilis volumus an. Pertinacia scripserit in eam, eos saepe urbanitas definitiones an, cu meliore placerat reprimique sea. Debet concludaturque vix ut, mei ea suas aliquam dissentiunt, eam altera melius rationibus et. Ei usu fugit deserunt percipitur. Nec ne mundi phaedrum." +
                                    "Gloriatur incorrupte sit et, lorem legendos pro id. Ei solum assentior vix, cum debet vitae aliquid an. Eos cu quando salutatus deseruisse, eleifend reprehendunt vix id. Mundi detracto vis id.";
                });
                for (int i = 0; i < 1000000; i++)
                {
                    busPublisher.Publish(messageBasic);
                }
            }
        }
    }
}
