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
    sealed class XmlTablePropertyMapping : ITablePropertyMapping
    {
        public XmlTablePropertyMapping(Type classType, XElement xTableProperty)
        {
            Name = XmlUtils.GetAsString(xTableProperty, "@name");

            Member = classType.GetMember(Name, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault();

            if (Member == null)
                throw new DocumentParseException("Canot find member '{0}'", Name);

            var xGenerator = XmlUtils.Single(xTableProperty, "generator");
            if (xGenerator != null)
            {
                var xGeneratorElement = XmlUtils.Single(xGenerator, "*");
                Generator = GeneratorFactory.GetGenerator(xGeneratorElement);
            }

            Insert = !XmlUtils.Exists(xTableProperty, "@insert") || XmlUtils.GetAsBoolean(xTableProperty, "@insert");
            Update = !XmlUtils.Exists(xTableProperty, "@update") || XmlUtils.GetAsBoolean(xTableProperty, "@update");


            if (XmlUtils.Exists(xTableProperty, "@converter"))
            {
                var converterTypeString = XmlUtils.GetAsString(xTableProperty, "@converter");
                Converter = ConverterFactory.Create(converterTypeString);
            }
        }

        public string Name { get; private set; }

        public MemberInfo Member { get; private set; }

        public IGenerator Generator { get; private set; }

        public bool Insert { get; private set; }

        public bool Update { get; private set; }

        public IConverter Converter { get; set; }
    }
}