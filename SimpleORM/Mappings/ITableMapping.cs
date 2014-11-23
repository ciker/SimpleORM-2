using System.Collections.Generic;

namespace SimpleORM.Mappings
{
    public interface ITableMapping : IMapping, IObjectMapping, IHasSubClasses, IHasDiscriminatorColumn, IHasVersion, IHasPrimaryKey { }

    public interface ITablePropertyMapping : IPropertyMapping, IHasGenerator
    {
        bool Insert { get; }
        
        bool Update { get; }
    }

    public interface IHasPrimaryKey
    {
        IList<IPropertyMapping> PrimaryKeyProperties { get; }
    }

    public interface IHasVersion
    {
        IVersionProperty VersionProperty { get; }
    }

    public interface IVersionProperty : IPropertyMapping { }
}