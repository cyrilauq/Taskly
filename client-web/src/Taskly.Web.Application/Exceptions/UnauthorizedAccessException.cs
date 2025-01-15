namespace Taskly.Web.Application.Exceptions
{
    public class UnauthorizedAccessException(string message) : ServiceException(message)
    {
    }
}
