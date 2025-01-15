namespace Taskly.Web.Application.Exceptions
{
    /// <summary>
    /// Base class for service's exceptions
    /// </summary>
    /// <param name="message">Message of the exception</param>
    public class ServiceException(string message) : Exception(message)
    {
    }
}
