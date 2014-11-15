using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml
{
    sealed class XmlTableMapping : ITableMapping
    {
        public XmlTableMapping(string classXml)
        {
            var xClass = XElement.Parse(classXml);

            Schema = XmlUtils.GetAsString(xClass, "@schema");
            Name = XmlUtils.GetAsString(xClass, "@table");

            Type = XmlUtils.GetAsType(xClass, "@name");

            Properties = new List<IPropertyMapping>();
            foreach (var xProperty in XmlUtils.Select(xClass, "property"))
            {
                Properties.Add(new XmlTablePropertyMapping(xProperty));
            }

            var xDiscriminator = XmlUtils.Single(xClass, "discriminator");
            if (xDiscriminator != null)
                Discriminator = new XmlDiscriminatorColumn(xDiscriminator);

            SubClasses = new List<ISubClassMapping>();
            foreach (var xSubClass in XmlUtils.Select(xClass, "subclass"))
            {
                SubClasses.Add(new XmlSubClassMapping(xSubClass));
            }

            var xVersionProperty = XmlUtils.Single(xClass, "version");
            if (xVersionProperty != null)
                VersionProperty = new XmlVersionProperty(xVersionProperty);

            var xPrimaryKey = XmlUtils.Single(xClass, "primary-key");
            if (xPrimaryKey != null)
            {
                PrimaryKeyProperties = new List<IPropertyMapping>();

                var properties = Properties.ToDictionary(p => p.Name);

                foreach (var xProperty in XmlUtils.Select(xPrimaryKey, "property"))
                {
                    var name = XmlUtils.GetAsString(xProperty, "@name");

                    IPropertyMapping propertyMapping;
                    if (!properties.TryGetValue(name, out propertyMapping))
                        throw new DocumentParseException("Cannot find primary key property '{0}'", name);

                    PrimaryKeyProperties.Add(propertyMapping);
                }
            }
        }

        public string Name { get; private set; }

        public string Schema { get; private set; }

        public Type Type { get; private set; }

        public IList<IPropertyMapping> Properties { get; private set; }

        public IDiscriminatorColumn Discriminator { get; private set; }

        public IList<ISubClassMapping> SubClasses { get; private set; }

        public IVersionProperty VersionProperty { get; private set; }

        public IList<IPropertyMapping> PrimaryKeyProperties { get; private set; }
    }

    sealed class XmlTablePropertyMapping : ITablePropertyMapping
    {
        public XmlTablePropertyMapping(XElement xTableProperty)
        {
            Name = XmlUtils.GetAsString(xTableProperty, "@name");

            var xGenerator = XmlUtils.Single(xTableProperty, "generator");
            if (xGenerator != null)
            {
                var xGeneratorElement = XmlUtils.Single(xGenerator, "*");
                var generatorType = xGeneratorElement.Name;

//                Generator = new XmlDiscriminatorColumn(xGenerator);
            }

            Insert = XmlUtils.GetAsBoolean(xTableProperty, "@insert", true);
            Update = XmlUtils.GetAsBoolean(xTableProperty, "@insert", true);
        }

        public IHasType HasType { get; private set; }

        public string Name { get; private set; }

        public MemberInfo Member { get; private set; }

        public IGenerator Generator { get; private set; }

        public bool Insert { get; private set; }

        public bool Update { get; private set; }

        public Type Type { get; private set; }

        public string DbType { get; private set; }

        public int? Length { get; private set; }
    }
}
