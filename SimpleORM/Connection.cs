using System;
using System.Collections.Generic;
using System.Data;
using SimpleORM.Queries;

namespace SimpleORM
{
    public class Connection : IConnection
    {
        public IDbConnection NativeConnection { get; private set; }
        public T Get<T>(IQuerySingle<T> query)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(IQuerySingleOrDefault<T> query)
        {
            throw new NotImplementedException();
        }

        public IList<T> Get<T>(IQueryCollection<T> query)
        {
            throw new NotImplementedException();
        }

        public void Load<T1, T2>(IList<T1> items, ILoadSingle<T1, T2> query)
        {
            throw new NotImplementedException();
        }

        public void Load<T1, T2>(IList<T1> items, ILoadSingleOrDefault<T1, T2> query)
        {
            throw new NotImplementedException();
        }

        public void LoadCollection<T1, T2>(IList<T1> items, ILoadCollection<T1, T2> query)
        {
            throw new NotImplementedException();
        }
    }
}
