using System;

namespace SimpleORM.PropertyTypeConverters.Exceptions
{
    public class PropertyConversionException : Exception
    {
        public PropertyConversionException(Type type1, Type type2) : base(string.Format("Cannot convert from '{0}' type to '{1}'", type1.FullName, type2.FullName))
        {
        }
    }
}
