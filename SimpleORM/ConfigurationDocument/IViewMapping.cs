using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleORM.ConfigurationDocument
{
    public interface IViewMapping : IMappingDocument
    {
        string Table { get; }

        string Schema { get; }

        Type Type { get; }

        IList<ITablePropertyMapping> Properties { get; }
    }

    public interface IViewPropertyMapping
    {
        ITypeMapping TypeMapping { get; }

        string Column { get; }

        MemberInfo Property { get; }
    }
}