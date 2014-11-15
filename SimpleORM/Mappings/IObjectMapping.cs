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

        IDiscriminatorColumn Discriminator { get; }

        IList<ISubClassMapping> SubClasses { get; }
    }

    public interface IDiscriminatorColumn
    {
        string Column { get; }

        ITypeMapping TypeMapping { get; }
    }

    public interface ISubClassMapping : IObjectMapping
    {
        object DiscriminatorValue { get; }

        ISubClassJoin Join { get; }
    }

    public interface ISubClassJoin
    {
        string Schema { get; }

        string Table { get; }

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