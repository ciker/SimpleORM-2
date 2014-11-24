using System;

namespace SimpleORM.Impl.Mappings.Xml.Exceptions
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string format, params object[] args) : base(string.Format(format, args))
        {
        }
    }
}
