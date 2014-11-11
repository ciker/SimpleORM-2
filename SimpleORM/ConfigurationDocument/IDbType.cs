using System;
using System.Collections.Generic;

namespace SimpleORM.ConfigurationDocument
{
    public interface ITypeMapping
    {
        string Name { get; }

        IList<Type> SupportedTypes { get; }

        int DbType { get; }

        int Length { get; }
    }
}