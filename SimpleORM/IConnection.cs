using System.Collections.Generic;
using System.Data;
using SimpleORM.Queries;

namespace SimpleORM
{
    public interface IConnection
    {
        IDbConnection NativeConnection { get; }

        T Get<T>(IQuerySingle<T> query);
                
        IList<T> Get<T>(IQueryCollection<T> query);

        IList<TTarget> Get<TTarget, TThrough>(IQueryCollection<TTarget, TThrough> query);
                
        void ForEach<TContainer, TTarget>(IList<TContainer> items, ILoadSingle<TContainer, TTarget> query);

        void ForEach<TContainer, TTarget>(IList<TContainer> items, ILoadSubCollection<TContainer, TTarget> query);

        void ForEach<TContainer, TTarget, TThrough>(IList<TContainer> items, ILoadSubCollection<TContainer, TTarget, TThrough> query);
    }
}
