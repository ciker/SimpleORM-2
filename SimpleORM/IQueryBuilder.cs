using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SimpleORM.Queries;

namespace SimpleORM
{
    public delegate object ColumnsFunc<in T>(T t);
    public delegate bool QueryFunc<in T>(T t);
    public delegate TTarget SingleTarget<in TContainer, out TTarget>(TContainer container);
    public delegate IList<TTarget> CollectionTarget<in TContainer, TTarget>(TContainer container);
    public delegate bool Join<in TContainer, in TTarget>(TContainer container, TTarget target);

    public interface IQueryBuilder
    {
        IQuerySingle<T> Get<T>(Func<T, bool> query, Func<IList<T>> columns = null, bool throwIfNotExists = true);

        IQueryCollection<T> Collect<T>(Func<T, bool> query = null, ColumnsFunc<T> columns = null);
        
        ICollectionQueryBuilder<T> ForEach<T>(IList<T> items);
    }

    public interface ICollectionQueryBuilder<T>
    {
        ILoadSingle<T, TTarget> Load<TTarget>(
            SingleTarget<T, TTarget> target,
            Join<T, TTarget> join,
            ColumnsFunc<TTarget> columns = null,
            bool throwIfNotExists = true);

        ILoadSubCollection<T, TTarget> Load<TTarget>(
            CollectionTarget<T, TTarget> target,
            Join<T, TTarget> join,
            ColumnsFunc<TTarget> columns = null);

        IThrough<T, TTarget> Load<TTarget>(
            CollectionTarget<T, TTarget> target,
            QueryFunc<TTarget> query,
            ColumnsFunc<TTarget> columns = null);

        IThrough<T, TTarget> Load<TTarget>(
            CollectionTarget<T, TTarget> target,
            ColumnsFunc<TTarget> columns = null);
    }
}
