using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using log4net;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Factories;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml
{
    public class XmlMappingBuilder : IMappingBuilder
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(XmlMappingBuilder));

        private readonly IDictionary<Type, IRootObjectMapping> _tableOrViewMappings = new Dictionary<Type, IRootObjectMapping>();

        public void Configure(XElement configuration)
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

            if (mapping is ITableMapping || mapping is IViewMapping)
            {
                var rootObjectMapping = mapping as IRootObjectMapping;
                _tableOrViewMappings[rootObjectMapping.Type] = rootObjectMapping;
            }

        }

        public bool TryGetTableOrView(Type type, out IRootObjectMapping mapping)
        {
            return _tableOrViewMappings.TryGetValue(type, out mapping);
        }
    }
}