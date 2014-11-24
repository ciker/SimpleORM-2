using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using log4net;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Factories;
using SimpleORM.Impl.Mappings.Xml.Mappings;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.MappingBuilders;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml
{
    public class XmlMappingBuilder : DefaultMappingBuilder
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(XmlMappingBuilder));

        public XmlMappingBuilder()
        {
            RegisterMappingBuilder("table-mapping", xMapping => new XmlTableMapping(xMapping));
            RegisterMappingBuilder("view-mapping", xMapping => new XmlViewMapping(xMapping));
        }

        public sealed override void Configure(XElement configuration)
        {
            //TODO - validate using schema
            //configuration.Validate();

            var source = configuration.Elements().First();

            if (source.Name == "assembly-resources")
            {
                var assemblyName = XmlUtils.GetAsString(source, "name");
                var resourcesMask = XmlUtils.GetAsString(source, "mask");

                LoadFromAssembly(assemblyName, resourcesMask);
            }
        }

        protected void RegisterMappingBuilder(string documentType, MappingBuilder builder)
        {
            MappingFactory.RegisterMapping(documentType, builder);
        }

        private void LoadFromAssembly(string name, string mask)
        {
            var assembly = Assembly.Load(name);

            var regex = new Regex(mask);

            var resourceNames = assembly.GetManifestResourceNames()
                .Where(n => regex.IsMatch(n));

            foreach (var resourceName in resourceNames)
            {
                var resourceStream = assembly.GetManifestResourceStream(resourceName);
                if (resourceStream == null)
                    throw new DocumentLoadException("Cannot load resource '{0}', stream is null", resourceName);

                var streamReader = new StreamReader(resourceStream);
                try
                {
                    RegisterMapping(streamReader.ReadToEnd());
                }
                catch (Exception ex)
                {
                    throw new DocumentParseException(string.Format("Cannot load '{0}'", resourceName), ex);
                }
            }
        }

        private void RegisterMapping(string xml)
        {
            var xMapping = XElement.Parse(xml);

            var mapping = MappingFactory.CreateMapping(xMapping);

            IDictionary<Type, IMapping> mappingTypes;
            if (!_mappings.TryGetValue(mapping.Type, out mappingTypes))
            {
                mappingTypes = new Dictionary<Type, IMapping>();
                _mappings[mapping.Type] = mappingTypes;
            }

            var mappingType = mapping is ITableViewMapping
                ? typeof(ITableViewMapping)
                : mapping.GetType();


            IMapping checkMapping;
            if (mappingTypes.TryGetValue(mappingType, out checkMapping))
                throw new ConfigurationException("Mapping of type '{0}' for '{1} type is already registered for '{2}' type",
                    mappingType.FullName, mapping.Type, checkMapping.Type);

            mappingTypes[mappingType] = mapping;
        }
    }
}