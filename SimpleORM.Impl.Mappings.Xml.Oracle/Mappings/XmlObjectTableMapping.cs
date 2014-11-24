using System;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Oracle.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Oracle.Mappings
{
    sealed class XmlObjectTableMapping : IObjectTableMapping
    {
        public XmlObjectTableMapping(XElement xMapping)
        {
            var xClass = XmlUtils.Single(xMapping, "class");

            if (xClass == null)
                throw new DocumentParseException("No class element");

            Schema = XmlUtils.GetAsString(xClass, "@schema");
            Name = XmlUtils.GetAsString(xClass, "@table");

            Type = XmlUtils.GetAsType(xClass, "@name");

            var objectTypeString = XmlUtils.GetAsString(xClass, "@object-type");

            ObjectType = TypeUtils.ParseType(objectTypeString, false);
        }

        public string Name { get; private set; }

        public string Schema { get; private set; }

        public Type Type { get; private set; }

        public Type ObjectType { get; set; }
    }
}