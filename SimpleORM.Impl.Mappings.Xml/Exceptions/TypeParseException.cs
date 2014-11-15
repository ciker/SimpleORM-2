using System;

namespace SimpleORM.Impl.Mappings.Xml.Exceptions
{
    class TypeParseException : Exception
    {
        public TypeParseException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}
