using System;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Oracle.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Oracle.Mappings
{
    sealed class XmlObjectTableMapping : IObjectTableMapping
    {
        private static readonly XNamespace XNamespace = "urn:dbm-oracle-object-table-mapping";

        public XmlObjectTableMapping(XElement xMapping)
        {
            var xObjectTable = xMapping.Element(XNamespace + "object-table");

            Schema = xObjectTable.Attribute("schema").GetAsString();
            Name = xObjectTable.Attribute("name").Value;

            Type = xObjectTable.Attribute("class").GetAsType();

            var objectTypeString = xObjectTable.Attribute("object-type").Value;

            ObjectType = TypeUtils.ParseType(objectTypeString, false);
        }

        public string Name { get; private set; }

        public string Schema { get; private set; }

        public Type Type { get; private set; }

        public Type ObjectType { get; set; }
    }
}