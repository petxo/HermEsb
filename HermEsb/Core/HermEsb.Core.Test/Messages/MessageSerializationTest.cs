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
            var messageBus = new MessageBus
                                 {
                                     Body = "Test",
                                     Header = {BodyType = "System.string", CreatedAt = new DateTime(2010,10,10)}
                                 };

            var serialize = jsonDataContractSerializer.Serialize(messageBus);

            var json = "{\"Body\":\"Test\",\"Header\":{\"BodyType\":\"System.string\",\"CallContext\":{\"_currentSession\":[]},\"CallStack\":{\"_array\":[],\"_size\":0,\"_version\":0},\"CreatedAt\":\"\\/Date(1286661600000+0200)\\/\",\"EncodingCodePage\":0,\"IdentificationService\":{\"Id\":null,\"Type\":null},\"Priority\":0,\"ReinjectionNumber\":0,\"Type\":0}}";

            Assert.AreEqual(json, serialize);
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