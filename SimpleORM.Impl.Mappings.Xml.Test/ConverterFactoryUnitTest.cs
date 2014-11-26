using NUnit.Framework;
using SimpleORM.Converters;
using SimpleORM.Impl.Mappings.Xml.Factories;

namespace SimpleORM.Impl.Mappings.Xml.Test
{
    [TestFixture]
    public class ConverterFactoryUnitTest
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            //Register shorthands
            new XmlMappingBuilder();
        }

        [Test]
        public void ResolveShorthandYesNo()
        {
            Assert.AreEqual(ConverterFactory.Create("YN").GetType(), typeof(YesNoConverter));
        }

        [Test]
        public void ResolveShorthandLowerYesNo()
        {
            Assert.AreEqual(ConverterFactory.Create("yn").GetType(), typeof(LowerYesNoConverter));
        }

        [Test]
        public void ResolveShorthandTrueFalse()
        {
            Assert.AreEqual(ConverterFactory.Create("TF").GetType(), typeof(TrueFalseConverter));
        }

        [Test]
        public void ResolveShorthandLowerTrueFalse()
        {
            Assert.AreEqual(ConverterFactory.Create("tf").GetType(), typeof(LowerTrueFalseConverter));
        }
    }
}
