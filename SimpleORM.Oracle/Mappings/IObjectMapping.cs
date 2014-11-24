using SimpleORM.Mappings;

namespace SimpleORM.Oracle.Mappings
{
    public interface IObjectMapping : IMapping, IHasProperties { }

    public interface IObjectPropertyMapping : IPropertyMapping { }
}