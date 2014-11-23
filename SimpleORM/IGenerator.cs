namespace SimpleORM
{
    public interface IGenerator
    {
        object GetNextValue(Connection connection);
    }
}
