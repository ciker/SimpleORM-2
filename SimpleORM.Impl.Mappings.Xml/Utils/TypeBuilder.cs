using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings.Xml.Utils
{
    sealed internal class TypeBuilder
    {
        private static readonly IDictionary<string, Type> Shorthands = new Dictionary<string, Type>
        {
            { "bool", typeof(bool) },

            { "string", typeof(string) },
            { "type", typeof(Type) },
            
            { "date-time", typeof(DateTime) },

            { "byte", typeof(byte) },
            { "short", typeof(short) },
            { "int", typeof(int) },
            { "long", typeof(long) },
            { "float", typeof(float) },
            { "double", typeof(double) },
            { "decimal", typeof(decimal) }
        };

        public static void ParseType(XElement element, out Type type)
        {
            type = null;
            var typeStr = XmlUtils.GetAsString(element, "@type");

            if (!string.IsNullOrEmpty(typeStr))
            {
                var isNullable = typeStr[typeStr.Length] == '?';

                if (isNullable)
                    typeStr = typeStr.Substring(0, typeStr.Length - 1);

                if (!Shorthands.TryGetValue(typeStr, out type))
                    type = Type.GetType(typeStr);

                if (type == null)
                    throw new TypeParseException("Unknown type '{0}'", typeStr);

                if (isNullable)
                {
                    if (!type.IsPrimitive)
                        throw new TypeParseException("Cannot make type '{0}' nullable, type isn't primitive", typeStr);

                    type = typeof(Nullable<>).MakeGenericType(type);
                }
            }


        }
    }
}
