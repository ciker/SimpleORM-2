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
            DbParameterName = xParameter.Attribute("db-name").Value;

            var parameterName = xParameter.Attribute("name").Value;

            Parameter = methodInfo.GetParameters().FirstOrDefault(p => p.Name == parameterName);

            if (Parameter == null)
                throw new DocumentParseException("Canot find parameter '{0}'", parameterName);

            XAttribute xConverter;
            if (xParameter.TryGetAttribute("converter", out xConverter))
            {
                Converter = ConverterFactory.Create(xConverter.Value);
            }

            XAttribute xDbType;
            if (xParameter.TryGetAttribute("db-type", out xDbType))
            {
                DbType = xDbType.GetAsEnum<DbType>();
            }

            XAttribute xLength;
            if (xParameter.TryGetAttribute("length", out xLength))
            {
                Length = xLength.GetAsInt();
            }
        }

        public IConverter Converter { get; set; }

        public ParameterInfo Parameter { get; private set; }

        public string DbParameterName { get; private set; }

        public DbType DbType { get; set; }

        public int Length { get; private set; }
    }
}