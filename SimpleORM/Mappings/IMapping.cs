namespace SimpleORM.Mappings
{
    public interface IMapping
    {
        /// <summary>
        /// Database entity name (table, view, function, procedure etc.)
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Database entity schema
        /// </summary>
        string Schema { get; }
    }
}