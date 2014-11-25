using System.Data;
using System.Reflection;
using SimpleORM.Mappings;

namespace SimpleORM.Oracle.Mappings
{
    public interface IFunctionMapping : IMapping, IHasParameters
    {
        MethodInfo Delegate { get; }

        IFunctionReturnMapping Return { get; set; }
    }

    public interface IFunctionReturnMapping
    {
        IConverter Converter { get; }

        DbType DbType { get; set; }

        int Length { get; }
    }
}