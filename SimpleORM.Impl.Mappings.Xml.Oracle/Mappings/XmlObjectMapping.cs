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
        public XmlObjectMapping(XDocument xMapping)
        {
            var xClass = XmlUtils.Single(xMapping.Root, "class");

            if (xClass == null)
                throw new DocumentParseException("No class element");

            Schema = XmlUtils.GetAsString(xClass, "@schema");
            Name = XmlUtils.GetAsString(xClass, "@table");

            Type = XmlUtils.GetAsType(xClass, "@name");

            Properties = new List<IPropertyMapping>();
            foreach (var xProperty in XmlUtils.Select(xClass, "property"))
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
            Name = XmlUtils.GetAsString(xObjectProperty, "@attribute");
            
            var name = XmlUtils.GetAsString(xObjectProperty, "@name");

            Member = classType.GetMember(name, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault();

            if (Member == null)
                throw new DocumentParseException("Canot find member '{0}'", name);

            if (XmlUtils.Exists(xObjectProperty, "@converter"))
            {
                var converterTypeString = XmlUtils.GetAsString(xObjectProperty, "@converter");
                Converter = ConverterFactory.Create(converterTypeString);
            }
        }

        public IConverter Converter { get; set; }

        public string Name { get; private set; }

        public MemberInfo Member { get; private set; }
    }
}