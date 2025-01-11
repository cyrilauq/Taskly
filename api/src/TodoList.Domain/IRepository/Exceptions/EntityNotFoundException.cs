namespace TodoList.Domain.IRepository.Exceptions
{
    public class EntityNotFoundException(string message) : Exception(message)
    {

    }
}
