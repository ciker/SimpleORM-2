using System;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
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

            try
            {
                Type = TypeUtils.ParseType(typeString, true);
            }
            catch (Exception ex)
            {
                throw new DocumentParseException("Wrong discriminator column type", ex);
            }

            Column = xDiscriminator.Attribute("column").Value;
        }

        public Type Type { get; private set; }

        public string Column { get; private set; }
    }
}