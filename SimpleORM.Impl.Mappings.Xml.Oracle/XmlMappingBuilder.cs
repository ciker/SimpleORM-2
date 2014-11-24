using SimpleORM.Impl.Mappings.Xml.Oracle.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Oracle
{
    public class XmlMappingBuilder : Xml.XmlMappingBuilder
    {
        public XmlMappingBuilder()
        {
            RegisterMappingBuilder("object-mapping", xMapping => new XmlObjectMapping(xMapping));
            RegisterMappingBuilder("object-table-mapping", xMapping => new XmlObjectTableMapping(xMapping));
        }
    }
}