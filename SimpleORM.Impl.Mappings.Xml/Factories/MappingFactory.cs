using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Factories
{
    internal static class MappingFactory
    {
        public static IMapping CreateMapping(XElement xMapping)
        {
            var documentType = xMapping.Name;

            if (documentType == "table-mapping")
            {
                var xClass = XmlUtils.Single(xMapping, "class");

                if (xClass == null)
                    throw new DocumentParseException("No class element");

                return new XmlTableMapping(xClass);
            }
            
            if (documentType == "view-mapping")
            {
                var xClass = XmlUtils.Single(xMapping, "class");

                if (xClass == null)
                    throw new DocumentParseException("No class element");

                return new XmlViewMapping(xClass);
            }

            throw new DocumentParseException("Unknown document type '{0}'", documentType);
        }
    }
}
