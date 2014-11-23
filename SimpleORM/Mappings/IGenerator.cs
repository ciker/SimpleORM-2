namespace SimpleORM.Mappings
{
    public interface IHasGenerator
    {
        IGenerator Generator { get; }
    }
}