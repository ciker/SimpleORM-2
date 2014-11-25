using System;
using System.Collections.Generic;
using SimpleORM.Impl.Mappings.Xml.Exceptions;

namespace SimpleORM.Impl.Mappings.Xml.Factories
{
    public static class ConverterFactory
    {
        private static readonly IDictionary<string, Type> Shorthands = new Dictionary<string, Type>();

        public static void RegisterShorthand<T>(string name) where T : IConverter
        {
            Shorthands[name] = typeof(T);
        }

        public static IConverter Create(string converterTypeString)
        {
            if (converterTypeString == null)
                throw new ArgumentNullException("converterTypeString");

            Type converterType;
            if (!Shorthands.TryGetValue(converterTypeString, out converterType))
                converterType = Type.GetType(converterTypeString, true);

            if (!typeof(IConverter).IsAssignableFrom(converterType))
                throw new DocumentParseException("Illegal converter class '{0}', class must be inherited from '{1}'", converterType.FullName, typeof(IConverter).FullName);

            return (IConverter)Activator.CreateInstance(converterType);
        }
    }
}