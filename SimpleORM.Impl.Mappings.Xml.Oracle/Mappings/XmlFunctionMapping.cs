using System;
using System.Collections.Generic;
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
    sealed class XmlFunctionMapping : IFunctionMapping
    {
        public XmlFunctionMapping(XElement xMapping)
        {
            var xClass = XmlUtils.Single(xMapping, "function");

            if (xClass == null)
                throw new DocumentParseException("No function element");

            Schema = XmlUtils.GetAsString(xClass, "@schema");
            Name = XmlUtils.GetAsString(xClass, "@table");

            string delegateFullPath;

            if (XmlUtils.Exists(xClass, "@class"))
            {
                var @class = XmlUtils.GetAsType(xClass, "@class");
                var delegateName = XmlUtils.GetAsString(xClass, "@delegate");

                delegateFullPath = string.Format("{0}.{1}", @class, delegateName);

                var member = @class.GetMember(delegateName, MemberTypes.Field | MemberTypes.Property, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault();

                if (member == null)
                    throw new DocumentParseException("Cannot find static member '{0}' of '{1}' class", delegateName, @class.FullName);

                Type = TypeUtils.GetMemberType(member);
            }
            else
            {
                Type = XmlUtils.GetAsType(xClass, "@delegate");

                delegateFullPath = Type.FullName;
            }

            if (!typeof(Delegate).IsAssignableFrom(Type))
                throw new DocumentParseException("'{0}' should be a delegate", delegateFullPath);

            Delegate = Type.GetMethod("Invoke");

            if (Delegate.ReturnType == typeof(void))
                throw new DocumentParseException("'{0}' should return a value", delegateFullPath);

            Parameters = new List<IParameterMapping>();
            foreach (var xProperty in XmlUtils.Select(xClass, "property"))
            {
                Parameters.Add(new XmlParameterMapping(Delegate, xProperty));
            }

            if (XmlUtils.Exists(xMapping, "return-value"))
            {
                var xFunctionReturn = XmlUtils.Single(xMapping, "return-value");
                Return = new XmlFunctionReturnMapping(xFunctionReturn);
            }
        }

        public string Name { get; private set; }

        public string Schema { get; private set; }

        public Type Type { get; private set; }

        public IList<IParameterMapping> Parameters { get; private set; }

        public MethodInfo Delegate { get; private set; }

        public IFunctionReturnMapping Return { get; set; }
    }

    sealed class XmlFunctionReturnMapping : IFunctionReturnMapping
    {
        public XmlFunctionReturnMapping(XElement xFunctionReturn)
        {
            if (XmlUtils.Exists(xFunctionReturn, "@converter"))
            {
                var converterTypeString = XmlUtils.GetAsString(xFunctionReturn, "@converter");
                Converter = ConverterFactory.Create(converterTypeString);
            }

            if (XmlUtils.Exists(xFunctionReturn, "@db-type"))
            {
                DbType = XmlUtils.GetAsEnum<DbType>(xFunctionReturn, "@db-type");
            }

            Length = XmlUtils.GetAsInt(xFunctionReturn, "@length");
        }

        public IConverter Converter { get; private set; }

        public DbType DbType { get; set; }

        public int Length { get; private set; }
    }
}