using System.Xml.Linq;
using SimpleORM.Generators;
using SimpleORM.Impl.Mappings.Xml.Exceptions;

namespace SimpleORM.Impl.Mappings.Xml.Utils
{
    internal static class GeneratorFactory
    {
        public static IGenerator GetGenerator(XElement xGeneratorElement)
        {
            var generatorName = xGeneratorElement.Name;

            if (generatorName == "sequence")
            {
                var sequenceName = XmlUtils.GetAsString(xGeneratorElement, "@name");
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
