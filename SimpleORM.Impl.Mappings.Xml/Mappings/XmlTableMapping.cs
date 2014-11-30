using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Mappings
{
    sealed class XmlTableMapping : ITableMapping
    {
        private static readonly XNamespace XNamespace = "urn:dbm-table-mapping";

        public XmlTableMapping(XContainer xMapping)
        {
            var xClass = xMapping.Element(XNamespace + "class");

            Schema = xClass.Attribute("schema").GetAsString();
            Name = xClass.Attribute("name").Value;

            Type = xClass.Attribute("class").GetAsType();

            Properties = new List<IPropertyMapping>();
            foreach (var xProperty in xClass.Elements(XNamespace + "property"))
            {
                Properties.Add(new XmlTablePropertyMapping(Type, XNamespace, xProperty));
            }

            XElement xDiscriminator;
            if (xClass.TryGetElement(XNamespace + "discriminator", out xDiscriminator))
            {
                Discriminator = new XmlDiscriminatorColumn(xDiscriminator);
            }

            SubClasses = new List<ISubClassMapping>();
            foreach (var xSubClass in xClass.Elements(XNamespace + "subclass"))
            {
                SubClasses.Add(new XmlSubClassMapping(this, XNamespace, xSubClass));
            }

            XAttribute xDiscriminatorValue;
            if (xClass.TryGetAttribute("discriminator-value", out xDiscriminatorValue))
            {
                DiscriminatorValue = TypeUtils.ParseAs(Discriminator.Type, xDiscriminatorValue.Value);
            }

            XElement xVersionProperty;
            if (xClass.TryGetElement(XNamespace + "version", out xVersionProperty))
            {
                VersionProperty = new XmlVersionProperty(this, xVersionProperty);
            }

            XElement xPrimaryKey;
            if (!xClass.TryGetElement(XNamespace + "primary-key", out xPrimaryKey)) 
                return;

            PrimaryKeyProperties = new List<IPropertyMapping>();

            var properties = Properties.ToDictionary(p => p.Name);

            foreach (var xProperty in xPrimaryKey.Elements(XNamespace + "property"))
            {
                var name = xProperty.Attribute("name").Value;

                IPropertyMapping propertyMapping;
                if (!properties.TryGetValue(name, out propertyMapping))
                    throw new DocumentParseException("Cannot find primary key property '{0}'", name);

                PrimaryKeyProperties.Add(propertyMapping);
            }
        }

        public string Name { get; private set; }

        public string Schema { get; private set; }

        public Type Type { get; private set; }

        public IList<IPropertyMapping> Properties { get; private set; }

        public IDiscriminatorColumn Discriminator { get; private set; }

        public IList<ISubClassMapping> SubClasses { get; private set; }

        public object DiscriminatorValue { get; private set; }

        public IVersionProperty VersionProperty { get; private set; }

        public IList<IPropertyMapping> PrimaryKeyProperties { get; private set; }
    }
}