using System.Linq;
using HermEsb.Core.Handlers;
using HermEsb.Core.Test.Fakes.Handlers;
using HermEsb.Core.Test.Fakes.Messages;
using NUnit.Framework;

namespace HermEsb.Core.Test.Handlers
{
    [TestFixture]
    public class HandlerRepositoryTest
    {
        [Test]
        public void AddHandler_GetHandlersByMessage()
        {
            var subject = new HandlerRepository();

            subject.AddHandler(typeof(MessageFake), typeof(FakeHandler));

            var handlers = subject.GetHandlersByMessage(typeof(MessageFake));

            Assert.IsTrue(handlers.Count(x => x == typeof(FakeHandler)) == 1);
        }
    }
}
