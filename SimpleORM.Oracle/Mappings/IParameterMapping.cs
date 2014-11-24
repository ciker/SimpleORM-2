using System.Collections.Generic;

namespace SimpleORM.Oracle.Mappings
{
    public interface IHasParameters
    {
        IList<IParameterMapping> Parameters { get; }
    }

    public interface IParameterMapping
    {
        string ParameterName { get; }

        string DbParameterName { get; }

        int DbType { get; }
    }
}