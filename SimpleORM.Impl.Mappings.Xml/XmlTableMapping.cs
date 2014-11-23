using System;
using System.Collections.Generic;
using System.Linq;
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
                Properties.Add(new XmlTablePropertyMapping(Type, xProperty));
            }

            var xDiscriminator = XmlUtils.Single(xClass, "discriminator");
            if (xDiscriminator != null)
                Discriminator = new XmlDiscriminatorColumn(xDiscriminator);

            SubClasses = new List<ISubClassMapping>();
            foreach (var xSubClass in XmlUtils.Select(xClass, "subclass"))
            {
                SubClasses.Add(new XmlSubClassMapping(this, xSubClass));
            }

            var xVersionProperty = XmlUtils.Single(xClass, "version");
            if (xVersionProperty != null)
                VersionProperty = new XmlVersionProperty(this, xVersionProperty);

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
}