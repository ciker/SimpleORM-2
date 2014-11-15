using System.Configuration;
using System.Xml;

namespace SimpleORM.Configuration
{
    public sealed class SimpleORMSection : IConfigurationSectionHandler
    {
        public const string Name = "simple-orm";

        public object Create(object parent, object configContext, XmlNode section)
        {
            return section;
        }
    }
}
