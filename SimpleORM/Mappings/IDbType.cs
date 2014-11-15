using System;

namespace SimpleORM.Mappings
{
    public interface IHasType
    {
        /// <summary>
        /// CLR Type
        /// </summary>
        Type Type { get; }

        string DbType { get; }

        int? Length { get; }
    }
}