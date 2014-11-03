using System;
using System.Collections.Generic;

namespace SimpleORM.Extensions
{
    public static class ConnectionExtensions
    {
        public static T GetSingle<T>(this IConnection connection, Func<T, bool> query)
        {
            var queryBuilder = new QueryBuilder();
            
            var q = queryBuilder.GetSingle(query);

            return connection.Get(q);
        }

        public static T GetSingleOrDefault<T>(this IConnection connection, Func<T, bool> query)
        {
            var queryBuilder = new QueryBuilder();

            var q = queryBuilder.GetSingleOrDefault(query);

            return connection.Get(q);
        }

        public static IList<T> GetCollection<T>(this IConnection connection, Func<T, bool> query)
        {
            var queryBuilder = new QueryBuilder();

            var q = queryBuilder.GetCollection(query);

            return connection.Get(q);
        }

        public static IList<TTarget> GetCollection<TTarget, TThrough>(this IConnection connection, 
            Func<TTarget, TThrough, bool> through, 
            Func<TTarget, TThrough, bool> query)
        {
            var queryBuilder = new QueryBuilder();

            var q = queryBuilder.GetCollection(query, through);

            return connection.Get(q);
        }

        public static void LoadSingle<TContainer, TTarget>(this IConnection connection, IList<TContainer> items, 
            Func<TContainer, TTarget> target, 
            Func<TContainer, TTarget, bool> query)
        {
            var queryBuilder = new QueryBuilder();

            var q = queryBuilder.LoadSingle(items, target, query);

            connection.Load(items, q);
        }

        public static void LoadSingleOrDefault<TContainer, TTarget>(this IConnection connection, 
            IList<TContainer> items, 
            Func<TContainer, TTarget> target, 
            Func<TContainer, TTarget, bool> query)
        {
            var queryBuilder = new QueryBuilder();

            var q = queryBuilder.LoadSingleOrDefault(items, target, query);

            connection.Load(items, q);
        }

        public static void LoadCollection<TContainer, TTarget>(this IConnection connection, 
            IList<TContainer> items, 
            Func<TContainer, IList<TTarget>> target, 
            Func<TContainer, TTarget, bool> query)
        {
            var queryBuilder = new QueryBuilder();

            var q = queryBuilder.LoadCollection(items, target, query);

            connection.LoadCollection(items, q);
        }

        public static void LoadCollection<TContainer, TTarget, TThrough>(this IConnection connection, 
            IList<TContainer> items,
            Func<TContainer, IList<TTarget>> target,
            Func<TContainer, TTarget, TThrough, bool> through,
            Func<TTarget, bool> query = null)
        {
            var queryBuilder = new QueryBuilder();

            var q = queryBuilder.LoadCollection(items, target, through, query);

            connection.LoadCollection(items, q);
        }
    }
}
