using System;
using System.Reflection;
using System.Xml.Linq;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml
{
    sealed class XmlVersionProperty : IVersionProperty
    {
        public XmlVersionProperty(XElement xVersion)
        {
        }

        public string Name { get; private set; }

        public MemberInfo Member { get; private set; }

        public Type Type { get; private set; }

        public string DbType { get; private set; }

        public int? Length { get; private set; }
    }
}