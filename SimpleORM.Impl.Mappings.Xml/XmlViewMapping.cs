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
    sealed class XmlViewMapping : IViewMapping
    {
        public XmlViewMapping(string classXml)
        {
            var xClass = XElement.Parse(classXml);

            Schema = XmlUtils.GetAsString(xClass, "@schema");
            Name = XmlUtils.GetAsString(xClass, "@table");

            Type = XmlUtils.GetAsType(xClass, "@name");

            Properties = new List<IPropertyMapping>();
            foreach (var xProperty in XmlUtils.Select(xClass, "property"))
            {
                Properties.Add(new XmlViewPropertyMapping(Type, xProperty));
            }

            var xDiscriminator = XmlUtils.Single(xClass, "discriminator");
            if (xDiscriminator != null)
                Discriminator = new XmlDiscriminatorColumn(xDiscriminator);

            SubClasses = new List<ISubClassMapping>();
            foreach (var xSubClass in XmlUtils.Select(xClass, "subclass"))
            {
                SubClasses.Add(new XmlSubClassMapping(this, xSubClass));
            }
        }


        public string Name { get; private set; }

        public string Schema { get; private set; }

        public Type Type { get; private set; }

        public IList<IPropertyMapping> Properties { get; private set; }

        public IDiscriminatorColumn Discriminator { get; private set; }

        public IList<ISubClassMapping> SubClasses { get; private set; }
    }

    sealed class XmlViewPropertyMapping : IViewPropertyMapping
    {
        public XmlViewPropertyMapping(Type classType, XElement xTableProperty)
        {
            Name = XmlUtils.GetAsString(xTableProperty, "@name");

            Member = classType.GetMember(Name, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault();

            if (Member == null)
                throw new DocumentParseException("Canot find member '{0}'", Name);

            if (XmlUtils.Exists(xTableProperty, "@converter"))
            {
                var converterTypeString = XmlUtils.GetAsString(xTableProperty, "@converter");
                Converter = PropertyTypeConverterFactory.Create(converterTypeString);
            }
        }


        public string Name { get; private set; }

        public MemberInfo Member { get; private set; }

        public IPropertyTypeConverter Converter { get; set; }
    }
}