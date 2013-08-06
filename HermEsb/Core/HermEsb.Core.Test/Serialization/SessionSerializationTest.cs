using System.Collections.Generic;
using System.Globalization;
using HermEsb.Core.Messages;
using HermEsb.Core.Serialization;
using NUnit.Framework;

namespace HermEsb.Core.Test.Serialization
{
    [TestFixture]
    public class SessionSerializationTest
    {
        [Test]
        public void SerializeSession()
        {
            var session = new Session();
            session.Add("test1", "test");
            session.Add("test2", "test");
            session.Add("test3", "test");
            session.Add("test4", "test");
            session.Add("test5", "test");
            session.Add("test6", "test");
            session.Add("test7", "test");
            session.Add("test8", "test");
            var jsonDataContractSerializer = new JsonDataContractSerializer();

            var serialize = jsonDataContractSerializer.Serialize(session);
            var deserialize = jsonDataContractSerializer.Deserialize<Session>(serialize);
            Assert.IsTrue(deserialize.Count == 8);
        }

        [Test]
        public void SerializeCallStack()
        {
            var stack = new Stack<CallerContext>();

            for (int i = 0; i < 10; i++)
            {
                stack.Push(new CallerContext { Identification = new Identification { Id = i.ToString(CultureInfo.InvariantCulture) }, Session = new Session() });
            }

            var jsonDataContractSerializer = new JsonDataContractSerializer();

            var serialize = jsonDataContractSerializer.Serialize(stack);
            var deserialize = jsonDataContractSerializer.Deserialize<Stack<CallerContext>>(serialize);
        }
    }
}