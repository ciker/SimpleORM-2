using System;
using SimpleORM.Mappings;

namespace SimpleORM.Oracle.Mappings
{
    public interface IObjectTableMapping : IMapping
    {
        Type ObjectType { get; set; }
    }
}