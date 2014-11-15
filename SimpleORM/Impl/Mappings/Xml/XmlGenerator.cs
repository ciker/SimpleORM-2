using System;
using System.Xml.Linq;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml
{
    sealed class XmlSequenceGenerator : ISequenceGenerator
    {
        public XmlSequenceGenerator(XElement xSequenceGenerator)
        {
        }

        public Type Type { get; private set; }

        public string Name { get; private set; }
    }

    sealed class XmlDbAssignedGenerator : IDbAssignedGenerator
    {
        public XmlDbAssignedGenerator(XElement xDbAssignedGenerator)
        {
        }

        public Type Type { get; private set; }
    }
}