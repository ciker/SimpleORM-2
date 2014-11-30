using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Factories;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Mappings
{
    sealed class XmlViewPropertyMapping : IViewPropertyMapping
    {
        public XmlViewPropertyMapping(Type classType, XElement xTableProperty)
        {
            Name = xTableProperty.Attribute("column").Value;

            var name = xTableProperty.Attribute("name").Value;

            Member = classType.GetMember(name, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault();

            if (Member == null)
                throw new DocumentParseException("Canot find member '{0}'", name);

            XAttribute xConverter;
            if (xTableProperty.TryGetAttribute("converter", out xConverter))
            {
                Converter = ConverterFactory.Create(xConverter.Value);
            }
        }


        public string Name { get; private set; }

        public MemberInfo Member { get; private set; }

        public IConverter Converter { get; set; }
    }
}