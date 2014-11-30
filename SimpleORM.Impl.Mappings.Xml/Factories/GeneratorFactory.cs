using System.Xml.Linq;
using SimpleORM.Generators;
using SimpleORM.Impl.Mappings.Xml.Exceptions;

namespace SimpleORM.Impl.Mappings.Xml.Factories
{
    internal static class GeneratorFactory
    {
        public static IGenerator GetGenerator(XNamespace xNamespace, XElement xGeneratorElement)
        {
            var generatorName = xGeneratorElement.Name;

            if (generatorName == "sequence")
            {
                var sequenceName = xGeneratorElement.Attribute("name").Value;
                return new SequenceGenerator(sequenceName);
            }

            if (generatorName == "db-assigned")
            {
                return DbAssignedGenerator.Instance;
            }

            throw new DocumentParseException("Unknown generator type '{0}'", generatorName);
        }
    }
}