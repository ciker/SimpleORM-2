﻿using System;

namespace SimpleORM.Impl.Mappings.Xml.Exceptions
{
    class ParseTypeException : Exception
    {
        public ParseTypeException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}
