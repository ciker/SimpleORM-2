using System.Data;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Factories;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Oracle.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Oracle.Mappings
{
    sealed class XmlParameterMapping : IParameterMapping
    {
        public XmlParameterMapping()
        {
            DbType = (DbType)(-1);
        }

        public XmlParameterMapping(MethodInfo methodInfo, XElement xParameter)
        {
            DbParameterName = XmlUtils.GetAsString(xParameter, "@db-name");

            var parameterName = XmlUtils.GetAsString(xParameter, "@name");

            Parameter = methodInfo.GetParameters().FirstOrDefault(p => p.Name == parameterName);

            if (Parameter == null)
                throw new DocumentParseException("Canot find parameter '{0}'", parameterName);

            if (XmlUtils.Exists(xParameter, "@converter"))
            {
                var converterTypeString = XmlUtils.GetAsString(xParameter, "@converter");
                Converter = ConverterFactory.Create(converterTypeString);
            }

            if (XmlUtils.Exists(xParameter, "@db-type"))
            {
                DbType = XmlUtils.GetAsEnum<DbType>(xParameter, "@db-type");
            }

            Length = XmlUtils.GetAsInt(xParameter, "@length");
        }

        public IConverter Converter { get; set; }

        public ParameterInfo Parameter { get; private set; }

        public string DbParameterName { get; private set; }

        public DbType DbType { get; set; }

        public int Length { get; private set; }
    }
}