namespace TodoList.Domain.Exceptions
{
    public class EntityNotExistsException(string message) : Exception(message)
    {
    }
}
