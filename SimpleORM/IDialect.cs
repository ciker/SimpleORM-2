namespace SimpleORM
{
    public interface IDialect
    {
        IQueryBuilder QueryBuilder { get; }
    }
}