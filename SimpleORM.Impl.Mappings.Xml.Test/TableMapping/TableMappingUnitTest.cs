using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using NUnit.Framework;
using SimpleORM.Converters;
using SimpleORM.Generators;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Factories;
using SimpleORM.Impl.Mappings.Xml.Mappings;
using SimpleORM.Impl.Mappings.Xml.Test.TableMapping.Model;
using SimpleORM.Mappings;

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
        [ExpectedException(typeof(DocumentParseException))]
        public void NoProperties()
        {
            var document = GetMappingDocument("NoProperties.xml");
            MappingFactory.CreateMapping(document);
        }

        [Test]
        [ExpectedException(typeof(DocumentParseException))]
        public void NoTableName()
        {
            var document = GetMappingDocument("NoTableName.xml");
            MappingFactory.CreateMapping(document);
        }

        [Test]
        [ExpectedException(typeof(DocumentParseException))]
        public void NoClassName()
        {
            var document = GetMappingDocument("NoClassName.xml");
            MappingFactory.CreateMapping(document);
        }

        [Test]
        public void NoSchema()
        {
            var document = GetMappingDocument("NoSchema.xml");
            var mapping = MappingFactory.CreateMapping(document);

            Assert.AreEqual(mapping.Schema, null);
        }

        [Test]
        public void NoDiscriminatorValue()
        {
            var document = GetMappingDocument("CheckClassAttributes.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);

            Assert.AreEqual(mapping.DiscriminatorValue, null);
        }

        [Test]
        public void CheckSchemaName()
        {
            var document = GetMappingDocument("CheckClassAttributes.xml");
            var mapping = MappingFactory.CreateMapping(document);

            Assert.AreEqual(mapping.Schema, "test_dbm");
        }

        [Test]
        public void CheckTableName()
        {
            var document = GetMappingDocument("CheckClassAttributes.xml");
            var mapping = MappingFactory.CreateMapping(document);

            Assert.AreEqual(mapping.Name, "shapes");
        }

        [Test]
        public void CheckClassName()
        {
            var document = GetMappingDocument("CheckClassAttributes.xml");
            var mapping = MappingFactory.CreateMapping(document);

            Assert.AreEqual(mapping.Type, typeof(Shape));
        }

        [Test]
        public void CheckMappingType()
        {
            var document = GetMappingDocument("CheckClassAttributes.xml");
            var mapping = MappingFactory.CreateMapping(document);

            Assert.AreEqual(mapping.GetType(), typeof(XmlTableMapping));
        }

        [Test]
        [ExpectedException(typeof(DocumentParseException))]
        public void NoPropertyName()
        {
            var document = GetMappingDocument("NoPropertyName.xml");
            MappingFactory.CreateMapping(document);
        }

        [Test]
        [ExpectedException(typeof(DocumentParseException))]
        public void NoPropertyColumn()
        {
            var document = GetMappingDocument("NoPropertyColumn.xml");
            MappingFactory.CreateMapping(document);
        }

        [Test]
        public void CheckPropertyType()
        {
            var document = GetMappingDocument("CheckPropertyOptionalAttributes.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = mapping.Properties.First();
            Assert.AreEqual(idProperty.GetType(), typeof(XmlTablePropertyMapping));
        }

        [Test]
        public void NoPropertyInsert()
        {
            var document = GetMappingDocument("CheckPropertyOptionalAttributes.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            Assert.AreEqual(idProperty.Insert, true);
        }

        [Test]
        public void NoPropertyUpdate()
        {
            var document = GetMappingDocument("CheckPropertyOptionalAttributes.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            Assert.AreEqual(idProperty.Update, true);
        }

        [Test]
        public void NoPropertyConverter()
        {
            var document = GetMappingDocument("CheckPropertyOptionalAttributes.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            Assert.AreEqual(idProperty.Converter, null);
        }

        [Test]
        public void NoPropertyGenerator()
        {
            var document = GetMappingDocument("CheckPropertyOptionalAttributes.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            Assert.AreEqual(idProperty.Generator, null);
        }

        [Test]
        public void CheckPropertyInsertTrue()
        {
            var document = GetMappingDocument("CheckPropertyInsertTrue.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            Assert.AreEqual(idProperty.Insert, true);
        }

        [Test]
        public void CheckPropertyInsertFalse()
        {
            var document = GetMappingDocument("CheckPropertyInsertFalse.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            Assert.AreEqual(idProperty.Insert, false);
        }

        [Test]
        public void CheckPropertyInsertTrue1()
        {
            var document = GetMappingDocument("CheckPropertyInsertTrue1.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            Assert.AreEqual(idProperty.Insert, true);
        }

        [Test]
        public void CheckPropertyInsertFalse0()
        {
            var document = GetMappingDocument("CheckPropertyInsertFalse0.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            Assert.AreEqual(idProperty.Insert, false);
        }

        [Test]
        public void CheckPropertyPublicFieldReference()
        {
            var document = GetMappingDocument("CheckPropertyPublicFieldReference.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            var fieldInfo = typeof(PropertyFieldReference).GetMember("Field", BindingFlags.Instance | BindingFlags.Public).First();
            Assert.AreEqual(idProperty.Member, fieldInfo);
        }

        [Test]
        public void CheckPropertyPrivateFieldReference()
        {
            var document = GetMappingDocument("CheckPropertyPrivateFieldReference.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            var fieldInfo = typeof(PropertyFieldReference).GetMember("_field", BindingFlags.Instance | BindingFlags.NonPublic).First();
            Assert.AreEqual(idProperty.Member, fieldInfo);
        }

        [Test]
        [ExpectedException(typeof(DocumentParseException))]
        public void CheckPropertyStaticFieldReference()
        {
            var document = GetMappingDocument("CheckPropertyStaticFieldReference.xml");
            MappingFactory.CreateMapping(document);
        }

        [Test]
        public void CheckPropertyPublicPropertyReference()
        {
            var document = GetMappingDocument("CheckPropertyPublicPropertyReference.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            var fieldInfo = typeof(PropertyPropertyReference).GetMember("PublicProperty", BindingFlags.Instance | BindingFlags.Public).First();
            Assert.AreEqual(idProperty.Member, fieldInfo);
        }

        [Test]
        public void CheckPropertyPrivatePropertyReference()
        {
            var document = GetMappingDocument("CheckPropertyPrivatePropertyReference.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            var fieldInfo = typeof(PropertyPropertyReference).GetMember("PrivateProperty", BindingFlags.Instance | BindingFlags.NonPublic).First();
            Assert.AreEqual(idProperty.Member, fieldInfo);
        }

        [Test]
        [ExpectedException(typeof(Exception))] //TODO -- here should be some core exception
        public void CheckPropertyNoSetterPropertyReference()
        {
            var document = GetMappingDocument("CheckPropertyNoSetterPropertyReference.xml");
            MappingFactory.CreateMapping(document);
        }

        [Test]
        [ExpectedException(typeof(Exception))] //TODO -- here should be some core exception
        public void CheckPropertyNoGetterPropertyReference()
        {
            var document = GetMappingDocument("CheckPropertyNoGetterPropertyReference.xml");
            MappingFactory.CreateMapping(document);
        }

        [Test]
        [ExpectedException(typeof(DocumentParseException))]
        public void CheckPropertyStaticPropertyReference()
        {
            var document = GetMappingDocument("CheckPropertyStaticPropertyReference.xml");
            MappingFactory.CreateMapping(document);
        }

        [Test]
        public void CheckPropertyConverterShorthand()
        {
            var document = GetMappingDocument("CheckPropertyConverterShorthand.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            Assert.AreEqual(idProperty.Converter.GetType(), typeof(YesNoConverter));
        }

        [Test]
        public void CheckPropertyConverter()
        {
            var document = GetMappingDocument("CheckPropertyConverter.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            Assert.AreEqual(idProperty.Converter.GetType(), typeof(LowerYesNoConverter));
        }

        [Test]
        [ExpectedException(typeof(DocumentParseException))]
        public void CheckPropertyPseudoConverter()
        {
            var document = GetMappingDocument("CheckPropertyPseudoConverter.xml");
            MappingFactory.CreateMapping(document);
        }

        [Test]
        [ExpectedException(typeof(DocumentParseException))]
        public void CheckPropertyUnknownConverter()
        {
            var document = GetMappingDocument("CheckPropertyWrongConverter.xml");
            MappingFactory.CreateMapping(document);
        }

        [Test]
        [ExpectedException(typeof(DocumentParseException))]
        public void CheckPropertyEmptyGenerator()
        {
            var document = GetMappingDocument("CheckPropertyEmptyGenerator.xml");
            MappingFactory.CreateMapping(document);
        }

        [Test]
        [ExpectedException(typeof(DocumentParseException))]
        public void CheckPropertyWrongGenerator()
        {
            var document = GetMappingDocument("CheckPropertyWrongGenerator.xml");
            MappingFactory.CreateMapping(document);
        }

        [Test]
        public void CheckPropertyDbAssignedGenerator()
        {
            var document = GetMappingDocument("CheckPropertyDbAssignedGenerator.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            Assert.AreEqual(idProperty.Generator.GetType(), typeof(DbAssignedGenerator));
        }

        [Test]
        public void CheckPropertySequenceGenerator()
        {
            var document = GetMappingDocument("CheckPropertySequenceGenerator.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            Assert.AreEqual(idProperty.Generator.GetType(), typeof(SequenceGenerator));
        }

        [Test]
        public void CheckPropertySequenceGeneratorName()
        {
            var document = GetMappingDocument("CheckPropertySequenceGenerator.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            var sequenceGenerator = (SequenceGenerator)idProperty.Generator;
            Assert.AreEqual(sequenceGenerator.Name, "seq_shapes");
        }

        [Test]
        [ExpectedException(typeof(DocumentParseException))]
        public void NoSequenceGeneratorName()
        {
            var document = GetMappingDocument("NoSequenceGeneratorName.xml");
            var mapping = (ITableMapping)MappingFactory.CreateMapping(document);
            var idProperty = (ITablePropertyMapping)mapping.Properties.First();
            var sequenceGenerator = (SequenceGenerator)idProperty.Generator;
            Assert.AreEqual(sequenceGenerator.Name, "seq_shapes");
        }
    }
}
