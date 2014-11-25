using System.Reflection;
using SimpleORM.Mappings;

namespace SimpleORM.Oracle.Mappings
{
    public interface IProcedureMapping : IMapping, IHasParameters
    {
        MethodInfo Delegate { get; }
    }
}