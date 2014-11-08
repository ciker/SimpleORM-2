using System;
using System.Collections.Generic;
using SimpleORM.Queries;

namespace SimpleORM
{
    public class QueryBuilder : IQueryBuilder
    {
        public IQuerySingle<T> Get<T>(QueryFunc<T> query, Func<IList<T>> columns = null, bool throwIfNotExists = true)
        {
            throw new NotImplementedException();
        }

        public IQueryCollection<T> Collect<T>(QueryFunc<T> query = null, ColumnsFunc<T> columns = null)
        {
            throw new NotImplementedException();
        }

        public ICollectionQueryBuilder<T> ForEach<T>(IList<T> items)
        {
            throw new NotImplementedException();
        }
    }
}
