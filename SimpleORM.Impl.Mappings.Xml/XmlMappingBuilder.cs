using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using log4net;
using SimpleORM.Converters;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Factories;
using SimpleORM.Impl.Mappings.Xml.Mappings;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.MappingBuilders;

namespace SimpleORM.Impl.Mappings.Xml
{
    public class XmlMappingBuilder : DefaultMappingBuilder
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(XmlMappingBuilder));

        static XmlMappingBuilder()
        {
            RegisterMappingBuilder("table-mapping", xMapping => new XmlTableMapping(xMapping));
            RegisterMappingBuilder("view-mapping", xMapping => new XmlViewMapping(xMapping));

            ConverterFactory.RegisterShorthand<YesNoConverter>("YN");
            ConverterFactory.RegisterShorthand<LowerYesNoConverter>("yn");

            ConverterFactory.RegisterShorthand<TrueFalseConverter>("TF");
            ConverterFactory.RegisterShorthand<LowerTrueFalseConverter>("tf");
        }

        protected static void RegisterMappingBuilder(string documentType, MappingBuilder builder)
        {
            MappingFactory.RegisterMapping(documentType, builder);
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

        private void LoadFromAssembly(string name, string mask)
        {
            var assembly = Assembly.Load(name);

            var regex = new Regex(mask);

            var resourceNames = assembly.GetManifestResourceNames().Where(n => regex.IsMatch(n));

            foreach (var resourceName in resourceNames)
            {
                var resourceStream = assembly.GetManifestResourceStream(resourceName);
                if (resourceStream == null)
                    throw new DocumentLoadException("Cannot load resource '{0}', stream is null", resourceName);

                var streamReader = new StreamReader(resourceStream);
                try
                {
                    var xMapping = XElement.Parse(streamReader.ReadToEnd());

                    var mapping = MappingFactory.CreateMapping(xMapping);

                    RegisterMapping(mapping);
                }
                catch (Exception ex)
                {
                    throw new DocumentParseException(string.Format("Cannot load resource '{0}'", resourceName), ex);
                }
            }
        }
    }
}