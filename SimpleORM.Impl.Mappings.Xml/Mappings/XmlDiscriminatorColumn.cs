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
            var typeString = xDiscriminator.Attribute("type").Value;

            if (string.IsNullOrEmpty(typeString))
                throw new DocumentParseException("Discriminator type is empty");

            Type = TypeUtils.ParseType(typeString, true);

            XAttribute xConverter;
            if (xDiscriminator.TryGetAttribute("converter", out xConverter))
            {
                Converter = ConverterFactory.Create(xConverter.Value);
            }

            Column = xDiscriminator.Attribute("column").Value;
        }

        public Type Type { get; private set; }

        public IConverter Converter { get; set; }

        public string Column { get; private set; }
    }
}