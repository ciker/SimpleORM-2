using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Factories
{
    public delegate IMapping MappingBuilder(XDocument xMapping);

    internal static class MappingFactory
    {
        private static readonly XmlSchemaSet Schemas = new XmlSchemaSet();

        private static readonly IDictionary<string, MappingBuilder> MappingBuilders = new Dictionary<string, MappingBuilder>();

        internal static void RegisterMapping(string documentType, MappingBuilder mappingBuilder)
        {
            //Schemas.Add(XmlSchema.Read(stream, (sender, args) => ));            
            MappingBuilders[documentType] = mappingBuilder;
        }

        public static IMapping CreateMapping(XDocument xMapping)
        {
            var rootElement = xMapping.Root;

            if (rootElement == null)
                throw new Exception("Root element is not found");

            var documentType = rootElement.Name;

            MappingBuilder mappingBuilder;
            if (!MappingBuilders.TryGetValue(documentType.LocalName, out mappingBuilder))
                throw new DocumentParseException("Unknown document type '{0}'", documentType);

            return mappingBuilder(xMapping);
        }
    }
}
