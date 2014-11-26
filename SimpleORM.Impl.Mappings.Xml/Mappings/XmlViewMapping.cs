using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Factories;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Mappings
{
    sealed class XmlViewMapping : IViewMapping
    {
        public XmlViewMapping(XElement xMapping)
        {
            var xClass = XmlUtils.Single(xMapping, "class");

            if (xClass == null)
                throw new DocumentParseException("No class element");

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
            Name = XmlUtils.GetAsString(xTableProperty, "@column");
            
            var name = XmlUtils.GetAsString(xTableProperty, "@name");

            Member = classType.GetMember(name, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault();

            if (Member == null)
                throw new DocumentParseException("Canot find member '{0}'", name);

            if (XmlUtils.Exists(xTableProperty, "@converter"))
            {
                var converterTypeString = XmlUtils.GetAsString(xTableProperty, "@converter");
                Converter = ConverterFactory.Create(converterTypeString);
            }
        }


        public string Name { get; private set; }

        public MemberInfo Member { get; private set; }

        public IConverter Converter { get; set; }
    }
}