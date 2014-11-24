using System;

namespace SimpleORM.Exceptions
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string format, params object[] args) : base(string.Format(format, args))
        {
        }
    }
}
