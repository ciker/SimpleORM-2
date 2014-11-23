using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Factories;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml
{
    sealed class XmlVersionProperty : IVersionProperty
    {
        public XmlVersionProperty(IObjectMapping objectMapping, XElement xVersion)
        {
            var name = XmlUtils.GetAsString(xVersion, "@name");

            Member = objectMapping.Type.GetMember(name, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault();

            if (Member == null)
                throw new DocumentParseException("Canot find member '{0}'", Name);

            Name = XmlUtils.GetAsString(xVersion, "@column");


            if (XmlUtils.Exists(xVersion, "@converter"))
            {
                var converterTypeString = XmlUtils.GetAsString(xVersion, "@converter");
                Converter = PropertyTypeConverterFactory.Create(converterTypeString);
            }
        }

        public string Name { get; private set; }

        public MemberInfo Member { get; private set; }

        public IPropertyTypeConverter Converter { get; set; }
    }
}