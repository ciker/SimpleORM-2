using System;
using System.Collections.Generic;

namespace SimpleORM.Mappings
{
    public interface IObjectMapping
    {
        /// <summary>
        /// .NET object type
        /// </summary>
        Type Type { get; }

        IList<IPropertyMapping> Properties { get; }
    }

    public interface IDiscriminatorColumn
    {
        Type Type { get; }

        IPropertyTypeConverter Converter { get; set; }

        string Column { get; }
    }

    public interface ISubClassMapping : IObjectMapping, IHasSubClasses
    {
        object DiscriminatorValue { get; }

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