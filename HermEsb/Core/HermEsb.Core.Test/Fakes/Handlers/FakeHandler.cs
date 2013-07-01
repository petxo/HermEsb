using HermEsb.Core.Handlers.Service;
using HermEsb.Core.Test.Fakes.Messages;

namespace HermEsb.Core.Test.Fakes.Handlers
{
    public class FakeHandler : IServiceMessageHandler<MessageFake>
    {
        public void HandleMessage(MessageFake message)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IBus Bus { get; set; }
    }
}