using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Factories;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Mappings
{
    sealed class XmlVersionProperty : IVersionProperty
    {
        public XmlVersionProperty(IMapping objectMapping, XElement xVersion)
        {
            var name = xVersion.Attribute("name").Value;

            Member = objectMapping.Type.GetMember(name, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault();

            if (Member == null)
                throw new DocumentParseException("Canot find member '{0}'", Name);

            Name = xVersion.Attribute("column").Value;

            XAttribute xConverter;
            if (xVersion.TryGetAttribute("converter", out xConverter))
            {
                Converter = ConverterFactory.Create(xConverter.Value);
            }
        }

        public string Name { get; private set; }

        public MemberInfo Member { get; private set; }

        public IConverter Converter { get; set; }
    }
}