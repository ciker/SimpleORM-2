using System;
using System.Collections.Generic;
using SimpleORM.Queries;

namespace SimpleORM
{
    public interface IQueryBuilder
    {
        IQuerySingle<T> GetSingle<T>(Func<T, bool> query);

        IQuerySingleOrDefault<T> GetSingleOrDefault<T>(Func<T, bool> query);

        IQueryCollection<T> GetCollection<T>(Func<T, bool> query);

        IQueryCollectionThrough<TTarget, TThrough> GetCollection<TTarget, TThrough>(
            Func<TTarget, TThrough, bool> through, 
            Func<TTarget, TThrough, bool> query);
        
        ILoadSingle<TContainer, TTarget> LoadSingle<TContainer, TTarget>(
            IList<TContainer> items, 
            Func<TContainer, TTarget> target, 
            Func<TContainer, TTarget, bool> query);

        ILoadSingleOrDefault<TContainer, TTarget> LoadSingleOrDefault<TContainer, TTarget>(
            IList<TContainer> items, 
            Func<TContainer, TTarget> target, 
            Func<TContainer, TTarget, bool> query);

        ILoadCollection<TContainer, TTarget> LoadCollection<TContainer, TTarget>(
            IList<TContainer> items, 
            Func<TContainer, IList<TTarget>> target, 
            Func<TContainer, TTarget, bool> query);

        ILoadCollectionThrough<TContainer, TTarget, TThrough> LoadCollection<TContainer, TTarget, TThrough>(
            IList<TContainer> items,
            Func<TContainer, IList<TTarget>> target,
            Func<TContainer, TTarget, TThrough, bool> through,
            Func<TTarget, bool> query = null);
    }
}
