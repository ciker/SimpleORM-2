using System;

namespace SimpleORM.ConfigurationDocument
{
    public interface IMappingDocument
    {
        Type ClassType { get; }
    }
}
