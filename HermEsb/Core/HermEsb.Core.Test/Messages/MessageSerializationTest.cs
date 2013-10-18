using System.Collections.Generic;
using HermEsb.Core.Messages;
using HermEsb.Core.Serialization;
using NUnit.Framework;
using System;
using System.Text;

namespace HermEsb.Core.Test.Messages
{
    [TestFixture]
    public class MessageSerializationTest
    {
        [Test]
        public void SerializeMessageBus()
        {
            var jsonDataContractSerializer = new JsonDataContractSerializer();
            var callerContexts = new Stack<CallerContext>();
            callerContexts.Push(new CallerContext { Identification = new Identification() { Id = "IdTest", Type = "TypeTest" } });
            var messageBus = new MessageBus
                                 {
                                     Body = "Test",
                                     Header = { BodyType = "Un tipo de body cualquiera me vale.", CreatedAt = new DateTime(2010, 10, 10), CallStack = callerContexts }
                                 };

            var serialize = jsonDataContractSerializer.Serialize(messageBus);

            var bytes = MessageBusParser.ToBytes(messageBus);
            var routerHeader = MessageBusParser.GetHeaderFromBytes(bytes);
            var message = MessageBusParser.GetMessageFromBytes(bytes);

            Assert.AreEqual(serialize, message);
            Assert.AreEqual("IdTest", routerHeader.Identification.Id);
            Assert.AreEqual("TypeTest", routerHeader.Identification.Type);
        }

        [Test]
        public void TestDeMierda()
        {
            var bytes = new byte[8];
            var ticks = DateTime.Now.Ticks;
            
            bytes[0] = (byte)(ticks >> 56);
            bytes[1] = (byte)(ticks >> 48);
            bytes[2] = (byte)(ticks >> 40);
            bytes[3] = (byte)(ticks >> 32);
            bytes[4] = (byte)(ticks >> 24);
            bytes[5] = (byte)(ticks >> 16);
            bytes[6] = (byte)(ticks >> 8);
            bytes[7] = (byte)(ticks);

            long newticks = ((long)bytes[0] << 56) + ((long)bytes[1] << 48) + ((long)bytes[2] << 40)
                         + ((long)bytes[3] << 32) + ((long)bytes[4] << 24) + ((long)bytes[5] << 16)
                         + ((long)bytes[6] << 8) + (long)bytes[7];

            Assert.AreEqual(ticks, newticks);
        }

        [Test]
        public void SerializeSpanishLettersMessageBus()
        {
            var jsonDataContractSerializer = new JsonDataContractSerializer();

            var messageBus = new MessageBus
            {
                Body = "El señor de l'ametlla del vallès está de vacaciones",
                Header = { BodyType = "System.string", CreatedAt = new DateTime(2010, 10, 10) }
            };

            var serialize = jsonDataContractSerializer.Serialize(messageBus);

            var deserialized = jsonDataContractSerializer.Deserialize(serialize, Encoding.UTF8, typeof(MessageBus)) as MessageBus;


            Assert.AreEqual(messageBus.Body, deserialized.Body);
            Assert.AreEqual(messageBus.Header, deserialized.Header);
        }
    }
}