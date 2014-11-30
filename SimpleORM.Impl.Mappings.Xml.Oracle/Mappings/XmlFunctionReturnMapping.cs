using System.Data;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Factories;
using SimpleORM.Impl.Mappings.Xml.Utils;
using SimpleORM.Oracle.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Oracle.Mappings
{
    sealed class XmlFunctionReturnMapping : IFunctionReturnMapping
    {
        public XmlFunctionReturnMapping(XElement xFunctionReturn)
        {
            XAttribute xConverter;
            if (xFunctionReturn.TryGetAttribute("converter", out xConverter))
            {
                Converter = ConverterFactory.Create(xConverter.Value);
            }

            XAttribute xDbType;
            if (xFunctionReturn.TryGetAttribute("db-type", out xDbType))
            {
                DbType = xDbType.GetAsEnum<DbType>();
            }

            XAttribute xLength;
            if (xFunctionReturn.TryGetAttribute("length", out xLength))
            {
                Length = xLength.GetAsInt();
            }
        }

        public IConverter Converter { get; private set; }

        public DbType DbType { get; set; }

        public int Length { get; private set; }
    }
}