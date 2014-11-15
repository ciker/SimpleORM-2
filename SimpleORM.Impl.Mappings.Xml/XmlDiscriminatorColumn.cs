using System.Xml.Linq;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml
{
    sealed class XmlDiscriminatorColumn : IDiscriminatorColumn
    {
        public XmlDiscriminatorColumn(XElement xDiscriminator)
        {
        }

        public string Column { get; private set; }

        public IHasType HasType { get; private set; }
    }
}