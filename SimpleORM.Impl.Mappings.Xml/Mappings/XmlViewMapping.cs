using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Mappings
{
    sealed class XmlViewMapping : IViewMapping
    {
        private static readonly XNamespace XNamespace = "urn:dbm-view-mapping";

        public XmlViewMapping(XContainer xMapping)
        {
            var xClass = xMapping.Element(XNamespace + "class");

            Schema = xClass.Attribute("schema").GetAsString();
            Name = xClass.Attribute("name").Value;

            Type = xClass.Attribute("class").GetAsType();

            Properties = new List<IPropertyMapping>();
            foreach (var xProperty in xClass.Elements(XNamespace + "property"))
            {
                Properties.Add(new XmlViewPropertyMapping(Type, xProperty));
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
        }

        public string Name { get; private set; }

        public string Schema { get; private set; }

        public Type Type { get; private set; }

        public IList<IPropertyMapping> Properties { get; private set; }

        public IDiscriminatorColumn Discriminator { get; private set; }

        public IList<ISubClassMapping> SubClasses { get; private set; }

        public object DiscriminatorValue { get; private set; }
    }
}