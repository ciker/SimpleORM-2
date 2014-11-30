using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Mappings
{
    sealed class XmlSubClassMapping : ISubClassMapping
    {
        public XmlSubClassMapping(IHasDiscriminatorColumn tableMapping, XNamespace xNamespace, XElement xSubClass)
        {
            Type = xSubClass.Attribute("name").GetAsType();

            Properties = new List<IPropertyMapping>();

            foreach (var xProperty in xSubClass.Elements(xNamespace + "property"))
            {
                Properties.Add(new XmlTablePropertyMapping(Type, xNamespace, xProperty));
            }

            XAttribute xDiscriminatorValue;
            if (xSubClass.TryGetAttribute("discriminator-value", out xDiscriminatorValue))
            {
                DiscriminatorValue = TypeUtils.ParseAs(tableMapping.Discriminator.Type, xDiscriminatorValue.Value);
            }

            SubClasses = new List<ISubClassMapping>();
            foreach (var xSubSubClass in xSubClass.Elements(xNamespace + "subclass"))
            {
                SubClasses.Add(new XmlSubClassMapping(tableMapping, xNamespace, xSubSubClass));
            }

            XElement xSubClassJoin;
            if (xSubClass.TryGetElement(xNamespace + "xSubClass", out xSubClassJoin))
            {
                Join = new XmlSubClassJoin(xNamespace, xSubClassJoin);
            }
        }

        public Type Type { get; private set; }

        public IList<IPropertyMapping> Properties { get; private set; }

        public IList<ISubClassMapping> SubClasses { get; private set; }

        public object DiscriminatorValue { get; private set; }

        public ISubClassJoin Join { get; private set; }
    }

    sealed class XmlSubClassJoin : ISubClassJoin
    {
        public XmlSubClassJoin(XNamespace xNamespace, XElement xSubClassJoin)
        {
            Schema = xSubClassJoin.Attribute("schema").GetAsString();
            Name = xSubClassJoin.Attribute("table").Value;

            ColumnJoins = new List<ISubClassJoinColumn>();
            foreach (var xColumn in xSubClassJoin.Elements(xNamespace + "column"))
            {
                ColumnJoins.Add(new XmlSubClassJoinColumn(xColumn));
            }
        }

        public string Schema { get; private set; }

        public string Name { get; private set; }

        public IList<ISubClassJoinColumn> ColumnJoins { get; private set; }
    }

    sealed class XmlSubClassJoinColumn : ISubClassJoinColumn
    {
        public XmlSubClassJoinColumn(XElement xSubClassJoinColumn)
        {
            Name = xSubClassJoinColumn.Attribute("name").Value;
            JoinSchema = xSubClassJoinColumn.Attribute("join-schema").GetAsString();
            JoinTable = xSubClassJoinColumn.Attribute("join-table").Value;
            JoinColumn = xSubClassJoinColumn.Attribute("join-column").Value;
        }

        public string Name { get; private set; }

        public string JoinSchema { get; private set; }

        public string JoinTable { get; private set; }

        public string JoinColumn { get; private set; }
    }
}