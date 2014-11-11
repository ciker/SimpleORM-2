using System;

namespace SimpleORM.ConfigurationDocument
{
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