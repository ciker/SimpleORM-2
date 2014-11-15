using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml
{
    sealed class XmlSubClassMapping : ISubClassMapping
    {
        public XmlSubClassMapping(XElement xSubClass)
        {
        }

        public Type Type { get; private set; }

        public IList<IPropertyMapping> Properties { get; private set; }

        public IDiscriminatorColumn Discriminator { get; private set; }

        public IList<ISubClassMapping> SubClasses { get; private set; }

        public object DiscriminatorValue { get; private set; }

        public ISubClassJoin Join { get; private set; }
    }

    sealed class XmlSubClassJoin : ISubClassJoin
    {
        public XmlSubClassJoin(XElement xSubClassJoin)
        {
        }

        public string Schema { get; private set; }

        public string Table { get; private set; }

        public IList<ISubClassJoinColumn> ColumnJoins { get; private set; }
    }

    sealed class XmlSubClassJoinColumn : ISubClassJoinColumn
    {
        public XmlSubClassJoinColumn(XElement xSubClassJoinColumn)
        {
        }

        public string Name { get; private set; }

        public string JoinSchema { get; private set; }

        public string JoinTable { get; private set; }

        public string JoinColumn { get; private set; }
    }
}