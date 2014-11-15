using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using log4net;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Mappings;
using SimpleORM.Utils;

namespace SimpleORM.Impl.Mappings.Xml
{
    public class XmlMappingBuilder : IMappingBuilder
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(XmlMappingBuilder));

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
                LoadMapping(streamReader.ReadToEnd());
            }
        }

        private void LoadMapping(string xml)
        {
            
        }

        public bool TryGet(Type type, out IMapping mapping)
        {
            throw new NotImplementedException();
        }
    }
}
