using System;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Factories;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Mappings
{
    sealed class XmlDiscriminatorColumn : IDiscriminatorColumn
    {
        public XmlDiscriminatorColumn(XElement xDiscriminator)
        {
            var typeString = XmlUtils.GetAsString(xDiscriminator, "@type");

            if (string.IsNullOrEmpty(typeString))
                throw new DocumentParseException("Discriminator type is empty");

            Type = TypeUtils.ParseType(typeString, true);

            var converterTypeString = XmlUtils.GetAsString(xDiscriminator, "@converter");
            if (converterTypeString != null)
                Converter = PropertyTypeConverterFactory.Create(converterTypeString);

            Column = XmlUtils.GetAsString(xDiscriminator, "@column");
        }

        public Type Type { get; private set; }

        public IPropertyTypeConverter Converter { get; set; }

        public string Column { get; private set; }
    }
}