using HermEsb.Core.Handlers.Dynamic;
using System.Linq;
using HermEsb.Core.Test.Handlers.Dynamic.Domain;
using NUnit.Framework;

namespace HermEsb.Core.Test.Handlers.Dynamic
{
    [TestFixture]
    public class DerivedTypesFinderTest
    {
        
        [Test]
        public void ExtractDerivedTypes()
        {
            var fromType = DerivedTypesFinder.FromType(typeof (IDynamicBaseMessage));

            Assert.Contains(typeof(IDynamicChild1Level2), fromType.ToList());
            Assert.Contains(typeof(IDynamicChild1Level3), fromType.ToList());
        }
    }
}