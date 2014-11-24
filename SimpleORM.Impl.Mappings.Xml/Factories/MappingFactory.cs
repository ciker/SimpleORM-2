using System.Collections.Generic;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Factories
{
    public delegate IMapping MappingBuilder(XElement xMapping);

    internal static class MappingFactory
    {
        private static readonly IDictionary<string, MappingBuilder> MappingBuilders = new Dictionary<string, MappingBuilder>();

        internal static void RegisterMapping(string documentType, MappingBuilder mappingBuilder)
        {
            MappingBuilders[documentType] = mappingBuilder;
        }

        public static IMapping CreateMapping(XElement xMapping)
        {
            var documentType = xMapping.Name;

            MappingBuilder mappingBuilder;
            if (!MappingBuilders.TryGetValue(documentType.LocalName, out mappingBuilder))
                throw new DocumentParseException("Unknown document type '{0}'", documentType);

            return mappingBuilder(xMapping);
        }
    }
}
