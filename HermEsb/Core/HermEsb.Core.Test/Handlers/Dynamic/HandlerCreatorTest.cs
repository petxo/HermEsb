using System;
using HermEsb.Core.Handlers.Dynamic;
using HermEsb.Core.Test.Handlers.Dynamic.Domain;
using HermEsb.Core.Test.Handlers.Dynamic.Handlers;
using NUnit.Framework;

namespace HermEsb.Core.Test.Handlers.Dynamic
{
    [TestFixture]
    public class HandlerCreatorTest
    {
        [Test]
        public void CreateTypeHandler()
        {
            var dictionary = HandlerCreatorInstance.Instance.CreateFrom(typeof (DynamicMessageHandler<>), typeof (IDynamicBaseMessage));

            foreach (var type in dictionary)
            {
                var instance = Activator.CreateInstance(type.Value, "Hola");
                Assert.IsNotNull(instance);
            }
        }
    }
}