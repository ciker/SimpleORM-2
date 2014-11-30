using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Factories;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Mappings;
using SimpleORM.Oracle.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Oracle.Mappings
{
    sealed class XmlObjectMapping : IObjectMapping
    {
        private static readonly XNamespace XNamespace = "urn:dbm-oracle-object-mapping";

        public XmlObjectMapping(XContainer xMapping)
        {
            var xObject = xMapping.Element(XNamespace + "object");

            Schema = xObject.Attribute("schema").GetAsString();
            Name = xObject.Attribute("name").Value;

            Type = xObject.Attribute("name").GetAsType();

            Properties = new List<IPropertyMapping>();
            foreach (var xProperty in xObject.Elements(XNamespace + "property"))
            {
                Properties.Add(new XmlObjectPropertyMapping(Type, xProperty));
            }
        }

        public string Name { get; private set; }

        public string Schema { get; private set; }

        public Type Type { get; private set; }

        public IList<IPropertyMapping> Properties { get; private set; }
    }

    sealed class XmlObjectPropertyMapping : IObjectPropertyMapping
    {
        public XmlObjectPropertyMapping(Type classType, XElement xObjectProperty)
        {
            Name = xObjectProperty.Attribute("attribute").Value;
            
            var name = xObjectProperty.Attribute("name").Value;

            Member = classType.GetMember(name, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault();

            if (Member == null)
                throw new DocumentParseException("Canot find member '{0}'", name);

            XAttribute xConverter;
            if (xObjectProperty.TryGetAttribute("converter", out xConverter))
            {
                Converter = ConverterFactory.Create(xConverter.Value);
            }
        }

        public IConverter Converter { get; set; }

        public string Name { get; private set; }

        public MemberInfo Member { get; private set; }
    }
}