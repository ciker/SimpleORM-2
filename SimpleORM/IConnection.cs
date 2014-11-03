using System.Collections.Generic;
using System.Data;
using SimpleORM.Queries;

namespace SimpleORM
{
    public interface IConnection
    {
        IDbConnection NativeConnection { get; }

        T Get<T>(IQuerySingle<T> query);
        
        T Get<T>(IQuerySingleOrDefault<T> query);
        
        IList<T> Get<T>(IQueryCollection<T> query);

        IList<TTarget> Get<TTarget, TThrough>(IQueryCollectionThrough<TTarget, TThrough> query);
                
        void Load<TContainer, TTarget>(IList<TContainer> items, ILoadSingle<TContainer, TTarget> query);

        void Load<TContainer, TTarget>(IList<TContainer> items, ILoadSingleOrDefault<TContainer, TTarget> query);
        
        void LoadCollection<TContainer, TTarget>(IList<TContainer> items, ILoadCollection<TContainer, TTarget> query);
        
        void LoadCollection<TContainer, TTarget, TThrough>(IList<TContainer> items, ILoadCollectionThrough<TContainer, TTarget, TThrough> query);
    }
}
