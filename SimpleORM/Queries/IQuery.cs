namespace SimpleORM.Queries
{
    public interface IQuery
    {
        string SQL { get; }
    }

    public interface IQuerySingle<T>
    {
        
    }

    public interface IQuerySingleOrDefault<T>
    {
        
    }

    public interface IQueryCollection<T>
    {
        
    }

    public interface IQueryCollectionThrough<TTarget, TThrough>
    {
        
    }

    public interface ILoadSingle<TContainer, TTarget>
    {
        
    }

    public interface ILoadSingleOrDefault<TContainer, TTarget>
    {
        
    }

    public interface ILoadCollection<TContainer, TTarget>
    {
        
    }

    public interface ILoadCollectionThrough<TContainer, TTarget, TThrough>
    {
        
    }
}
