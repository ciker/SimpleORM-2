using System;
using System.Collections.Generic;

namespace SimpleORM.Mappings
{
    public interface ITypeMapping
    {
        string Name { get; }

        IList<Type> SupportedTypes { get; }

        int DbType { get; }

        int Length { get; }
    }
}