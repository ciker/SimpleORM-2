using System;
using System.Collections.Generic;
using SimpleORM.Queries;

namespace SimpleORM
{
    public class QueryBuilder : IQueryBuilder
    {
        public IQuerySingle<T> GetSingle<T>(Func<T, bool> query)
        {
            throw new NotImplementedException();
        }

        public IQuerySingleOrDefault<T> GetSingleOrDefault<T>(Func<T, bool> query)
        {
            throw new NotImplementedException();
        }

        public IQueryCollection<T> GetCollection<T>(Func<T, bool> query)
        {
            throw new NotImplementedException();
        }

        public IQueryCollectionThrough<TTarget, TThrough> GetCollection<TTarget, TThrough>(Func<TTarget, TThrough, bool> through, Func<TTarget, TThrough, bool> query)
        {
            throw new NotImplementedException();
        }

        public ILoadSingle<TContainer, TTarget> LoadSingle<TContainer, TTarget>(IList<TContainer> items, Func<TContainer, TTarget> target, Func<TContainer, TTarget, bool> query)
        {
            throw new NotImplementedException();
        }

        public ILoadSingleOrDefault<TContainer, TTarget> LoadSingleOrDefault<TContainer, TTarget>(IList<TContainer> items, Func<TContainer, TTarget> target, Func<TContainer, TTarget, bool> query)
        {
            throw new NotImplementedException();
        }

        public ILoadCollection<TContainer, TTarget> LoadCollection<TContainer, TTarget>(IList<TContainer> items, Func<TContainer, IList<TTarget>> target, Func<TContainer, TTarget, bool> query)
        {
            throw new NotImplementedException();
        }

        public ILoadCollectionThrough<TContainer, TTarget, TThrough> LoadCollection<TContainer, TTarget, TThrough>(IList<TContainer> items, Func<TContainer, IList<TTarget>> target, Func<TContainer, TTarget, TThrough, bool> through, Func<TTarget, bool> query = null)
        {
            throw new NotImplementedException();
        }
    }
}
