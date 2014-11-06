using System;

namespace SimpleORM.Queries
{
    public interface IQuery
    {
        string SQL { get; }
    }

    public interface IQuerySingle<T>
    {
        
    }

    public interface IQueryCollection<T>
    {
        IQueryCollection<T, TThrough> Through<TThrough>(Func<T, TThrough, bool> through, QueryFunc<TThrough> query);
    }

    public interface IQueryCollection<TTarget, TThrough>
    {
        
    }

    public interface ILoadSingle<TContainer, TTarget>
    {
        
    }

    public interface ILoadSubCollection<TContainer, TTarget>
    {
    }

    public interface IThrough<TContainer, TTarget>
    {
        ILoadSubCollection<TContainer, TTarget, TThrough> Through<TThrough>(Func<TContainer, TTarget, TThrough, bool> through);
    }

    public interface ILoadSubCollection<TContainer, TTarget, TThrough>
    {
        
    }
}
