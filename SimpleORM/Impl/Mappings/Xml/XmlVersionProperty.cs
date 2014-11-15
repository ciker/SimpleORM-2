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

        public ITypeMapping TypeMapping { get; private set; }

        public string Name { get; private set; }

        public MemberInfo Member { get; private set; }
    }
}