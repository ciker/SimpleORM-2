using System;

namespace SimpleORM.Impl.Mappings.Xml.Exceptions
{
    public class DocumentParseException : Exception
    {
        public DocumentParseException(string format, params object[] args) : base(string.Format(format, args)) { }
    }
}
