using System;
using System.Collections.Generic;
using System.Linq;
using HermEsb.Core.Handlers;
using HermEsb.Core.Handlers.Service;
using HermEsb.Core.Ioc;
using HermEsb.Core.Test.Fakes.Messages;
using HermEsb.Core.Test.Handlers.Dynamic.Domain;
using Moq;
using NUnit.Framework;

namespace HermEsb.Core.Test.Service.Hanlders
{
    [TestFixture]
    public class HandlerRepositoryTest
    {
        [Ignore]
        public void LoadHandlers()
        {
            var contextManager = new Mock<IContextManager>();
            contextManager.Setup(c => c.AddComponent(It.IsAny<Type>()));

            var handlerRepository = HandlerRepositoryFactory.GetFactory<ServiceHandlerRepositoryFactory>(contextManager.Object)
                .Create(new List<string> { "Mrwesb.Core.Test.dll" });

            Assert.IsTrue(handlerRepository.GetHandlersByMessage(typeof(MessageFake)).Any());
            Assert.IsTrue(handlerRepository.GetHandlersByMessage(typeof(IDynamicChild1Level2)).Any());
        }

        [Ignore]
        public void LoadHandlersFromAssemblies()
        {
            var contextManager = new Mock<IContextManager>();
            contextManager.Setup(c => c.AddComponent(It.IsAny<Type>()));

            var handlerRepository = HandlerRepositoryFactory.GetFactory<ServiceHandlerRepositoryFactory>(contextManager.Object)
                .Create(new List<string> { "Operativa.UI.Envios.dll" });

            Assert.IsTrue(handlerRepository.GetHandlersByMessage(typeof(MessageFake)).Any());
        }

    }
}