using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SimpleORM.Impl.Mappings.Xml.Utils
{
    public static class XmlUtils
    {
        public static string GetAsString(XElement element, string xPath, string defaultValue = default(string))
        {
            var enumerable = (IEnumerable)element.XPathEvaluate(xPath);

            var result = enumerable.Cast<XObject>().FirstOrDefault();

            if (result == null)
                return defaultValue;

            var xElement = result as XElement;
            if (xElement != null)
                return (xElement).Value;

            var xAttribute = result as XAttribute;
            if (xAttribute != null)
                return (xAttribute).Value;

            var xText = result as XText;

            return xText != null ? xText.Value : defaultValue;
        }

        public static int GetAsByte(XElement element, string xPath)
        {
            var stringValue = GetAsString(element, xPath);

            return byte.Parse(stringValue);
        }

        public static int GetAsShort(XElement element, string xPath)
        {
            var stringValue = GetAsString(element, xPath);

            return short.Parse(stringValue);
        }

        public static int GetAsInt(XElement element, string xPath)
        {
            var stringValue = GetAsString(element, xPath);

            return int.Parse(stringValue);
        }

        public static long GetAsLong(XElement element, string xPath)
        {
            var stringValue = GetAsString(element, xPath);

            return long.Parse(stringValue);
        }

        public static DateTime GetAsDateTime(XElement element, string xPath)
        {
            var stringValue = GetAsString(element, xPath);

            return DateTime.Parse(stringValue);
        }

        public static DateTime GetAsDateTime(XElement element, string xPath, string format)
        {
            var stringValue = GetAsString(element, xPath);

            return DateTime.ParseExact(stringValue, format, CultureInfo.InvariantCulture);
        }

        public static Type GetAsType(XElement element, string xPath)
        {
            var stringValue = GetAsString(element, xPath);

            return Type.GetType(stringValue, true);
        }

        public static bool GetAsBoolean(XElement element, string xPath)
        {
            var stringValue = GetAsString(element, xPath);

            return bool.Parse(stringValue);
        }

        public static Guid GetAsGuid(XElement element, string xPath)
        {
            var stringValue = GetAsString(element, xPath);

            return Guid.Parse(stringValue);
        }

        public static T GetAsEnum<T>(XElement element, string xPath)
        {
            var str = GetAsString(element, xPath);

            return (T)Enum.Parse(typeof(T), str);
        }

        public static IEnumerable<XElement> Select(XElement element, string xPath)
        {
            return element.XPathSelectElements(xPath);
        }

        public static XElement Single(XElement element, string xPath)
        {
            return element.XPathSelectElement(xPath);
        }

        public static bool Exists(XElement element, string xPath)
        {
            return element.XPathSelectElement(xPath) != null;
        }
    }
}
