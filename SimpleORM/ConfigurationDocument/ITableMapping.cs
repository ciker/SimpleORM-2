using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleORM.ConfigurationDocument
{
    public interface ITableMapping : IMappingDocument
    {
        string Table { get; }

        string Schema { get; }

        Type Type { get; }

        IList<ITablePropertyMapping> Properties { get; }

        IDiscriminatorColumn Discriminator { get; }

        ITablePropertyMapping VersionProperty { get; }

        IList<ITablePropertyMapping> KeyProperties { get; }

        IList<ISubClassMapping> SubClasses { get; } 
    }

    public interface ITablePropertyMapping
    {
        ITypeMapping TypeMapping { get; }

        string Column { get; }

        MemberInfo Property { get; }

        bool Insert { get; }
        
        bool Update { get; }
    }

    public interface IDiscriminatorColumn
    {
        string Column { get; }

        ITypeMapping TypeMapping { get; }
    }

    public interface ISubClassMapping
    {
        object DiscriminatorValue { get; }

        IList<ISubClassMapping> SubClasses { get; }

        IList<ITablePropertyMapping> Properties { get; }
    }

    public interface ISubClassJoin
    {
        string Table { get; }

        IList<ISubClassJoinColumn> ColumnJoins { get; }
    }

    public interface ISubClassJoinColumn
    {
        string Name { get; }
        
        string JoinTable { get; }
        
        string JoinColumn { get; }
    }
}