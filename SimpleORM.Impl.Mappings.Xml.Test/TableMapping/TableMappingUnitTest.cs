using System;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Schema;
using NUnit.Framework;
using SimpleORM.Impl.Mappings.Xml.Factories;

namespace SimpleORM.Impl.Mappings.Xml.Test.TableMapping
{
    [TestFixture]
    class TableMappingUnitTest
    {
        private Assembly _currentAssembly;

        [TestFixtureSetUp]
        public void Setup()
        {
            //Register builders and schemas
            new XmlMappingBuilder();

            _currentAssembly = Assembly.GetAssembly(typeof(TableMappingUnitTest));
        }

        private XDocument GetMappingDocument(string resourceName)
        {
            resourceName = "SimpleORM.Impl.Mappings.Xml.Test.TableMapping.Resources." + resourceName;
            var stream = _currentAssembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new Exception(string.Format("Cannot find resource '{0}'", resourceName));

            return XDocument.Load(stream);
        }

        [Test]
        [ExpectedException(typeof(XmlSchemaValidationException))]
        public void NoProperties()
        {
            var document = GetMappingDocument("NoProperties.xml");
            MappingFactory.CreateMapping(document);
        }

        [Test]
        public void NoClassSchema()
        {
            var document = GetMappingDocument("NoClassSchema.xml");
            MappingFactory.CreateMapping(document);
        }
    }
}
