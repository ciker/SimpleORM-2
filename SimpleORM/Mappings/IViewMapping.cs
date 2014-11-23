namespace SimpleORM.Mappings
{
    public interface IViewMapping : IMapping, IObjectMapping, IHasSubClasses, IHasDiscriminatorColumn { }

    public interface IViewPropertyMapping : IPropertyMapping { }
}