using System.Collections.Generic;

namespace SimpleORM.Mappings
{
    public interface ITableMapping : IMapping, IObjectMapping, IHasVersion, IHasPrimaryKey { }

    public interface ITablePropertyMapping : IPropertyMapping, IHasGenerator
    {
        bool Insert { get; }
        
        bool Update { get; }
    }

    public interface IHasPrimaryKey
    {
        IList<IPropertyMapping> KeyProperties { get; }
    }

    public interface IHasVersion
    {
        IPropertyMapping VersionProperty { get; }
    }
}