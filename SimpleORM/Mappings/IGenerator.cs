using System;

namespace SimpleORM.Mappings
{
    public interface IHasGenerator
    {
        IGenerator Generator { get; }
    }

    public interface IGenerator
    {
        Type Type { get; }
    }

    public interface ISequenceGenerator : IGenerator
    {
        string Name { get; }
    }

    public interface IDbAssignedGenerator : IGenerator { }
}