using System;
using System.Collections.Generic;

namespace SimpleORM.Mappings
{
    public interface IHasProperties
    {
        IList<IPropertyMapping> Properties { get; }
    }

    public interface IDiscriminatorColumn
    {
        Type Type { get; }

        IConverter Converter { get; set; }

        string Column { get; }
    }

    public interface ITableViewMapping : IMapping, IHasProperties, IHasDiscriminatorColumn, IHasSubClasses { }

    public interface ISubClassMapping : IHasProperties, IHasSubClasses
    {
        ISubClassJoin Join { get; }
    }

    public interface ISubClassJoin 
    {
        string Name { get; }

        string Schema { get; }

        IList<ISubClassJoinColumn> ColumnJoins { get; }
    }

    public interface ISubClassJoinColumn
    {
        string Name { get; }

        string JoinSchema { get; }

        string JoinTable { get; }

        string JoinColumn { get; }
    }
}