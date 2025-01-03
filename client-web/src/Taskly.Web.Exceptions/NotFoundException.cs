namespace Taskly.Web.Exceptions
{
    public class NotFoundException(string message): Exception(message)
    {
    }
}
