using System;

namespace SimpleORM.Impl.Mappings.Xml.Exceptions
{
    public sealed class DocumentLoadException : Exception
    {
        public DocumentLoadException(string format, params object[] args) : base(string.Format(format, args))
        {
        }
    }
}
