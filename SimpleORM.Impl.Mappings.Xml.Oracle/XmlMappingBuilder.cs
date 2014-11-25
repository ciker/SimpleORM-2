using SimpleORM.Impl.Mappings.Xml.Oracle.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Oracle
{
    public class XmlMappingBuilder : Xml.XmlMappingBuilder
    {
        static XmlMappingBuilder()
        {
            RegisterMappingBuilder("object-mapping", xMapping => new XmlObjectMapping(xMapping));
            RegisterMappingBuilder("object-table-mapping", xMapping => new XmlObjectTableMapping(xMapping));
            
            RegisterMappingBuilder("function-mapping", xMapping => new XmlFunctionMapping(xMapping));
            RegisterMappingBuilder("procedure-mapping", xMapping => new XmlProcedureMapping(xMapping));
        }
    }
}