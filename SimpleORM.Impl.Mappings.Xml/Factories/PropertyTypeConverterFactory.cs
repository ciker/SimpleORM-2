using System;
using SimpleORM.Impl.Mappings.Xml.Exceptions;

namespace SimpleORM.Impl.Mappings.Xml.Factories
{
    public static class PropertyTypeConverterFactory
    {
        public static IPropertyTypeConverter Create(string converterTypeString)
        {
            if (converterTypeString == null)
                throw new ArgumentNullException("converterTypeString");
            
            var converterType = Type.GetType(converterTypeString, true);

            if (!typeof(IPropertyTypeConverter).IsAssignableFrom(converterType))
                throw new DocumentParseException("Illegal converter class '{0}', class must be inherited from '{1}'", converterType.FullName, typeof(IPropertyTypeConverter).FullName);

            return (IPropertyTypeConverter)Activator.CreateInstance(converterType);
        }
    }
}
