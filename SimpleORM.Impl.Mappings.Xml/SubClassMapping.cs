using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml
{
    sealed class XmlSubClassMapping : ISubClassMapping
    {
        public XmlSubClassMapping(IHasDiscriminatorColumn tableMapping, XElement xSubClass)
        {
            Type = XmlUtils.GetAsType(xSubClass, "@name");

            Properties = new List<IPropertyMapping>();
            foreach (var xProperty in XmlUtils.Select(xSubClass, "property"))
            {
                Properties.Add(new XmlTablePropertyMapping(Type, xProperty));
            }

            var xDiscriminatorValue = XmlUtils.Single(xSubClass, "@discriminator-value");
            if (xDiscriminatorValue != null)
            {
                var discriminatorValue = XmlUtils.GetAsString(xSubClass, "@discriminator-value");
                DiscriminatorValue = TypeUtils.ParseAs(tableMapping.Discriminator.Type, discriminatorValue);
            }

            SubClasses = new List<ISubClassMapping>();
            foreach (var xSubSubClass in XmlUtils.Select(xSubClass, "subclass"))
            {
                SubClasses.Add(new XmlSubClassMapping(tableMapping, xSubSubClass));
            }

            var xSubClassJoin = XmlUtils.Single(xSubClass, "join");
            if (xSubClassJoin != null)
            {
                Join = new XmlSubClassJoin(xSubClassJoin);
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
        public XmlSubClassJoin(XElement xSubClassJoin)
        {
            Schema = XmlUtils.GetAsString(xSubClassJoin, "@schema");
            Name = XmlUtils.GetAsString(xSubClassJoin, "@table");

            ColumnJoins = new List<ISubClassJoinColumn>();
            foreach (var xColumn in XmlUtils.Select(xSubClassJoin, "column"))
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
            Name = XmlUtils.GetAsString(xSubClassJoinColumn, "@name");
            JoinSchema = XmlUtils.GetAsString(xSubClassJoinColumn, "@join-schema");
            JoinTable = XmlUtils.GetAsString(xSubClassJoinColumn, "@join-table");
            JoinColumn = XmlUtils.GetAsString(xSubClassJoinColumn, "@join-column");
        }

        public string Name { get; private set; }

        public string JoinSchema { get; private set; }

        public string JoinTable { get; private set; }

        public string JoinColumn { get; private set; }
    }
}